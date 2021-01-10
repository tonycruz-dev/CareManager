import { Component, OnInit } from '@angular/core';
import { ICandidateBooked } from 'src/app/shared/models/CandidateBooked';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AgencyService } from '../agency.service';
import { RatingConfig } from 'ngx-bootstrap/rating';
import { ConfirmeFinal } from 'src/app/shared/models/ConfirmeFinal';


export function getRatingConfig(): RatingConfig {
  return Object.assign(new RatingConfig(), { ariaLabel: 'My Rating' });
}
@Component({
  selector: 'app-job-request-detail',
  templateUrl: './job-request-detail.component.html',
  styleUrls: ['./job-request-detail.component.css'],
  providers: [{ provide: RatingConfig, useFactory: getRatingConfig }]
})
export class JobRequestDetailComponent implements OnInit {

  candidateBooked: ICandidateBooked;
  frmCandidate = {
    finalNote: '',
    max: 10,
    rate: 4
  };
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private agencyService: AgencyService,
    private toastr: ToastrService) { }


  ngOnInit(): void {
    this.candidateBooked = this.agencyService.getBookedCanditate(+this.route.snapshot.paramMap.get('id'));
    console.log(this.candidateBooked);
  }
  confirmSelection(event: KeyboardEvent) {
    if (event.keyCode === 13 || event.key === 'Enter') {
      // this.isReadonly = true;
    }
  }
  onUpdateFinalJobRequest(){
    const confirmeFinal =  new ConfirmeFinal();
    confirmeFinal.comment = this.frmCandidate.finalNote;
    confirmeFinal.raiting = this.frmCandidate.rate;
    confirmeFinal.jobConfirmedId = this.candidateBooked.jobConfirmedId;
    confirmeFinal.jobToRequestId = this.candidateBooked.jobToRequestId;
    this.agencyService.saveFinalizedJobConfirmedAsync(confirmeFinal).subscribe(() => {
      this.toastr.success('Job Done');
      this.router.navigateByUrl('/agency/booked/' + this.candidateBooked.jobToRequestId);
    });
  }

}
