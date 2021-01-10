import { Component, OnInit, Input, EventEmitter, Output, AfterViewInit } from '@angular/core';
import { CandidateJobInvite } from '../../models/candidate-jobs-invite';
import { CandidateService } from 'src/app/candidates/candidates.service';

@Component({
  selector: 'app-jobs-request',
  templateUrl: './jobs-request.component.html',
  styleUrls: ['./jobs-request.component.css']
})
export class JobsRequestComponent implements OnInit, AfterViewInit {

  dataSource: CandidateJobInvite[];
  itemPerPage = 5;
  pageCount = 0;
  displaySource: CandidateJobInvite[];
  pageStart = 0;
  currentPage = 0;
  constructor(
    private candidatesServices: CandidateService) { }

  ngOnInit(): void {
    this.candidatesServices.jobRequest.subscribe(listJobs => {

      this.dataSource = listJobs;
      this.displaySource = this.dataSource.slice(0, 0 + 5);
      this.pageCount = Math.ceil(this.dataSource.length / 5) || 1;

   });
  }
  ngAfterViewInit(): void {
    this.candidatesServices.getListJobs().subscribe();
  }

  paginate = (page: number) =>  {
    const size = this.itemPerPage;
    const start = page * size;
    const nextPage = this.dataSource.slice(start, start + size);
    this.displaySource = nextPage;
    this.currentPage = page;
  }
  onAcceptJob(job: CandidateJobInvite) {

    this.candidatesServices.acceptJob(job.id).subscribe(() => {
      job.accept = true;
      job.reject = false;
      this.JobRequestStates(job);
      this.onRefreshJobStatus();
    });
  }
  onRefreshJobStatus() {
    this.candidatesServices.getJobConformed().subscribe();
  }
  onRejectJob(job: CandidateJobInvite) {
    this.candidatesServices.rejectJob(job.id).subscribe(() => {
      job.accept = false;
      job.reject = true;
      this.JobRequestStates(job);
      this.onRefreshJobStatus();
    });
  }
  JobRequestStates(job: CandidateJobInvite): boolean {
    if (job.accept !== null) {
      return true;
    } else {
      return false;
    }
 }
 isJobRequestRespond(job: CandidateJobInvite): string {
  if (job.accept !== null) {
    if (job.accept) {
       return 'success-element ui-sortable-handle';
    } else {
      return 'danger-element ui-sortable-handle';
    }
  } else {
    return 'warning-element ui-sortable-handle';
  }
 }
 // [ngClass]='job.accept !== null || job.accept,'

}
