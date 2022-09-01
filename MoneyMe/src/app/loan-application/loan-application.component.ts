import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Customer } from 'src/customer.model';
import { LoanApplication } from 'src/loan-applicatiom.model';
import { Quote } from 'src/quote.model';

@Component({
  selector: 'app-loan-application',
  templateUrl: './loan-application.component.html',
  styleUrls: ['./loan-application.component.css']
})
export class LoanApplicationComponent implements OnInit {

  private _loanApplication: LoanApplication = new LoanApplication();
  public get loanApplication(): LoanApplication {
    return this._loanApplication;
  }
  public set loanApplication(v: LoanApplication) {
    this._loanApplication = v;
  }

  private _customer: Customer = new Customer();
  public get customer(): Customer {
    return this._customer;
  }
  public set customer(v: Customer) {
    this._customer = v;
  }

  constructor(private httpClient: HttpClient, private _activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    var quoteId = this._activatedRoute.snapshot.paramMap.get('id') || '';

    this.httpClient.get<LoanApplication>(`https://localhost:5001/api/quote/${quoteId}`).subscribe(
      {
        next: loanApplication => {
          this.loanApplication = loanApplication;
          console.log(this.loanApplication.amountRequired)
          this.httpClient.get<Customer>(`https://localhost:5001/api/customer/${loanApplication.customerId}`).subscribe({
            next: customer => this.customer = customer
          });
        }
      });
  }

  public async applyForLoanAsync(): Promise<void> {

  }

}
