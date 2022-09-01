import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { QuoteCalculatorComponent } from './quote-calculator/quote-calculator.component';
import { AppComponent } from './app.component';
import { LoanApplicationComponent } from './loan-application/loan-application.component';

const routes: Routes = [
  { path: 'quote/:encryptedQuoteUrl', component: QuoteCalculatorComponent },
  { path: 'apply/:id', component: LoanApplicationComponent },
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ]
})
export class AppRoutingModule { }
