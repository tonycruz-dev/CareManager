import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SigninComponent } from './signin/signin.component';
import { SignupComponent } from './signup/signup.component';
import { AgencyRegisterComponent } from './agency-register/agency-register.component';
import { CandidateRegisterComponent } from './candidate-register/candidate-register.component';


const routes: Routes = [
  {path: 'signin', component: SigninComponent},
  {path: 'signup', component: SignupComponent},
  {path: 'agencyregister', component: AgencyRegisterComponent},
  {path: 'candidateregister', component: CandidateRegisterComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
