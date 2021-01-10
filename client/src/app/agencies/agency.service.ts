import { JobToRequest, IJobToRequest } from 'src/app/shared/models/JobToRequest';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { IJobrequest } from '../shared/models/JobRequest';
import { map, tap } from 'rxjs/operators';
import { JobRequestPagination, IJobRequestPagination } from '../shared/models/JobRequestPagination';
import { JobRequestParams } from '../shared/models/JobrequestParam';
import { IAgencyLoops } from '../shared/models/AgencyLoops';
import { ICandidateToInvite, ICandidateResponded } from '../shared/models/candidateToInvite';
import { of, BehaviorSubject } from 'rxjs';
import { ICandidateBooked } from '../shared/models/CandidateBooked';
import { ConfirmeFinal } from '../shared/models/ConfirmeFinal';
import { JobConformed } from '../shared/models/JobConformed';
import { Agency, IAgency } from '../shared/models/Agency';
import { ClientLocation, IClientLocation } from '../shared/models/ClientLocation';
import { IAgencyPhoto } from '../shared/models/AgencyPhoto';

@Injectable({
  providedIn: 'root'
})
export class AgencyService {
  baseUrl = environment.apiUrl;
  jobsRequests: IJobrequest[] = [];
  InsertedjobsRequests: IJobrequest[] = [];
  JobRequestPagination = new JobRequestPagination();
  jobRequestParams = new JobRequestParams();
  AgencyLoops: IAgencyLoops;
  candidateBookes: ICandidateBooked[] = [];
  clientLocations: IClientLocation[] = [];

  private agencyPhotosSB = new BehaviorSubject<IAgencyPhoto[]>([]);
  agencyPhotos$ = this.agencyPhotosSB.asObservable();
  private agencyPhotos: IAgencyPhoto[] = [];


  constructor(private http: HttpClient, private router: Router) { }

  getJobRequest() {
    // tslint:disable-next-line:no-debugger
    debugger;
    let params = new HttpParams();

    if (this.jobRequestParams.search) {
      params = params.append('search', this.jobRequestParams.search);
    }
    if (this.jobRequestParams.dateFrom) {
      params = params.append('dateFrom', this.jobRequestParams.dateFrom);
      params = params.append('dateTo', this.jobRequestParams.dateTo);
    }
    params = params.append('sort', this.jobRequestParams.sort);
    params = params.append('pageNumber', this.jobRequestParams.pageNumber.toString());
    params = params.append('pageSize', this.jobRequestParams.pageSize.toString());

    // return this.http.get<IJobRequestPagination>(this.baseUrl + 'Agencies/GetJobToRequests?pageSize=15&sort=dateDesc')
    return this.http.get<IJobRequestPagination>(this.baseUrl + 'Agencies/GetJobRequestData', { observe: 'response', params })
    .pipe(
      map( response => {
        this.jobsRequests = [...this.jobsRequests, ...response.body.data];
        this.JobRequestPagination = response.body;
        return this.JobRequestPagination;
      })
    );
  }

  getJobRequestParams() {
    return this.jobRequestParams;
  }

  setShopParams(params: JobRequestParams) {
    this.jobRequestParams = params;
  }
  getAgencyLoop() {
    return this.http.get<IAgencyLoops>(this.baseUrl + 'Agencies/GetAgencyLoops')
    .pipe(
      map(results => {
        this.AgencyLoops = results;
        console.log(this.AgencyLoops);
        return this.AgencyLoops;
      })
    );
  }
  InsertJobsRequest(jobRequest: JobToRequest) {
    return this.http.post<IJobrequest[]>(this.baseUrl + 'Agencies/CreateJobRequest', jobRequest)
    .pipe(
      map(results => {
        this.InsertedjobsRequests = [...this.InsertedjobsRequests, ...results] ;
        return this.InsertedjobsRequests;
      })
    );
  }
  getJobRequestbyId(id: number) {
    const jobrequest = this.jobsRequests.find(jr => jr.id === id);

    if (jobrequest) {
      return of(jobrequest);
    }

    return this.http.get<IJobrequest>(this.baseUrl + 'Agencies/GetJobToRequestById/' + id)
    .pipe(
      map(results => results)
    );
  }
  getCanditestToInvite(gradeId: number) {
    return this.http.get<ICandidateToInvite[]>(this.baseUrl + `Agencies/GetCandidateForInvite/${gradeId}`)
    .pipe(
      map(results => results)
    );
  }
  sendCanditestToInvite(candidatesId: number[], jobToRequestId: number) {
    const postInvite = {candidatesId, jobToRequestId};
    return this.http.post<ICandidateToInvite[]>(this.baseUrl + 'Agencies/InsertInvitedCandidate', postInvite)
    .pipe(
      map(results => results)
    );
  }
  getCanditestInprogress(jobRequestId: number) {
    /// api/Agencies/GetCandidateInProgress/{id}
    return this.http.get<ICandidateToInvite[]>(this.baseUrl + `Agencies/GetCandidateInProgress/${jobRequestId}`)
    .pipe(
      map(results => {
        console.log(results);
        return results;
      })
    );
  }
  getCanditestResponded(jobRequestId: number) {
    /// api/Agencies/GetCandidateInProgress/{id}
    return this.http.get<ICandidateResponded[]>(this.baseUrl + `Agencies/GetCandidateResponded/${jobRequestId}`)
    .pipe(
      map(results => {
        console.log(results);
        return results;
      })
    );
  }
  getCanditestBooked(jobRequestId: number) {
    const url = this.baseUrl + `Agencies/GetCandidateBooked/${jobRequestId}`;
    return this.http.get<ICandidateBooked[]>(url)
    .pipe(
      map(results => {
        console.log(results);
        this.candidateBookes = results;
        return results;
      })
    );
  }
  getBookedCanditest(jobRequestId: number) {
    const url = this.baseUrl + `Agencies/GetCandidateBooked/${jobRequestId}`;
    return this.http.get<ICandidateBooked[]>(url)
    .pipe(
      map(results => {
        this.candidateBookes = results;
        return results;
      }),
    );
  }
  getBookedCanditate(id: number): ICandidateBooked {
    const candidate = this.candidateBookes.find(cand => cand.jobConfirmedId === id);
    return candidate;
  }
  saveFinalizedJobConfirmedAsync(confirmeFinal: ConfirmeFinal){
    return this.http.put<JobConformed[]>(this.baseUrl + 'Agencies/FinalizedJobConfirmed', confirmeFinal)
    .pipe(
      map(results => {
        console.log(results);
      })
    );
  }
  saveAgency(agency: Agency){
    return this.http.post<IAgency[]>(this.baseUrl + 'Agencies/AddAgency', agency)
    .pipe(
      map(results => {
        console.log(results);
        return results;
      })
    );
  }
 // client location Manager
 saveClientLocation(clientlocation: ClientLocation){
  return this.http.post<IAgency[]>(this.baseUrl + 'Agencies/AddClientLocation', clientlocation)
  .pipe(
    map(results => {
      console.log(results);
      return results;
    })
  );
  }
  getClientLocations() {
    const url = this.baseUrl + `Agencies/GetClientLocations/`;
    return this.http.get<IClientLocation[]>(url)
    .pipe(
      map(results => {
        this.clientLocations = results;
        return results;
      }),
    );
  }
  getAgencyPhotos(){
    return this.http.get<IAgencyPhoto[]>(this.baseUrl + `AgencyPhoto`)
    .pipe(
      map(photos => {
        console.log(photos);
        this.agencyPhotos = photos;
        return photos;
      }),
      tap(photos => this.agencyPhotosSB.next(photos))
    );
  }
  setAgencyPhotoMain(id: number) {
    /// api/CandidatePhoto/{id}/setMain
    return this.http.put(this.baseUrl + `AgencyPhoto/setMain/${id}`, {id})
    .pipe(
      map(() => {
        const findMinPhoto = this.agencyPhotos.find(p => p.isMain === true);
        findMinPhoto.isMain = false;
        const findNewPhoto = this.agencyPhotos.find(p => p.id === id);
        findNewPhoto.isMain = true;
        const photoIndex = this.agencyPhotos.findIndex(cp => cp.id === findNewPhoto.id);
        this.agencyPhotos[photoIndex] = {...findNewPhoto};
        this.agencyPhotosSB.next([...this.agencyPhotos]);

        return 'Done';
      })
    );
  }
  deleteAgencyPhoto(id: number) {
  // /api/CandidatePhoto/{id}
    return this.http.delete(this.baseUrl + `AgencyPhoto/DeleteAgentPhoto/${id}`)
      .pipe(
        map(() => {
          const photos = [...this.agencyPhotos.filter(cp => cp.id !== id)];
          this.agencyPhotos = photos;
          this.agencyPhotosSB.next(photos);
          return 'Done';
        })
    );
  }
  UpdateAgencyPhoto(id: number, description: string) {
    const photo = this.agencyPhotos.find(cp => cp.id === id);
    photo.description = description;
    const photoIndex = this.agencyPhotos.findIndex(cp => cp.id === id);
    this.agencyPhotos[photoIndex] = {...photo};
    const photos = [...this.agencyPhotos];
    this.agencyPhotos = photos;
    this.agencyPhotosSB.next(photos);
  }
  updateAfterInsert(agencyPhoto: IAgencyPhoto) {
    this.agencyPhotos.push(agencyPhoto);
    this.agencyPhotosSB.next(this.agencyPhotos);
  }

}
