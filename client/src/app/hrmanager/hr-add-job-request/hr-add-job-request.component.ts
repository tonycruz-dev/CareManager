import { IAgency } from 'src/app/shared/models/Agency';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute,  Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';
import { HrManagerService } from '../hr-manager.service';

import { IAgencyLoops } from '../../shared/models/AgencyLoops';
import { JobToRequest } from '../../shared/models/JobToRequest';
import { IJobrequest } from '../../shared/models/JobRequest';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-hr-add-job-request',
  templateUrl: './hr-add-job-request.component.html',
  styleUrls: ['./hr-add-job-request.component.css']
})
export class HrAddJobRequestComponent implements OnInit {
  agencyId: string;
  AgencyLoops: IAgencyLoops;
  agency$: Observable<IAgency>;
  jobToRequest = new JobToRequest();
  isOpenFrom = false;
  InsertedjobsRequests: IJobrequest[] = [];

  constructor(
    private hrService: HrManagerService,
    private toastr: ToastrService,
    private route: ActivatedRoute) {
    this.jobToRequest.timeDetailId = 17;
    this.jobToRequest.endTimeDetailId = 35;
   }

   ngOnInit(): void {
    this.agencyId = this.route.snapshot.paramMap.get('id');
    this.agency$ = this.hrService.agencyBS$;
    this.hrService.getAgency(+this.route.snapshot.paramMap.get('id')).subscribe(() => {
      this.getAgencyLoops();
    });
  }
  getAgencyLoops(){
    this.hrService.getAgencyLoop(this.agencyId).subscribe(results => {
      this.AgencyLoops = results;
    });
  }
  save(form: NgForm){
    this.jobToRequest.agencyId = +this.agencyId;
    this.hrService.InsertJobsRequest(this.jobToRequest).subscribe(results => {
        this.InsertedjobsRequests = results;
        this.toastr.success('Job Request Created');
     });
  }


}
