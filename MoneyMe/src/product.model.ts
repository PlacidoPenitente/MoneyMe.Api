export class Product {
    private _id!: string;
    public get id(): string {
        return this._id;
    }
    public set id(v: string) {
        this._id = v;
    }

    private _name!: string;
    public get name(): string {
        return this._name;
    }
    public set name(v: string) {
        this._name = v;
    }

    private _minimumDuration!: number;
    public get minimumDuration(): number {
        return this._minimumDuration;
    }
    public set minimumDuration(v: number) {
        this._minimumDuration = v;
    }

    private _maximumDuration!: number;
    public get maximumDuration(): number {
        return this._maximumDuration;
    }
    public set maximumDuration(v: number) {
        this._maximumDuration = v;
    }
}