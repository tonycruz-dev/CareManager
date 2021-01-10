import { Component, OnInit } from '@angular/core';
import { HrManagerService } from '../hr-manager.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IJobrequest } from '../../shared/models/JobRequest';
import { ICandidateToInvite } from '../../shared/models/candidateToInvite';

@Component({
  selector: 'app-hr-candidates-in-progress',
  templateUrl: './hr-candidates-in-progress.component.html',
  styleUrls: ['./hr-candidates-in-progress.component.css']
})
export class HrCandidatesInProgressComponent implements OnInit {
  jobRequest: IJobrequest;
  candidateInProgress: ICandidateToInvite[] = [];
  agencyId: number;
  constructor(
    private hrservices: HrManagerService,
    private route: ActivatedRoute,
    private router: Router) { }

    ngOnInit(): void {
      this.agencyId = this.hrservices.agency.id;
      this.hrservices.getJobRequestbyId(+this.route.snapshot.paramMap.get('id'))
      .subscribe(dataResult => {
        this.jobRequest = dataResult;
        this.getCandidateInProgress();
      });
    }
    getCandidateInProgress(){
      this.hrservices.getCanditestInprogress(+this.route.snapshot.paramMap.get('id')).subscribe(results => {
        this.candidateInProgress = results;
        console.log(this.candidateInProgress);
      });
    }

}
