import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-loan-application',
  templateUrl: './loan-application.component.html',
  styleUrls: ['./loan-application.component.css']
})
export class LoanApplicationComponent implements OnInit {
  private _panelState: boolean = false;
  public get panelState(): boolean {
    return this._panelState;
  }
  public set panelState(v: boolean) {
    this._panelState = v;
  }
  constructor() { }

  ngOnInit(): void {
  }

}
