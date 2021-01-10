import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { AgencyService } from '../agency.service';
import { IJobrequest } from '../../shared/models/JobRequest';
import { JobRequestPagination } from '../../shared/models/JobRequestPagination';
import { JobRequestParams } from '../../shared/models/JobrequestParam';

@Component({
  selector: 'app-job-request',
  templateUrl: './job-request.component.html',
  styleUrls: ['./job-request.component.css']
})
export class JobRequestComponent implements OnInit {
  @ViewChild('search', { static: false }) searchTerm: ElementRef;

   searchDateRange: Date[];
   jobRequestPagination = new JobRequestPagination();
   pageCount = 0;
   isOpenFrom = false;
   jobRequestParams: JobRequestParams;

  constructor(private agencyService: AgencyService) {
    this.jobRequestParams = agencyService.jobRequestParams;
  }

  ngOnInit(): void {
    this.getJobRequests();
  }
  getJobRequests() {
    this.agencyService.getJobRequest().subscribe(response => {
      this.jobRequestPagination = response;
      this.pageCount = this.jobRequestPagination.totalPages;
    }, error => {
      console.log(error);
    });
  }

  onPageChanged(event: any) {
    // tslint:disable-next-line:no-debugger
    // debugger;
    const params = this.agencyService.jobRequestParams;
    if (params.pageNumber !== event) {
      params.pageNumber = event;
      this.jobRequestParams.pageNumber =  params.pageNumber;
      this.agencyService.setShopParams(params);
      this.getJobRequests();
    }
  }
  onSearch() {
    const params = this.agencyService.jobRequestParams;
    params.search = this.searchTerm.nativeElement.value;
    params.pageNumber = 1;
    this.agencyService.setShopParams(params);
    this.getJobRequests();
  }
  onRefresh() {
   // debugger;
    const params = new JobRequestParams();
    this.jobRequestParams.pageNumber = 1;
    params.pageNumber = 1;
    this.pageCount = 0;
    this.agencyService.setShopParams(params);
    this.getJobRequests();
  }
  onSearchByDate() {
    const dFrom = this.searchDateRange[0].toISOString();
    const dTo = this.searchDateRange[1].toISOString();
    const params = this.agencyService.jobRequestParams;
    params.dateFrom = dFrom;
    params.dateTo = dTo;
    params.pageNumber = 1;
    this.agencyService.setShopParams(params);
    this.getJobRequests();

  }
  selectJobStatus(status: string){
    console.log(status);
    const params = this.agencyService.jobRequestParams;
    params.search = status;
    params.pageNumber = 1;
    this.agencyService.setShopParams(params);
    this.getJobRequests();
  }

}
