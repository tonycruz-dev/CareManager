import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AgencyService } from '../agency.service';
import { IJobrequest } from 'src/app/shared/models/JobRequest';
import { ICandidateToInvite } from 'src/app/shared/models/candidateToInvite';

@Component({
  selector: 'app-candidates-in-progress',
  templateUrl: './candidates-in-progress.component.html',
  styleUrls: ['./candidates-in-progress.component.css']
})
export class CandidatesInProgressComponent implements OnInit {
  jobRequest: IJobrequest;
  candidateInProgress: ICandidateToInvite[] = [];
  constructor(
    private route: ActivatedRoute,
    private agencyService: AgencyService,
    private router: Router) { }


  ngOnInit(): void {
    this.agencyService.getJobRequestbyId(+this.route.snapshot.paramMap.get('id'))
    .subscribe(dataResult => {
      this.jobRequest = dataResult;
      this.getCandidateInProgress();
    });
  }
  getCandidateInProgress(){
    this.agencyService.getCanditestInprogress(+this.route.snapshot.paramMap.get('id')).subscribe(results => {
      this.candidateInProgress = results;
      console.log(this.candidateInProgress);
    });
  }

}
