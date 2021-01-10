import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HrmanagerRoutingModule } from './hrmanager-routing.module';
import { HrHomeComponent } from './hr-home/hr-home.component';
import { HrAddJobRequestComponent } from './hr-add-job-request/hr-add-job-request.component';
import { HrAddLocationComponent } from './hr-add-location/hr-add-location.component';
import { HrCandidatesBookedComponent } from './hr-candidates-booked/hr-candidates-booked.component';
import { HrCandidatesInProgressComponent } from './hr-candidates-in-progress/hr-candidates-in-progress.component';
import { HrCandidatesRespondeComponent } from './hr-candidates-responde/hr-candidates-responde.component';
import { HrInviteCandidatesComponent } from './hr-invite-candidates/hr-invite-candidates.component';
import { HrJobRequestComponent } from './hr-job-request/hr-job-request.component';
import { HrJobRequestDetailComponent } from './hr-job-request-detail/hr-job-request-detail.component';
import { HrListLocationComponent } from './hr-list-location/hr-list-location.component';
import { HrManageProfileDocumentsComponent } from './hr-manage-profile-documents/hr-manage-profile-documents.component';
import { HrManageProfilePhotoComponent } from './hr-manage-profile-photo/hr-manage-profile-photo.component';
import { SharedModule } from '../shared/shared.module';
import { HrAgenciesComponent } from './hr-agencies/hr-agencies.component';
import { HrCandidatesComponent } from './hr-candidates/hr-candidates.component';
import { HrCandidateHomeComponent } from './hr-candidate-home/hr-candidate-home.component';
import { CandidateDocumentViewComponent } from './candidate-document-view/candidate-document-view.component';


@NgModule({
  declarations: [
    HrHomeComponent,
    HrAddJobRequestComponent,
    HrAddLocationComponent,
    HrCandidatesBookedComponent,
    HrCandidatesInProgressComponent,
    HrCandidatesRespondeComponent,
    HrInviteCandidatesComponent,
    HrJobRequestComponent,
    HrJobRequestDetailComponent,
    HrListLocationComponent,
    HrManageProfileDocumentsComponent,
    HrManageProfilePhotoComponent,
    HrAgenciesComponent,
    HrCandidatesComponent,
    HrCandidateHomeComponent,
    CandidateDocumentViewComponent],
  imports: [
    CommonModule,
    SharedModule,
    HrmanagerRoutingModule
  ]
})
export class HrmanagerModule { }
