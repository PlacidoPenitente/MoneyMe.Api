import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSliderChange } from '@angular/material/slider';
import { Quote } from 'src/quote.model';

@Component({
  selector: 'app-quote-calculator',
  templateUrl: './quote-calculator.component.html',
  styleUrls: ['./quote-calculator.component.css']
})
export class QuoteCalculatorComponent implements OnInit {

  private _position: string = "51.58px";
  public get position(): string {
    return this._position;
  }
  public set position(v: string) {
    this._position = v;
  }

  private _loanAmount: string = "$1,000";
  public get loanAmount(): string {
    return this._loanAmount;
  }
  public set loanAmount(v: string) {
    this._loanAmount = v;
  }

  private _amount: number = 1000;
  public get amount(): number {
    return this._amount;
  }
  public set amount(v: number) {
    this._amount = v;
  }

  private _sliderValue!: number;
  public get sliderValue(): number {
    return this._sliderValue;
  }
  public set sliderValue(v: number) {
    this._sliderValue = v;
  }

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.amount = 5000;
    this.loanAmount = "$" + this.amount.toLocaleString();
    this.sliderValue = (5000 / 100000) * 100;
    this.position = ((5000 / 100000) * 320) + 16 + "px";
  }

  onInputChange(event: MatSliderChange) {
    this.position = ((((event.value ?? 0) / 100) * 320)) + 16 + "px";

    this.amount = (((event.value ?? 0) / 100) * (100000));
    this.loanAmount = "$" + this.amount.toLocaleString();
  }

  public async requestQuoteAsync(): Promise<void> {
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
}
