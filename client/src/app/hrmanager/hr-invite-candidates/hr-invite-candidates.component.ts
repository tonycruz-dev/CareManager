import { Component, OnInit } from '@angular/core';
import { ActivatedRoute,  Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HrManagerService } from '../hr-manager.service';
import { IJobToRequest } from 'src/app/shared/models/JobToRequest';
import { IJobrequest } from '../../shared/models/JobRequest';
import { ICandidateToInvite } from '../../shared/models/candidateToInvite';

@Component({
  selector: 'app-hr-invite-candidates',
  templateUrl: './hr-invite-candidates.component.html',
  styleUrls: ['./hr-invite-candidates.component.css']
})
export class HrInviteCandidatesComponent implements OnInit {
  jobRequest: IJobrequest;
  candidateToInvites: ICandidateToInvite[] = [];
  candidateSelected = 0;
  buttonDisabled = true;
  agencyId: number;

  constructor(
    private hrservice: HrManagerService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService) { }

    ngOnInit(): void {
      this.agencyId = this.hrservice.agency.id;
      this.hrservice.getJobRequestbyId(+this.route.snapshot.paramMap.get('id'))

      .subscribe(dataResult => {
        this.jobRequest = dataResult;
        this.getCandidateToInvite(this.jobRequest.gradeId);
      });
    }
    getCandidateToInvite(gradeId: number){
      this.hrservice.getCanditestToInvite(gradeId).subscribe(results => {
        this.candidateToInvites = results;
        console.log(this.candidateToInvites);
        console.log(this.candidateSelected);
      });
    }
    isSelectedChange(invCan: ICandidateToInvite)
    {
      invCan.isSelected = !invCan.isSelected;
      if (invCan.isSelected)
      {
        this.candidateSelected  = this.candidateSelected  + 1;
        this.buttonDisabled = false;
      } else {
        this.candidateSelected  = this.candidateSelected  - 1;
        if (this.candidateSelected <= 0)
          {
            this.buttonDisabled = true;
          }
      }
    }
    sendInvite() {
      const canditeToInviteIds: number[] = [];
      this.candidateToInvites.forEach(cti => {
        if (cti.isSelected) {
          canditeToInviteIds.push(cti.id);
        }
      });
      this.hrservice.sendCanditestToInvite(canditeToInviteIds, this.jobRequest.id)
      .subscribe(response => {
        this.toastr.success('Job invite was send to selected candidate');
        this.router.navigate(['/hr-manager/hr-job-request',  this.agencyId]);
      });

    }
    sendAllInvite() {
      const canditeToInviteIds: number[] = [];
      this.candidateToInvites.forEach(cti => {
          canditeToInviteIds.push(cti.id);
      });
      this.hrservice.sendCanditestToInvite(canditeToInviteIds, this.jobRequest.id)
      .subscribe(response => {
          console.log(response);
          this.toastr.success('Job invite was send to all candidate');
          this.router.navigate(['/hr-manager/hr-job-request',  this.agencyId]);
          // this.router.navigate(['/root/child', crisis.id]);
      });

    }

}
