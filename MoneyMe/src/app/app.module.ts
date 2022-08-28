import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ThirdPartyComponent } from './third-party/third-party.component';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { MatSliderModule } from '@angular/material/slider';
import { MatCardModule } from '@angular/material/card';
import { QuoteCalculatorComponent } from './quote-calculator/quote-calculator.component';
import { QuoteSliderComponent } from './quote-slider/quote-slider.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { LoanApplicationComponent } from './loan-application/loan-application.component';
import { UserFormComponent } from './user-form/user-form.component';
import { UserInformationComponent } from './user-information/user-information.component';
import { FinanceDetailsComponent } from './finance-details/finance-details.component';

@NgModule({
  declarations: [
    AppComponent,
    ThirdPartyComponent,
    QuoteCalculatorComponent,
    QuoteSliderComponent,
    LoanApplicationComponent,
    UserFormComponent,
    UserInformationComponent,
    FinanceDetailsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CommonModule,
    FormsModule,
    MatTableModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
    MatSelectModule,
    MatRadioModule,
    MatButtonModule,
    MatSliderModule,
    MatCardModule,
    MatExpansionModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
