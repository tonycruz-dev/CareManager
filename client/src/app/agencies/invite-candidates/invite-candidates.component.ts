import { Component, OnInit } from '@angular/core';
import { ActivatedRoute,  Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AgencyService } from '../agency.service';
import { IJobToRequest } from 'src/app/shared/models/JobToRequest';
import { IJobrequest } from '../../shared/models/JobRequest';
import { ICandidateToInvite } from '../../shared/models/candidateToInvite';

@Component({
  selector: 'app-invite-candidates',
  templateUrl: './invite-candidates.component.html',
  styleUrls: ['./invite-candidates.component.css']
})
export class InviteCandidatesComponent implements OnInit {
 jobRequest: IJobrequest;
 candidateToInvites: ICandidateToInvite[] = [];
 candidateSelected = 0;
 buttonDisabled = true;


  constructor(
    private route: ActivatedRoute,
    private agencyService: AgencyService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.agencyService.getJobRequestbyId(+this.route.snapshot.paramMap.get('id'))
    .subscribe(dataResult => {
      this.jobRequest = dataResult;
      this.getCandidateToInvite(this.jobRequest.gradeId);
    });
  }
  getCandidateToInvite(gradeId: number){
    this.agencyService.getCanditestToInvite(gradeId).subscribe(results => {
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
    this.agencyService.sendCanditestToInvite(canditeToInviteIds, this.jobRequest.id)
    .subscribe(response => {
      this.toastr.success('Job invite was send to selected candidate');
      this.router.navigate(['/agency/jobs']);
    });

  }
  sendAllInvite() {
    const canditeToInviteIds: number[] = [];
    this.candidateToInvites.forEach(cti => {
        canditeToInviteIds.push(cti.id);
    });
    this.agencyService.sendCanditestToInvite(canditeToInviteIds, this.jobRequest.id)
    .subscribe(response => {
        console.log(response);
        this.toastr.success('Job invite was send to all candidate');
        this.router.navigate(['/agency/jobs']);
    });

  }

}
