import { Component, OnInit } from '@angular/core';
import { IJobrequest } from '../../shared/models/JobRequest';
import { ActivatedRoute } from '@angular/router';
import { AgencyService } from '../agency.service';
import { ICandidateBooked } from '../../shared/models/CandidateBooked';

@Component({
  selector: 'app-candidates-booked',
  templateUrl: './candidates-booked.component.html',
  styleUrls: ['./candidates-booked.component.css']
})
export class CandidatesBookedComponent implements OnInit {

  jobRequest: IJobrequest;
  candidateBooked: ICandidateBooked[] = [];
  constructor(
    private route: ActivatedRoute,
    private agencyService: AgencyService) { }


  ngOnInit(): void {
    this.agencyService.getJobRequestbyId(+this.route.snapshot.paramMap.get('id'))
    .subscribe(dataResult => {
      this.jobRequest = dataResult;
      this.getCandidateBooked();
    });
  }
  getCandidateBooked(){
    this.agencyService.getCanditestBooked(+this.route.snapshot.paramMap.get('id')).subscribe(results => {
      this.candidateBooked = results;
      console.log(this.candidateBooked);
    });
  }

}
