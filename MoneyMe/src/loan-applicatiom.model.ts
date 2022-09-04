export class LoanApplication {

    private _id: string = "";
    public get id(): string {
        return this._id;
    }
    public set id(v: string) {
        this._id = v;
    }

    private _customerId: string = "";
    public get customerId(): string {
        return this._customerId;
    }
    public set customerId(v: string) {
        this._customerId = v;
    }

    private _loanAmount: number = 0;
    public get loanAmount(): number {
        return this._loanAmount;
    }
    public set loanAmount(v: number) {
        this._loanAmount = v;
    }

    private _term: number = 0;
    public get term(): number {
        return this._term;
    }
    public set term(v: number) {
        this._term = v;
    }

    private _monthlyPayment: number = 0;
    public get monthlyPayment(): number {
        return this._monthlyPayment;
    }
    public set monthlyPayment(v: number) {
        this._monthlyPayment = v;
    }

    private _fee: number = 0;
    public get fee(): number {
        return this._fee;
    }
    public set fee(v: number) {
        this._fee = v;
    }

    private _interest: number = 0;
    public get interest(): number {
        return this._interest;
    }
    public set interest(v: number) {
        this._interest = v;
    }

    private _productId: string = "";
    public get productId(): string {
        return this._productId;
    }
    public set productId(v: string) {
        this._productId = v;
    }
}