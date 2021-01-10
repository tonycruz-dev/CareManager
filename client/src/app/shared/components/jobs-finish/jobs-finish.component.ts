import { Component, OnInit, AfterViewInit } from '@angular/core';
import { JobConformed } from '../../models/JobConformed';
import { CandidateService } from '../../../candidates/candidates.service';
import { Observable } from 'rxjs';
import { IJobFinish } from '../../models/JobFinish';

@Component({
  selector: 'app-jobs-finish',
  templateUrl: './jobs-finish.component.html',
  styleUrls: ['./jobs-finish.component.css']
})
export class JobsFinishComponent implements OnInit, AfterViewInit {
  dataSource: IJobFinish[];
  candidateFinish$: Observable<IJobFinish[]>;
  itemPerPage = 5;
  pageCount = 0;
  displaySource: IJobFinish[];
  pageStart = 0;
  currentPage = 0;

  constructor(private candidatesServices: CandidateService) { }

  ngAfterViewInit(): void {

    this.candidatesServices.getJobFinish().subscribe(finish => {
      this.dataSource = finish;
      this.displaySource = this.dataSource.slice(0, 0 + 5);
      this.pageCount = Math.ceil(this.dataSource.length / 5) || 1;
    });
  }

  ngOnInit(): void {
    this.candidateFinish$ = this.candidatesServices.candidateFinish$;
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
