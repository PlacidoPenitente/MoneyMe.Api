import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Quote } from 'src/quote.model';

@Component({
  selector: 'app-quote-calculator',
  templateUrl: './quote-calculator.component.html',
  styleUrls: ['./quote-calculator.component.css']
})
export class QuoteCalculatorComponent implements OnInit {

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

  private _product!: string;
  public get product(): string {
    return this._product;
  }
  public set product(v: string) {
    this._product = v;
  }

  private _loanAmount!: number;
  public get loanAmount(): number {
    return this._loanAmount;
  }
  public set loanAmount(v: number) {
    this._loanAmount = v;
  }

  private _terms!: number;
  public get terms(): number {
    return this._terms;
  }
  public set terms(v: number) {
    this._terms = v;
  }

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.loanAmount = 5000;
    this.terms = 24;
  }

  public async requestQuoteAsync(): Promise<void> {
    this.formGroup.markAllAsTouched();
    this.formGroup.updateValueAndValidity();

    if (this.formGroup.invalid) return;

    var quote = new Quote();
    quote.AmountRequired = 5000;
    quote.Term = 6;
    quote.Title = "Mr.";
    quote.FirstName = "Jay Mark";
    quote.LastName = "Estrera";
    quote.DateOfBirth = new Date(Date.parse("06/15/1995"));
    quote.Mobile = "+6412345678";
    quote.Email = "jaymark.estrera@gmail.com";

    var redirectUrlObservable = await this.httpClient.post<string>("https://localhost:5001/api/quote/request", quote, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
    redirectUrlObservable.subscribe({ next: (redirectUrl) => console.log("first") });
  }

  public format(value: number): string {
    return "$" + value.toLocaleString();
  }

  public formatAmortization(value: number): string {
    return value + " months";
  }
}