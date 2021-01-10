import { HrAddJobRequestComponent } from './hr-add-job-request/hr-add-job-request.component';
import { HrJobRequestComponent } from './hr-job-request/hr-job-request.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HrHomeComponent } from './hr-home/hr-home.component';
import { HrManageProfilePhotoComponent } from './hr-manage-profile-photo/hr-manage-profile-photo.component';
import { HrManageProfileDocumentsComponent } from './hr-manage-profile-documents/hr-manage-profile-documents.component';
import { HrCandidatesComponent } from './hr-candidates/hr-candidates.component';
import { HrAgenciesComponent } from './hr-agencies/hr-agencies.component';
import { HrInviteCandidatesComponent } from './hr-invite-candidates/hr-invite-candidates.component';
import { HrCandidatesBookedComponent } from './hr-candidates-booked/hr-candidates-booked.component';
import { HrCandidatesInProgressComponent } from './hr-candidates-in-progress/hr-candidates-in-progress.component';
import { HrCandidatesRespondeComponent } from './hr-candidates-responde/hr-candidates-responde.component';
import { HrJobRequestDetailComponent } from './hr-job-request-detail/hr-job-request-detail.component';
import { HrListLocationComponent } from './hr-list-location/hr-list-location.component';
import { HrCandidateHomeComponent } from './hr-candidate-home/hr-candidate-home.component';
import { CandidateDocumentViewComponent } from './candidate-document-view/candidate-document-view.component';


const routes: Routes = [
  {path: '', component: HrHomeComponent},
  {path: 'candidates', component: HrCandidatesComponent},
  {path: 'agencies', component: HrAgenciesComponent},
  {path: 'hr-job-request/:id', component: HrJobRequestComponent},
  {path: 'hr-addjob/:id', component: HrAddJobRequestComponent},
  {path: 'hr-invite/:id', component: HrInviteCandidatesComponent},
  {path: 'hr-booked/:id', component: HrCandidatesBookedComponent},
  {path: 'hr-inprogress/:id', component: HrCandidatesInProgressComponent},
  {path: 'hr-responded/:id', component: HrCandidatesRespondeComponent},
  {path: 'hr-candidatefinish/:id', component: HrJobRequestDetailComponent},
  // {path: 'finish/:id', component: CandidatesRespondeComponent},
  // {path: 'location-add', component: AddLocationComponent},
  {path: 'hr-location-list/:id', component: HrListLocationComponent},
  {path: 'hr-profile-photo', component: HrManageProfilePhotoComponent},
  {path: 'hr-candidate-home/:id', component: HrCandidateHomeComponent},
  {path: 'hr-photo/:id', component: HrManageProfilePhotoComponent},
  {path: 'hr-document/:id', component: HrManageProfileDocumentsComponent},
  {path: 'hr-document-view/:id', component: CandidateDocumentViewComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HrmanagerRoutingModule { }
