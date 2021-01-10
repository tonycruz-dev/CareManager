import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HrManagerService } from '../hr-manager.service';
import { IJobrequest } from '../../shared/models/JobRequest';
import { ICandidateResponded } from 'src/app/shared/models/candidateToInvite';

@Component({
  selector: 'app-hr-candidates-responde',
  templateUrl: './hr-candidates-responde.component.html',
  styleUrls: ['./hr-candidates-responde.component.css']
})
export class HrCandidatesRespondeComponent implements OnInit {

  jobRequest: IJobrequest;
  agencyId: number;

  candidateResponded: ICandidateResponded[] = [];
  constructor(
    private route: ActivatedRoute,
    private hrService: HrManagerService,
    private router: Router) { }

  ngOnInit(): void {
    this.agencyId = this.hrService.agency.id;
    this.hrService.getJobRequestbyId(+this.route.snapshot.paramMap.get('id'))
    .subscribe(dataResult => {
      this.jobRequest = dataResult;
      this.getCandidateResponded();
    });
  }
  getCandidateResponded(){
    this.hrService.getCanditestResponded(+this.route.snapshot.paramMap.get('id')).subscribe(results => {
      this.candidateResponded = results;
      console.log(this.candidateResponded);
    });
  }

}
