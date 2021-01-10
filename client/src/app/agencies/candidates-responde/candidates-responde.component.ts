import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AgencyService } from '../agency.service';
import { IJobrequest } from 'src/app/shared/models/JobRequest';
import { ICandidateResponded } from 'src/app/shared/models/candidateToInvite';
@Component({
  selector: 'app-candidates-responde',
  templateUrl: './candidates-responde.component.html',
  styleUrls: ['./candidates-responde.component.css']
})
export class CandidatesRespondeComponent implements OnInit {
  jobRequest: IJobrequest;
  candidateResponded: ICandidateResponded[] = [];
  constructor(
    private route: ActivatedRoute,
    private agencyService: AgencyService,
    private router: Router) { }

  ngOnInit(): void {
    this.agencyService.getJobRequestbyId(+this.route.snapshot.paramMap.get('id'))
    .subscribe(dataResult => {
      this.jobRequest = dataResult;
      this.getCandidateResponded();
    });
  }
  getCandidateResponded(){
    this.agencyService.getCanditestResponded(+this.route.snapshot.paramMap.get('id')).subscribe(results => {
      this.candidateResponded = results;
      console.log(this.candidateResponded);
    });
  }

}
