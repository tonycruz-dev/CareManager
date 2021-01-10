import { Component, OnInit } from '@angular/core';
import { ManageCandidateService } from '../../manage-candidate.service';
import { JobConformed } from '../../models/JobConformed';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-hr-jobs-status',
  templateUrl: './hr-jobs-status.component.html',
  styleUrls: ['./hr-jobs-status.component.css']
})
export class HrJobsStatusComponent implements OnInit {

  candidateJobConformed$: Observable<JobConformed[]>;

  dataSource: JobConformed[];
  displaySource: JobConformed[];
  itemPerPage = 5;
  pageCount = 0;
  pageStart = 0;
  currentPage = 0;

  constructor(
    private manageCandidateService: ManageCandidateService) { }

  ngOnInit(): void {
    this.candidateJobConformed$ = this.manageCandidateService.candidateJobConformedBS$
   .pipe (
     map(dataSrc => {
      this.dataSource = dataSrc;
      this.displaySource = this.dataSource.slice(0, 0 + 5);
      this.pageCount = Math.ceil(this.dataSource.length / 5) || 1;
      return dataSrc;
    }));
    this.candidateJobConformed$.subscribe();
  }
  paginate = (page: number) =>  {
    const size = this.itemPerPage;
    const start = page * size;
    const nextPage = this.dataSource.slice(start, start + size);
    this.displaySource = nextPage;
    this.currentPage = page;
  }
  onCancelJob(job: JobConformed) {
    console.log(job);
  }

}
