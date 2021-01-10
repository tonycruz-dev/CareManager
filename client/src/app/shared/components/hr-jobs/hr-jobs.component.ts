import { Component, OnInit } from '@angular/core';
import { ManageCandidateService } from '../../manage-candidate.service';
import { JobConformed } from '../../models/JobConformed';
import { Observable } from 'rxjs';
import { IJobFinish } from '../../models/JobFinish';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-hr-jobs',
  templateUrl: './hr-jobs.component.html',
  styleUrls: ['./hr-jobs.component.css']
})
export class HrJobsComponent implements OnInit {
  dataSource: IJobFinish[];
  candidateFinish$: Observable<IJobFinish[]>;
  displaySource: IJobFinish[];
  itemPerPage = 5;
  pageCount = 0;
  pageStart = 0;
  currentPage = 0;


  constructor(private manageCandidateService: ManageCandidateService) { }

  ngOnInit(): void {
   this.candidateFinish$ = this.manageCandidateService.candidateFinish$
   .pipe (
     map(dataSrc => {
      this.dataSource = dataSrc;
      this.displaySource = this.dataSource.slice(0, 0 + 5);
      this.pageCount = Math.ceil(this.dataSource.length / 5) || 1;
      return dataSrc;
    }));
   this.candidateFinish$.subscribe();
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
