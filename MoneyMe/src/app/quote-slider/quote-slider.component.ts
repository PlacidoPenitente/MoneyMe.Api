import { HttpClient } from '@angular/common/http';
import { AfterViewChecked, AfterViewInit, ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatSliderChange } from '@angular/material/slider';
import { ActivatedRoute } from '@angular/router';
import { PartialQuote } from 'src/partial-quote.model';
import { Product } from 'src/product.model';

@Component({
  selector: 'app-quote-slider',
  templateUrl: './quote-slider.component.html',
  styleUrls: ['./quote-slider.component.css']
})
export class QuoteSliderComponent implements OnInit, AfterViewInit {

  @ViewChild('slider') private slider!: any;

  @Output() updateSliderChange = new EventEmitter<Function>();

  private _updateSlider!: Function;
  @Input()
  public get updateSlider(): Function {
    return this._updateSlider;
  }
  public set updateSlider(v: Function) {
    this._updateSlider = v;
  }

  @Output() sliderValueChange = new EventEmitter<string>();

  private _sliderValue!: string;
  @Input()
  public get sliderValue(): string {
    return this._sliderValue;
  }
  public set sliderValue(v: string) {
    this._sliderValue = v;
  }

  private _position!: string
  @Input()
  public get position(): string {
    return this._position;
  }
  public set position(v: string) {
    this._position = v;
  }

  private _current!: string;
  public get current(): string {
    return this._current;
  }
  public set current(v: string) {
    this._current = v;
  }

  private _initial: number = 0;
  public get initial(): number {
    return this._initial;
  }
  public set initial(v: number) {
    this._initial = v;
  }

  @Output() currentValueChange = new EventEmitter<number>();

  private _currentValue!: number;
  @Input()
  public get currentValue(): number {
    return this._currentValue;
  }
  public set currentValue(v: number) {
    this._currentValue = v;
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

  private _minimumPercentage!: number;
  public get minimumPercentage(): number {
    return this._minimumPercentage;
  }
  public set minimumPercentage(v: number) {
    this._minimumPercentage = v;
  }

  private _maximumPercentage!: number;
  public get maximumPercentage(): number {
    return this._maximumPercentage;
  }
  public set maximumPercentage(v: number) {
    this._maximumPercentage = v;
  }

  private _step!: number;
  public get step(): number {
    return this._step;
  }
  public set step(v: number) {
    this._step = v;
  }

  private _title!: string;
  @Input()
  public get title(): string {
    return this._title;
  }
  public set title(v: string) {
    this._title = v;
  }

  private _format!: Function;
  @Input()
  public get format(): Function {
    return this._format;
  }
  public set format(v: Function) {
    this._format = v;
  }

  private _factor!: number;
  public get factor(): number {
    return this._factor;
  }
  public set factor(v: number) {
    this._factor = v;
  }

  private _product!: Product;
  @Input()
  public get product(): Product {
    return this._product;
  }
  public set product(v: Product) {
    this._product = v;
  }

  private _partialQuote!: PartialQuote;
  @Input()
  public get partialQuote(): PartialQuote {
    return this._partialQuote;
  }
  public set partialQuote(v: PartialQuote) {
    this._partialQuote = v;
  }

  constructor(private httpCient: HttpClient, private activatedRoute: ActivatedRoute, private changeDetectorRef: ChangeDetectorRef) {
  }

  ngAfterViewInit(): void {
    var initialPercentage = ((this.partialQuote.loanAmount - (Math.round((this.minimum) / this.factor) * this.factor)) / this.factor);
    this.initial = initialPercentage;
    this.updateSliderValue(this.initial, this.slider.nativeElement.offsetWidth);

    this.updateSliderChange.emit((x: number, y: string, min: number, max: number) => {
      if (x < min) {
        x = min;
      }

      if (min != null && max != null) {
        this.minimum = min;
        this.maximum = max;
        this.minimumPercentage = this.minimum / this.maximum;

        this.factor = 100;

        if (this.maximum < 100) {
          this.factor = 1
        }

        this.maximumPercentage = ((this.maximum - this.minimum) / this.factor);
      }

      var z = ((x - (Math.round((min ?? this.minimum) / this.factor) * this.factor)) / this.factor);
      this.initial = z;
      this.updateSliderValue(z, y);
    });

    this.sliderValueChange.emit(this.slider.nativeElement.offsetWidth);
    this.changeDetectorRef.detectChanges();
  }

  ngOnInit(): void {
    this.minimumPercentage = this.minimum / this.maximum;

    this.factor = 100;

    if (this.maximum < 100) {
      this.factor = 1
    }

    this.maximumPercentage = ((this.maximum - this.minimum) / this.factor);
  }

  onInputChange(event: MatSliderChange) {
    this.updateSliderValue(event.value ?? 0, this.slider.nativeElement.offsetWidth);
  }

  updateSliderValue(value: number, parentWidth: string) {
    var width = Number(parentWidth) + 48;
    var maxWidth = Number(parentWidth) + 48 - 82 + 17.5;
    var padding = maxWidth * .1;
    var labelPosition = ((value / this.maximumPercentage) * (maxWidth - padding)) - 18.5 + (padding / 2);

    if (labelPosition < 0) {
      labelPosition = 0;
    }
    else if (labelPosition + 100 > width) {
      labelPosition = width - 100;
    }

    this.position = labelPosition + "px";

    var actualValue = ((value / this.maximumPercentage) * (this.maximum - this.minimum));

    if (actualValue < 1) {
      this.currentValueChange.emit(this.minimum);
      this.current = this.format(this.minimum);
    }
    else if ((value / this.maximumPercentage) == 1) {
      this.step = this.maximumPercentage - Math.trunc(this.maximumPercentage);

      if (this.step < 1) {
        this.step = 1;
      }
      this.currentValueChange.emit(this.maximum);
      this.current = this.format(this.maximum);
    }
    else {
      this.step = 1;
      this.currentValueChange.emit(this.currentValue);
      this.currentValue = actualValue + Math.round((this.minimum) / this.factor) * this.factor;
      this.current = this.format(this.currentValue);
    }
  }
}
