import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CandidatesRoutingModule } from './candidates-routing.module';
import { HomeComponent } from './home/home.component';
import { CandidateHomeComponent } from './candidate-home/candidate-home.component';
import { SharedModule } from '../shared/shared.module';
import { UploadPhotoComponent } from './upload-photo/upload-photo.component';
import { UploadDocumentComponent } from './upload-document/upload-document.component';
import { DocumentViewComponent } from './document-view/document-view.component';


@NgModule({
  declarations: [HomeComponent, CandidateHomeComponent, UploadPhotoComponent, UploadDocumentComponent, DocumentViewComponent],
  imports: [
    CommonModule,
    CandidatesRoutingModule,
    SharedModule
  ]
})
export class CandidatesModule { }
