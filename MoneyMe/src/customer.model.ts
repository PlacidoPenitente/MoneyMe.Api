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

    private _mobile!: string;
    public get mobile(): string {
        return this._mobile;
    }
    public set mobile(v: string) {
        this._mobile = v;
    }

    private _email!: string;
    public get email(): string {
        return this._email;
    }
    public set email(v: string) {
        this._email = v;
    }
}