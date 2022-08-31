export class PartialQuote {
    private _customerId: string = "";
    public get customerId(): string {
        return this._customerId;
    }
    public set customerId(v: string) {
        this._customerId = v;
    }

    private _amountRequired: number = 0;
    public get amountRequired(): number {
        return this._amountRequired;
    }
    public set amountRequired(v: number) {
        this._amountRequired = v;
    }

    private _productId: string = "";
    public get productId(): string {
        return this._productId;
    }
    public set productId(v: string) {
        this._productId = v;
    }

    private _term: number = 0;
    public get term(): number {
        return this._term;
    }
    public set term(v: number) {
        this._term = v;
    }
}