import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Quote } from 'src/quote.model';

@Component({
  selector: 'app-quote-calculator',
  templateUrl: './quote-calculator.component.html',
  styleUrls: ['./quote-calculator.component.css']
})
export class QuoteCalculatorComponent implements OnInit {

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
  }

  formatLabel(value: number) {
    if (value >= 1000) {
      return Math.round(value / 1000) + 'k';
    }

    return value;
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
