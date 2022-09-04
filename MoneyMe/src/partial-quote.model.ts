export class PartialQuote {
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