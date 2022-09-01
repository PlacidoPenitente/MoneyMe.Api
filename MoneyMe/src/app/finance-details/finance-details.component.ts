import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-finance-details',
  templateUrl: './finance-details.component.html',
  styleUrls: ['./finance-details.component.css']
})
export class FinanceDetailsComponent implements OnInit {

  private _amount!: string;
  @Input()
  public get amount(): string {
    return this._amount;
  }
  public set amount(v: string) {
    this._amount = v;
  }

  private _amortization!: string;
  @Input()
  public get amortization(): string {
    return this._amortization;
  }
  public set amortization(v: string) {
    this._amortization = v;
  }

  constructor() { }

  ngOnInit(): void {
  }

}
