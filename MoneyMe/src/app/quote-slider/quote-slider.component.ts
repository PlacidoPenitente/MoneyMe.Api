import { Component, Input, OnInit } from '@angular/core';
import { MatSliderChange } from '@angular/material/slider';

@Component({
  selector: 'app-quote-slider',
  templateUrl: './quote-slider.component.html',
  styleUrls: ['./quote-slider.component.css']
})
export class QuoteSliderComponent implements OnInit {

  private _position: string = "16px";
  @Input()
  public get position(): string {
    return this._position;
  }
  public set position(v: string) {
    this._position = v;
  }

  private _current!: number;
  public get current(): number {
    return this._current;
  }
  public set current(v: number) {
    this._current = v;
  }

  private _maximum!: number;
  @Input()
  public get maximum(): number {
    return this._maximum;
  }
  public set maximum(v: number) {
    this._maximum = v;
  }

  private _minimum!: number;
  @Input()
  public get minimum(): number {
    return this._minimum;
  }
  public set minimum(v: number) {
    this._minimum = v;
  }

  private _title!: string;
  @Input()
  public get title(): string {
    return this._title;
  }
  public set title(v: string) {
    this._title = v;
  }

  constructor() { }

  ngOnInit(): void {
    this.position = ((((1) / 100) * 316) + 20) + "px";
    this.current = (((1) / 100) * (this.maximum));
  }

  onInputChange(event: MatSliderChange) {
    this.position = ((((event.value ?? 0) / 100) * 316) + 20) + "px";
    this.current = (((event.value ?? 0) / 100) * (this.maximum));
  }
}
