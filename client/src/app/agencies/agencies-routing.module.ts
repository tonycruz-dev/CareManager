import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AgencyHomeComponent } from './agency-home/agency-home.component';
import { JobRequestComponent } from './job-request/job-request.component';
import { AddJobRequestComponent } from './add-job-request/add-job-request.component';
import { InviteCandidatesComponent } from './invite-candidates/invite-candidates.component';
import { CandidatesBookedComponent } from './candidates-booked/candidates-booked.component';
import { CandidatesInProgressComponent } from './candidates-in-progress/candidates-in-progress.component';
import { CandidatesRespondeComponent } from './candidates-responde/candidates-responde.component';
import { JobRequestDetailComponent } from './job-request-detail/job-request-detail.component';
import { AddLocationComponent } from './add-location/add-location.component';
import { ListLocationComponent } from './list-location/list-location.component';
import { ManageProfilePhotoComponent } from './manage-profile-photo/manage-profile-photo.component';


const routes: Routes = [
  {path: '', component: AgencyHomeComponent},
  {path: 'jobs', component: JobRequestComponent},
  {path: 'addjob', component: AddJobRequestComponent},
  {path: 'invite/:id', component: InviteCandidatesComponent},
  {path: 'booked/:id', component: CandidatesBookedComponent},
  {path: 'inprogress/:id', component: CandidatesInProgressComponent},
  {path: 'responded/:id', component: CandidatesRespondeComponent},
  {path: 'candidatefinish/:id', component: JobRequestDetailComponent},
  {path: 'finish/:id', component: CandidatesRespondeComponent},
  {path: 'location-add', component: AddLocationComponent},
  {path: 'location-list', component: ListLocationComponent},
  {path: 'profile-photo', component: ManageProfilePhotoComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AgenciesRoutingModule { }
