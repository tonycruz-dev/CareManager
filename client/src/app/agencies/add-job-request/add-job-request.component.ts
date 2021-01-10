import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';
import { AgencyService } from '../agency.service';
import { IAgencyLoops } from '../../shared/models/AgencyLoops';
import { JobToRequest } from '../../shared/models/JobToRequest';
import { IJobrequest } from '../../shared/models/JobRequest';

@Component({
  selector: 'app-add-job-request',
  templateUrl: './add-job-request.component.html',
  styleUrls: ['./add-job-request.component.css']
})
export class AddJobRequestComponent implements OnInit {
  AgencyLoops: IAgencyLoops;
  jobToRequest = new JobToRequest();
  isOpenFrom = false;
  InsertedjobsRequests: IJobrequest[] = [];

  constructor(private agencyService: AgencyService, private toastr: ToastrService) {
    this.jobToRequest.timeDetailId = 17;
    this.jobToRequest.endTimeDetailId = 35;
  }

  ngOnInit(): void {
    this.agencyService.getAgencyLoop().subscribe(results => {
      this.AgencyLoops = results;
    });
  }
  save(form: NgForm){
    this.agencyService.InsertJobsRequest(this.jobToRequest).subscribe(results => {
        this.InsertedjobsRequests = results;
        this.toastr.success('Job Request Created');
     });
  }

}
