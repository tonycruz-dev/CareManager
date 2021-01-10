import { Component, OnInit } from '@angular/core';
import { HrManagerService } from '../hr-manager.service';
import { ActivatedRoute,  Router } from '@angular/router';
import { ManageCandidateService } from 'src/app/shared/manage-candidate.service';
import { IJobFinish } from '../../shared/models/JobFinish';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CandidateJobInvite } from '../../shared/models/candidate-jobs-invite';
import { JobConformed } from '../../shared/models/JobConformed';
import { ICandidate } from 'src/app/shared/models/Candidate';

@Component({
  selector: 'app-hr-candidate-home',
  templateUrl: './hr-candidate-home.component.html',
  styleUrls: ['./hr-candidate-home.component.css']
})
export class HrCandidateHomeComponent implements OnInit {
  candidateId: string;
  listFishJobs: IJobFinish[];
  candidateFinish$: Observable<IJobFinish[]>;
  candidatejobRequest$: Observable<CandidateJobInvite[]>;
  candidateJobConformed$: Observable<JobConformed[]>;
  candidate$: Observable<ICandidate>;

  constructor(
    private hrmanagerService: HrManagerService,
    private managecandidateService: ManageCandidateService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    // tslint:disable-next-line:no-debugger
    // debugger;
    this.candidateId = this.route.snapshot.paramMap.get('id');
    this.candidate$ = this.hrmanagerService.getCandidate(+ this.candidateId);

    this.candidateFinish$ = this.hrmanagerService.getJobFinish(+ this.candidateId)
    .pipe(
      map(jobFinish => {
       this.listFishJobs =  jobFinish;
       this.managecandidateService.setJobFinish(this.listFishJobs);
       return jobFinish;
      })
    );
    this.candidatejobRequest$ = this.hrmanagerService.getListJobs(+ this.candidateId)
    .pipe(
      map(jobrequest => {
         this.managecandidateService.setJobRequest(jobrequest);
         return jobrequest;
      })
    );
    this.candidateJobConformed$ = this.hrmanagerService.getJobConformed(+ this.candidateId)
    .pipe(
      map(jobconformed => {
         this.managecandidateService.setJobConformed(jobconformed);
         return jobconformed;
      })
    );
    this.candidate$.subscribe();
    this.candidateFinish$.subscribe();
    this.candidatejobRequest$.subscribe();
    this.candidateJobConformed$.subscribe();

  }

}
