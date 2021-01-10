import { Component, OnInit } from '@angular/core';
import { CandidateJobInvite } from '../../models/candidate-jobs-invite';
import { Observable } from 'rxjs';
import { ManageCandidateService } from '../../manage-candidate.service';
import { map } from 'rxjs/operators';
import { HrManagerService } from 'src/app/hrmanager/hr-manager.service';

@Component({
  selector: 'app-hr-jobs-request',
  templateUrl: './hr-jobs-request.component.html',
  styleUrls: ['./hr-jobs-request.component.css']
})
export class HrJobsRequestComponent implements OnInit {
  dataSource: CandidateJobInvite[];
  candidateJobInvite$: Observable<CandidateJobInvite[]>;


  displaySource: CandidateJobInvite[];
  itemPerPage = 5;
  pageCount = 0;
  pageStart = 0;
  currentPage = 0;
  constructor(
    private manageCandidateService: ManageCandidateService,
    private hrManagerService: HrManagerService) { }

  ngOnInit(): void {
   this.candidateJobInvite$ = this.manageCandidateService.candidateJobRequestBS$
   .pipe (
     map(dataSrc => {
      this.dataSource = dataSrc;
      this.displaySource = this.dataSource.slice(0, 0 + 5);
      this.pageCount = Math.ceil(this.dataSource.length / 5) || 1;
      return dataSrc;
    }));
   this.candidateJobInvite$.subscribe();
  }
  paginate = (page: number) =>  {
    const size = this.itemPerPage;
    const start = page * size;
    const nextPage = this.dataSource.slice(start, start + size);
    this.displaySource = nextPage;
    this.currentPage = page;
  }
  onAcceptJob(job: CandidateJobInvite) {

    this.hrManagerService.acceptJob(job.id).subscribe(() => {
      job.accept = true;
      job.reject = false;
      this.JobRequestStates(job);
      this.onRefreshJobStatus();
    });
  }
  onRefreshJobStatus() {
   // this.hrManagerService.getJobConformed().subscribe();
  }
  onRejectJob(job: CandidateJobInvite) {
    this.hrManagerService.rejectJob(job.id).subscribe(() => {
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

}
