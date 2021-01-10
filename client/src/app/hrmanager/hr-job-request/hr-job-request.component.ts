import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { HrManagerService } from '../hr-manager.service';
import { ActivatedRoute,  Router } from '@angular/router';
import { JobRequestPagination } from '../../shared/models/JobRequestPagination';
import { JobRequestParams } from '../../shared/models/JobrequestParam';
import { IAgency } from 'src/app/shared/models/Agency';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-hr-job-request',
  templateUrl: './hr-job-request.component.html',
  styleUrls: ['./hr-job-request.component.css']
})
export class HrJobRequestComponent implements OnInit {
  @ViewChild('search', { static: false }) searchTerm: ElementRef;
   agencyId: string;
   searchDateRange: Date[];
   jobRequestPagination = new JobRequestPagination();
   pageCount = 0;
   isOpenFrom = false;
   jobRequestParams: JobRequestParams;
   agency$: Observable<IAgency>;


  constructor(private HRService: HrManagerService,  private route: ActivatedRoute) {
    this.jobRequestParams = HRService.jobRequestParams;
  }

  ngOnInit(): void {
    this.agencyId = this.route.snapshot.paramMap.get('id');
    this.agency$ = this.HRService.agencyBS$;
    this.HRService.getAgency(+this.route.snapshot.paramMap.get('id'))
      .subscribe(results => {
       this.getJobRequests();
      // debugger;
       console.log(this.HRService.agency);
    });

  }
  getJobRequests() {
    this.jobRequestParams.agencyId = this.agencyId;
    this.HRService.setShopParams(this.jobRequestParams);
    this.HRService.getJobRequest().subscribe(response => {
      this.jobRequestPagination = response;
      console.log(this.jobRequestPagination.data);
      this.pageCount = this.jobRequestPagination.totalPages;
    }, error => {
      console.log(error);
    });
  }
  onPageChanged(event: any) {
    // tslint:disable-next-line:no-debugger
    // debugger;
    const params = this.HRService.jobRequestParams;
    if (params.pageNumber !== event) {
      params.pageNumber = event;
      this.jobRequestParams.pageNumber =  params.pageNumber;
      this.jobRequestParams.agencyId = this.agencyId;
      this.HRService.setShopParams(params);
      this.getJobRequests();
    }
  }
  onSearch() {
    const params = this.HRService.jobRequestParams;
    params.search = this.searchTerm.nativeElement.value;
    params.pageNumber = 1;
    this.jobRequestParams.agencyId = this.agencyId;
    this.HRService.setShopParams(params);
    this.getJobRequests();
  }
  onRefresh() {
   // debugger;
    const params = new JobRequestParams();
    this.jobRequestParams.pageNumber = 1;
    params.pageNumber = 1;
    this.jobRequestParams.agencyId = this.agencyId;
    this.pageCount = 0;
    this.HRService.setShopParams(params);
    this.getJobRequests();
  }
  onSearchByDate() {
    const dFrom = this.searchDateRange[0].toISOString();
    const dTo = this.searchDateRange[1].toISOString();
    const params = this.HRService.jobRequestParams;
    params.dateFrom = dFrom;
    params.dateTo = dTo;
    params.pageNumber = 1;
    this.jobRequestParams.agencyId = this.agencyId;
    this.HRService.setShopParams(params);
    this.getJobRequests();

  }
  selectJobStatus(status: string){
    console.log(status);
    const params = this.HRService.jobRequestParams;
    params.search = status;
    params.pageNumber = 1;
    this.jobRequestParams.agencyId = this.agencyId;
    this.HRService.setShopParams(params);
    this.getJobRequests();
  }

}
