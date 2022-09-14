import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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

  constructor(private httpClient: HttpClient, private _activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    var quoteId = this._activatedRoute.snapshot.paramMap.get('id') || '';

    this.httpClient.get<LoanApplication>(`https://localhost:5001/api/v1/quotes/${quoteId}`).subscribe(
      {
        next: loanApplication => {
          this.loanApplication = loanApplication;
          this.httpClient.get<Customer>(`https://localhost:5001/api/v1/customers/${loanApplication.customerId}`).subscribe({
            next: customer => this.customer = customer
          });
        }
      });
  }

  public editQuote(quoteId: string, userId: string) {
    this.router.navigateByUrl('quote/' + quoteId)
  }

  public async applyForLoanAsync(): Promise<void> {

  }

}
