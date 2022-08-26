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


  private _position: string = "10px";
  public get position(): string {
    return this._position;
  }
  public set position(v: string) {
    this._position = v;
  }


  private _amount: number = 2100;
  public get amount(): number {
    return this._amount;
  }
  public set amount(v: number) {
    this._amount = v;
  }


  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
  }

  onInputChange(event: MatSliderChange) {
    this.position = ((((event.value ?? 0) / 100) * 280) + 10) + "px";
    this.amount = (((event.value ?? 0) / 100) * (15000 - 2100)) + 2100
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
