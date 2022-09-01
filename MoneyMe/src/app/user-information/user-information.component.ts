import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-information',
  templateUrl: './user-information.component.html',
  styleUrls: ['./user-information.component.css']
})
export class UserInformationComponent implements OnInit {

  private _name!: string;
  @Input()
  public get name(): string {
    return this._name;
  }
  public set name(v: string) {
    this._name = v;
  }

  private _mobile!: string;
  @Input()
  public get mobile(): string {
    return this._mobile;
  }
  public set mobile(v: string) {
    this._mobile = v;
  }

  private _email!: string;
  @Input()
  public get email(): string {
    return this._email;
  }
  public set email(v: string) {
    this._email = v;
  }


  constructor() { }

  ngOnInit(): void {
  }
}
