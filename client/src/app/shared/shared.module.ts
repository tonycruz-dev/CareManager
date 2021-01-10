import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import {BsDropdownModule} from 'ngx-bootstrap/dropdown';
import { RatingModule } from 'ngx-bootstrap/rating';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { TextInputComponent } from './components/text-input/text-input.component';
import { JobsStatusComponent } from './components/jobs-status/jobs-status.component';
import { JobsRequestComponent } from './components/jobs-request/jobs-request.component';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { ModalModule } from 'ngx-bootstrap/modal';
import { JobsFinishComponent } from './components/jobs-finish/jobs-finish.component';
import { HrJobsComponent } from './components/hr-jobs/hr-jobs.component';
import { HrJobsRequestComponent } from './components/hr-jobs-request/hr-jobs-request.component';
import { HrJobsStatusComponent } from './components/hr-jobs-status/hr-jobs-status.component';
import { PdfViewerModule } from 'ng2-pdf-viewer';

@NgModule({
  declarations: [
    TextInputComponent,
    JobsStatusComponent,
    JobsRequestComponent,
    JobsFinishComponent,
    HrJobsComponent,
    HrJobsRequestComponent,
    HrJobsStatusComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgxDropzoneModule,
    PdfViewerModule,
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    AccordionModule.forRoot(),
    RatingModule.forRoot(),
    ModalModule.forRoot(),
    SweetAlert2Module.forRoot()
  ],
  exports: [
    ReactiveFormsModule,
    FormsModule,
    TextInputComponent,
    JobsStatusComponent,
    JobsRequestComponent,
    JobsFinishComponent,
    HrJobsComponent,
    HrJobsRequestComponent,
    HrJobsStatusComponent,
    PdfViewerModule,
    BsDropdownModule,
    BsDatepickerModule,
    NgxDropzoneModule,
    AccordionModule,
    RatingModule,
    SweetAlert2Module,
    ModalModule]
})
export class SharedModule { }
