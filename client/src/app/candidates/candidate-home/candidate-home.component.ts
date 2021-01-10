import { Component, OnInit, AfterViewInit } from '@angular/core';
import { CandidateService } from '../candidates.service';
import { CandidateJobInvite } from 'src/app/shared/models/candidate-jobs-invite';
import { AccountService } from 'src/app/account/account.service';
import { JobConformed } from 'src/app/shared/models/JobConformed';

@Component({
  selector: 'app-candidate-home',
  templateUrl: './candidate-home.component.html',
  styleUrls: ['./candidate-home.component.css']
})
export class CandidateHomeComponent implements OnInit, AfterViewInit {
  // jobList: CandidateJobInvite[] = [];
  // jobRequestDataSouce: CandidateJobInvite[] = [];
  // jobRequestDisplay: CandidateJobInvite[] = [];
 // jobListFinish: CandidateJobInvite[] = [];
 // jobListInProgress: CandidateJobInvite[] = [];


  dataSourceJobConf: JobConformed[];
  displaySourceJobConf: JobConformed[];
  pageStart = 0;
  currentPage = 0;
  itemPerPage = 5;
  pageCount = 0;
  pageCountJobsRequest = 0;


  conformedJobs: JobConformed[] = [];
  conformedJobsShow: JobConformed[] = [];



  constructor(
    private candidatesServices: CandidateService) { }

  ngOnInit(): void {
     this.candidatesServices.jobRequest.subscribe(listJobs => {
       // tslint:disable-next-line:no-debugger
      // debugger;
      // this.jobList = listJobs;
       // this.jobRequestDataSouce = listJobs;
      // this.jobRequestDisplay = this.jobRequestDataSouce.slice(0, 0 + 5);
      // this.pageCountJobsRequest = Math.ceil(this.jobRequestDataSouce.length / 5) || 1;

    });
     this.candidatesServices.jobconformed.subscribe(conformeJobs => {
        // tslint:disable-next-line:no-debugger
       // debugger;
        this.dataSourceJobConf = conformeJobs;
        this.displaySourceJobConf = this.dataSourceJobConf.slice(0, 0 + 5);
        this.pageCount = Math.ceil(conformeJobs.length / 5) || 1;
      // this.paginate(0);
    });
  }
  ngAfterViewInit(): void {
    this.loadJobRequest();
  }
  loadJobRequest() {
    this.candidatesServices.getListJobs().subscribe();
    this.candidatesServices.getJobConformed().subscribe();
  }
  onCancelJob(item) {
    console.log(item);
  }
  // onAcceptJob(item: CandidateJobInvite) {
  //  this.candidatesServices.acceptJob(item.id).subscribe(() => {
  //   const updateJobConf =  this.jobRequestDataSouce.find(j => j.id === item.id);
  //   updateJobConf.reject = false;
  //   updateJobConf.accept = true;
  //   item.reject = false;
  //   item.accept = true;
  //   // this.loadJobRequest();
  //  });
  // }
  // onRejectJob(item: CandidateJobInvite) {
  //   this.candidatesServices.rejectJob(item.id).subscribe(() => {
  //     const updateJobConf =  this.jobRequestDataSouce.find(j => j.id === item.id);
  //     updateJobConf.reject = true;
  //     updateJobConf.accept = false;
  //     item.reject = true;
  //     item.accept = false;
  //     // this.loadJobRequest();
  //  });
 // }

}
