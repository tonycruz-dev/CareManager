import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CandidateHomeComponent } from './candidate-home/candidate-home.component';
import { UploadPhotoComponent } from './upload-photo/upload-photo.component';
import { UploadDocumentComponent } from './upload-document/upload-document.component';
import { DocumentViewComponent } from './document-view/document-view.component';


const routes: Routes = [
  {path: '', component: CandidateHomeComponent},
  {path: 'photo', component: UploadPhotoComponent},
  {path: 'document', component: UploadDocumentComponent},
  {path: 'document-view/:id', component: DocumentViewComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CandidatesRoutingModule { }
