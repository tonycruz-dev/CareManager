import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MinorComponent } from './minor/minor.component';
import { UserLoginComponent } from './user-login/user-login.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'minor', component: MinorComponent},
  {path: 'login', component: UserLoginComponent},
  {path: 'account', loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule)},
  {path: 'agency', loadChildren: () => import('./agencies/agencies.module').then(mod => mod.AgenciesModule)},
  {path: 'candidate', loadChildren: () => import('./candidates/candidates.module').then(mod => mod.CandidatesModule)},
  {path: 'hr-manager', loadChildren: () => import('./hrmanager/hrmanager.module').then(mod => mod.HrmanagerModule)},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
