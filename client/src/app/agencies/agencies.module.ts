import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AgenciesRoutingModule } from './agencies-routing.module';
import { HomeComponent } from './home/home.component';
import { AgencyHomeComponent } from './agency-home/agency-home.component';
import { JobRequestComponent } from './job-request/job-request.component';
import { SharedModule } from '../shared/shared.module';
import { AddJobRequestComponent } from './add-job-request/add-job-request.component';
import { JobRequestDetailComponent } from './job-request-detail/job-request-detail.component';
import { InviteCandidatesComponent } from './invite-candidates/invite-candidates.component';
import { CandidatesInProgressComponent } from './candidates-in-progress/candidates-in-progress.component';
import { CandidatesRespondeComponent } from './candidates-responde/candidates-responde.component';
import { CandidatesBookedComponent } from './candidates-booked/candidates-booked.component';
import { ListLocationComponent } from './list-location/list-location.component';
import { AddLocationComponent } from './add-location/add-location.component';
import { EditLocationComponent } from './edit-location/edit-location.component';
import { DeleteLocationComponent } from './delete-location/delete-location.component';
import { ManageProfilePhotoComponent } from './manage-profile-photo/manage-profile-photo.component';
import { ManageProfileDocumentsComponent } from './manage-profile-documents/manage-profile-documents.component';


@NgModule({
  declarations: [
    HomeComponent,
    AgencyHomeComponent,
    JobRequestComponent,
     AddJobRequestComponent,
     JobRequestDetailComponent,
     InviteCandidatesComponent,
     CandidatesInProgressComponent,
     CandidatesRespondeComponent,
     CandidatesBookedComponent,
     ListLocationComponent,
     AddLocationComponent,
     EditLocationComponent,
     DeleteLocationComponent,
     ManageProfilePhotoComponent,
     ManageProfileDocumentsComponent],
  imports: [
    CommonModule,
    AgenciesRoutingModule,
    SharedModule
  ]
})
export class AgenciesModule { }
