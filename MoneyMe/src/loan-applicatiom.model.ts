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

    private _amountRequired: string = "";
    public get amountRequired(): string {
        return this._amountRequired;
    }
    public set amountRequired(v: string) {
        this._amountRequired = v;
    }

    private _terms: number = 0;
    public get terms(): number {
        return this._terms;
    }
    public set terms(v: number) {
        this._terms = v;
    }

    private _monthly: number = 0;
    public get monthly(): number {
        return this._monthly;
    }
    public set monthly(v: number) {
        this._monthly = v;
    }

    private _fee: number = 0;
    public get fee(): number {
        return this._fee;
    }
    public set fee(v: number) {
        this._fee = v;
    }

    private _productId: string = "";
    public get productId(): string {
        return this._productId;
    }
    public set productId(v: string) {
        this._productId = v;
    }
}