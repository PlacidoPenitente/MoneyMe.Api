import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css']
})
export class UserFormComponent implements OnInit {
  private nameRegex: string = "^[a-zA-Z0-9]+(([',.]?[- ]?[a-zA-Z0-9])?[a-zA-Z0-9]*)*[.]{0,1}$";
  private mobileRegex: string = "^[0][4-5][0-9]{8}$";

  private _formGroup!: FormGroup;
  @Input()
  public get formGroup(): FormGroup {
    return this._formGroup;
  }
  public set formGroup(v: FormGroup) {
    this._formGroup = v;
    this._formGroup.addControl('title', this.titleControl);
    this._formGroup.addControl('firstName', this.firstNameControl);
    this._formGroup.addControl('lastName', this.lastNameControl);
    this._formGroup.addControl('dateOfBirth', this.dateOfBirthControl);
    this._formGroup.addControl('email', this.emailControl);
    this._formGroup.addControl('mobile', this.mobileControl);
  }

  private _titleControl: FormControl = new FormControl('', {
    validators: [Validators.required],
    updateOn: 'change'
  });
  public get titleControl(): FormControl {
    return this._titleControl;
  }
  public set titleControl(v: FormControl) {
    this._titleControl = v;
  }

  private _mobileControl: FormControl = new FormControl('', {
    validators: [Validators.pattern(this.mobileRegex), Validators.required],
    updateOn: 'change'
  });
  public get mobileControl(): FormControl {
    return this._mobileControl;
  }
  public set mobileControl(v: FormControl) {
    this._mobileControl = v;
  }

  private _firstNameControl: FormControl = new FormControl('', {
    validators: [Validators.required, Validators.pattern(this.nameRegex), Validators.maxLength(35)],
    updateOn: 'change'
  });
  public get firstNameControl(): FormControl {
    return this._firstNameControl;
  }
  public set firstNameControl(v: FormControl) {
    this._firstNameControl = v;
  }

  private _lastNameControl: FormControl = new FormControl('', {
    validators: [Validators.required, Validators.pattern(this.nameRegex), Validators.maxLength(35)],
    updateOn: 'change'
  });
  public get lastNameControl(): FormControl {
    return this._lastNameControl;
  }
  public set lastNameControl(v: FormControl) {
    this._lastNameControl = v;
  }

  private _dateOfBirthControl: FormControl = new FormControl('', {
    validators: [Validators.required],
    updateOn: 'change'
  });
  public get dateOfBirthControl(): FormControl {
    return this._dateOfBirthControl;
  }
  public set dateOfBirthControl(v: FormControl) {
    this._dateOfBirthControl = v;
  }

  private _emailControl: FormControl = new FormControl('', {
    validators: [Validators.required, Validators.email],
    updateOn: 'change'
  });
  public get emailControl(): FormControl {
    return this._emailControl;
  }
  public set emailControl(v: FormControl) {
    this._emailControl = v;
  }

  private _maxDate!: Date;
  public get maxDate(): Date {
    return this._maxDate;
  }
  public set maxDate(v: Date) {
    this._maxDate = v;
  }

  private _minDate!: Date;
  public get minDate(): Date {
    return this._minDate;
  }
  public set minDate(v: Date) {
    this._minDate = v;
  }

  constructor() { }

  ngOnInit(): void {
    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);

    this.minDate = new Date();
    this.minDate.setFullYear(this.maxDate.getFullYear() - 75 + 18);
  }

}