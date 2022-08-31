import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AfterViewInit, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Customer } from 'src/customer.model';
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

  constructor(private httpClient: HttpClient, private _activatedRoute: ActivatedRoute, private changeDetectorRef: ChangeDetectorRef) {

  }
  ngAfterViewInit(): void {
    this.changeDetectorRef.detectChanges();
  }

  ngOnInit(): void {
    this.httpClient.get<Product[]>(`https://localhost:5001/api/product/all`).subscribe(
      {
        next: products => {
          this.products = products;

          var quoteUrl = this._activatedRoute.snapshot.paramMap.get('encryptedQuoteUrl') || '';

          this.httpClient.get<PartialQuote>(`https://localhost:5001/api/quote/continue/${encodeURIComponent(quoteUrl)}`).subscribe(
            {
              next: partialQuote => {
                this.partialQuote = partialQuote;
                console.log(partialQuote.term)
                this.selectedProduct = this.products.find(p => p.minimumDuration <= this.partialQuote.term && p.maximumDuration >= this.partialQuote.term) || products[products.length - 1];
                this.updateSlider(partialQuote.amountRequired, this.sliderValue);

                this.httpClient.get<Customer>(`https://localhost:5001/api/customer/${this.partialQuote.customerId}`).subscribe(
                  {
                    next: customer => {
                      this.formGroup.patchValue({ 'title': this.titles.indexOf(customer.title) + 1 });
                      this.formGroup.patchValue({ 'firstName': customer.firstName });
                      this.formGroup.patchValue({ 'lastName': customer.lastName });
                      this.formGroup.patchValue({ 'dateOfBirth': customer.dateOfBirth });
                      this.formGroup.patchValue({ 'mobile': customer.mobile });
                      this.formGroup.patchValue({ 'email': customer.email });
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
    quote.AmountRequired = this.loanAmount ?? this.partialQuote.amountRequired;
    console.log(this.partialQuote.term)
    quote.Term = this.term || this.partialQuote.term;
    quote.Title = this.titles[this.formGroup.get('title')?.value];
    quote.FirstName = this.formGroup.get('firstName')?.value;
    quote.LastName = this.formGroup.get('lastName')?.value;
    quote.DateOfBirth = this.formGroup.get('dateOfBirth')?.value;
    quote.Mobile = this.formGroup.get('mobile')?.value;
    quote.Email = this.formGroup.get('email')?.value;

    this.partialQuote.productId = this.selectedProduct.id;

    var redirectUrlObservable = await this.httpClient.post<Quote>("https://localhost:5001/api/quote/calculate", this.partialQuote, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
    redirectUrlObservable.subscribe({ next: (redirectUrl) => console.log("first") });
  }

  public format(value: number): string {
    return ("$" + value).toLocaleString();
  }

  public formatAmortization(value: number): string {
    return value + " months";
  }
}