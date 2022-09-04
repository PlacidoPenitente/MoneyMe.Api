import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AfterViewInit, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Customer } from 'src/customer.model';
import { LoanApplication } from 'src/loan-applicatiom.model';
import { PartialQuote } from 'src/partial-quote.model';
import { Product } from 'src/product.model';
import { Quote } from 'src/quote.model';

@Component({
  selector: 'app-quote-calculator',
  templateUrl: './quote-calculator.component.html',
  styleUrls: ['./quote-calculator.component.css']
})
export class QuoteCalculatorComponent implements OnInit, AfterViewInit {

  private titles: string[] = ['Mr.', 'Ms.', 'Mrs.'];

  private _formGroup: FormGroup = new FormGroup({});
  public get formGroup(): FormGroup {
    return this._formGroup;
  }
  public set formGroup(v: FormGroup) {
    this._formGroup = v;
  }

  private _panelState: boolean = false;
  public get panelState(): boolean {
    return this._panelState;
  }
  public set panelState(v: boolean) {
    this._panelState = v;
  }

  private _products!: Product[];
  public get products(): Product[] {
    return this._products;
  }
  public set products(v: Product[]) {
    this._products = v;
  }

  private _partialQuote: PartialQuote = new PartialQuote();
  public get partialQuote(): PartialQuote {
    return this._partialQuote;
  }
  public set partialQuote(v: PartialQuote) {
    this._partialQuote = v;
  }

  private _selectedProduct: Product = new Product();
  public get selectedProduct(): Product {
    return this._selectedProduct;
  }
  public set selectedProduct(v: Product) {
    this._selectedProduct = v;
    this.updateTermSlider(this.partialQuote.term, this.sliderValue, v.minimumDuration, v.maximumDuration);
  }

  private _loanAmount!: number;
  public get loanAmount(): number {
    return this._loanAmount;
  }
  public set loanAmount(v: number) {
    this._loanAmount = v;
  }

  private _term!: number;
  public get term(): number {
    return this._term;
  }
  public set term(v: number) {
    this._term = v;
  }

  constructor(private httpClient: HttpClient, private router: Router, private _activatedRoute: ActivatedRoute, private changeDetectorRef: ChangeDetectorRef) {

  }
  ngAfterViewInit(): void {
    this.changeDetectorRef.detectChanges();
  }

  ngOnInit(): void {
    this.httpClient.get<Product[]>(`https://localhost:5001/api/product/all`).subscribe(
      {
        next: products => {
          this.products = products;

          var encryptedQuoteUrl = this._activatedRoute.snapshot.paramMap.get('encryptedQuoteUrl') || '';
          this.httpClient.post<PartialQuote>(`https://localhost:5001/api/quote/continue`, { "encryptedQuoteUrl": encodeURIComponent(encryptedQuoteUrl) }, {
            headers: new HttpHeaders({
              'Content-Type': 'application/json'
            })
          }).subscribe(
            {
              next: partialQuote => {
                this.partialQuote = partialQuote;
                this.selectedProduct = this.products.find(p => p.minimumDuration <= this.partialQuote.term && p.maximumDuration >= this.partialQuote.term) || products[products.length - 1];
                this.updateSlider(partialQuote.loanAmount, this.sliderValue);

                this.httpClient.get<Customer>(`https://localhost:5001/api/customer/${this.partialQuote.customerId}`).subscribe(
                  {
                    next: customer => {
                      this.formGroup.patchValue({ 'title': this.titles.indexOf(customer.title) + 1 });
                      this.formGroup.patchValue({ 'firstName': customer.firstName });
                      this.formGroup.patchValue({ 'lastName': customer.lastName });
                      this.formGroup.patchValue({ 'dateOfBirth': customer.dateOfBirth });
                      this.formGroup.patchValue({ 'mobile': customer.mobileNumber });
                      this.formGroup.patchValue({ 'email': customer.emailAddress });
                    }
                  });
              }
            });
        }
      });
  }

  private _updateSlider!: Function;
  public get updateSlider(): Function {
    return this._updateSlider;
  }
  public set updateSlider(v: Function) {
    this._updateSlider = v;
  }

  private _updateTermSlider!: Function;
  public get updateTermSlider(): Function {
    return this._updateTermSlider;
  }
  public set updateTermSlider(v: Function) {
    this._updateTermSlider = v;
  }

  private _sliderValue!: string;
  public get sliderValue(): string {
    return this._sliderValue;
  }
  public set sliderValue(v: string) {
    this._sliderValue = v;
  }

  public async requestQuoteAsync(): Promise<void> {
    this.formGroup.markAllAsTouched();
    this.formGroup.updateValueAndValidity();

    if (this.formGroup.invalid) return;

    var quote = new Quote();
    quote.LoanAmount = this.loanAmount ?? this.partialQuote.loanAmount;
    quote.Term = this.term || this.partialQuote.term;
    quote.Title = this.titles[this.formGroup.get('title')?.value];
    quote.FirstName = this.formGroup.get('firstName')?.value;
    quote.LastName = this.formGroup.get('lastName')?.value;
    quote.DateOfBirth = this.formGroup.get('dateOfBirth')?.value;
    quote.MobileNumber = this.formGroup.get('mobile')?.value;
    quote.EmailAddress = this.formGroup.get('email')?.value;

    this.partialQuote.productId = this.selectedProduct.id;

    var redirectUrlObservable = await this.httpClient.post<LoanApplication>("https://localhost:5001/api/quote/calculate", this.partialQuote, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
    redirectUrlObservable.subscribe({ next: loanApplication => this.router.navigateByUrl('apply/' + loanApplication.id) });
  }

  public format(value: number): string {
    return ("$" + value).toLocaleString();
  }

  public formatAmortization(value: number): string {
    return value + " months";
  }
}