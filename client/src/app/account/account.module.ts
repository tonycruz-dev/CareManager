import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing.module';
import { SignupComponent } from './signup/signup.component';
import { SigninComponent } from './signin/signin.component';
import { SharedModule } from '../shared/shared.module';
import { AgencyRegisterComponent } from './agency-register/agency-register.component';
import { CandidateRegisterComponent } from './candidate-register/candidate-register.component';


@NgModule({
  declarations: [SignupComponent, SigninComponent, AgencyRegisterComponent, CandidateRegisterComponent],
  imports: [
    CommonModule,
    AccountRoutingModule,
    SharedModule
  ]
})
export class AccountModule { }
