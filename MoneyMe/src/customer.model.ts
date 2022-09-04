export class Customer {

    private _id!: string;
    public get id(): string {
        return this._id;
    }
    public set id(v: string) {
        this._id = v;
    }

    private _title!: string;
    public get title(): string {
        return this._title;
    }
    public set title(v: string) {
        this._title = v;
    }

    private _firstName!: string;
    public get firstName(): string {
        return this._firstName;
    }
    public set firstName(v: string) {
        this._firstName = v;
    }

    private _lastName!: string;
    public get lastName(): string {
        return this._lastName;
    }
    public set lastName(v: string) {
        this._lastName = v;
    }

    private _dateOfBirth!: Date;
    public get dateOfBirth(): Date {
        return this._dateOfBirth;
    }
    public set dateOfBirth(v: Date) {
        this._dateOfBirth = v;
    }

    private _mobileNumber!: string;
    public get mobileNumber(): string {
        return this._mobileNumber;
    }
    public set mobileNumber(v: string) {
        this._mobileNumber = v;
    }

    private _emailAddress!: string;
    public get emailAddress(): string {
        return this._emailAddress;
    }
    public set emailAddress(v: string) {
        this._emailAddress = v;
    }
}