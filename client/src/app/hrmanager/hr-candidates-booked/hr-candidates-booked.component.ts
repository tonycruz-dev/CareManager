import { Component, OnInit } from '@angular/core';
import { IJobrequest } from '../../shared/models/JobRequest';
import { ActivatedRoute } from '@angular/router';
import { HrManagerService } from '../hr-manager.service';
import { ICandidateBooked } from '../../shared/models/CandidateBooked';

@Component({
  selector: 'app-hr-candidates-booked',
  templateUrl: './hr-candidates-booked.component.html',
  styleUrls: ['./hr-candidates-booked.component.css']
})
export class HrCandidatesBookedComponent implements OnInit {

  jobRequest: IJobrequest;
  candidateBooked: ICandidateBooked[] = [];
  agencyId: number;
  constructor(
    private route: ActivatedRoute,
    private hrservice: HrManagerService) { }


  ngOnInit(): void {
    this.agencyId = this.hrservice.agency.id;
    this.hrservice.getJobRequestbyId(+this.route.snapshot.paramMap.get('id'))
    .subscribe(dataResult => {
      this.jobRequest = dataResult;
      this.getCandidateBooked();
    });
  }
  getCandidateBooked(){
    this.hrservice.getCanditestBooked(+this.route.snapshot.paramMap.get('id')).subscribe(results => {
      this.candidateBooked = results;
      console.log(this.candidateBooked);
    });
  }

}
