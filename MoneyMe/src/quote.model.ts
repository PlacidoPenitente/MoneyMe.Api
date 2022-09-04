export class Quote {
    private loanAmount!: number;
    public get LoanAmount(): number {
        return this.loanAmount;
    }
    public set LoanAmount(v: number) {
        this.loanAmount = v;
    }

    private term!: number;
    public get Term(): number {
        return this.term;
    }
    public set Term(v: number) {
        this.term = v;
    }

    private title!: string;
    public get Title(): string {
        return this.title;
    }
    public set Title(v: string) {
        this.title = v;
    }

    private firstName!: string;
    public get FirstName(): string {
        return this.firstName;
    }
    public set FirstName(v: string) {
        this.firstName = v;
    }

    private lastName!: string;
    public get LastName(): string {
        return this.lastName;
    }
    public set LastName(v: string) {
        this.lastName = v;
    }

    private dateOfBirth!: Date;
    public get DateOfBirth(): Date {
        return this.dateOfBirth;
    }
    public set DateOfBirth(v: Date) {
        this.dateOfBirth = v;
    }

    private mobileNumber!: string;
    public get MobileNumber(): string {
        return this.mobileNumber;
    }
    public set MobileNumber(v: string) {
        this.mobileNumber = v;
    }

    private emailAddress!: string;
    public get EmailAddress(): string {
        return this.emailAddress;
    }
    public set EmailAddress(v: string) {
        this.emailAddress = v;
    }
}