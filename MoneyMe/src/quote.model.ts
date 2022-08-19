export class Quote {
    private amountRequired!: number;
    public get AmountRequired(): number {
        return this.amountRequired;
    }
    public set AmountRequired(v: number) {
        this.amountRequired = v;
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

    private mobile!: string;
    public get Mobile(): string {
        return this.mobile;
    }
    public set Mobile(v: string) {
        this.mobile = v;
    }

    private email!: string;
    public get Email(): string {
        return this.email;
    }
    public set Email(v: string) {
        this.email = v;
    }
}