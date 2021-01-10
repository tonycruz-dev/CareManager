import { Component, OnInit, Input, Output, EventEmitter, AfterViewInit } from '@angular/core';
import { JobConformed } from '../../models/JobConformed';
import { CandidateService } from 'src/app/candidates/candidates.service';

@Component({
  selector: 'app-jobs-status',
  templateUrl: './jobs-status.component.html',
  styleUrls: ['./jobs-status.component.css']
})
export class JobsStatusComponent implements OnInit, AfterViewInit {

  dataSource: JobConformed[];
  itemPerPage = 5;
  pageCount = 0;
  displaySource: JobConformed[];
  pageStart = 0;
  currentPage = 0;

    constructor(
    private candidatesServices: CandidateService) { }

  ngAfterViewInit(): void {
    this.candidatesServices.getJobConformed().subscribe();
  }

  ngOnInit(): void {
    this.candidatesServices.jobconformed.subscribe(conformeJobs => {
      // tslint:disable-next-line:no-debugger
     // debugger;
      this.dataSource = conformeJobs;
      this.displaySource = this.dataSource.slice(0, 0 + 5);
      this.pageCount = Math.ceil(this.dataSource.length / 5) || 1;
    // this.paginate(0);
  });


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
