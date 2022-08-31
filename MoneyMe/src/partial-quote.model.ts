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

    private _terms: number = 0;
    public get terms(): number {
        return this._terms;
    }
    public set terms(v: number) {
        this._terms = v;
    }
}