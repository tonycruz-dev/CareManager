import { IJobRequestPagination } from './../shared/models/JobRequestPagination';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, pipe, of, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map, tap } from 'rxjs/operators';
import { HRDashboard, IAgency } from './models/HRDashboard';
import { ICandidate } from '../shared/models/Candidate';
import { IJobrequest } from '../shared/models/JobRequest';
import { JobRequestPagination } from '../shared/models/JobRequestPagination';
import { JobRequestParams } from '../shared/models/JobrequestParam';
import { IAgencyLoops } from '../shared/models/AgencyLoops';
import { ICandidateBooked } from '../shared/models/CandidateBooked';
import { IClientLocation } from '../shared/models/ClientLocation';
import { JobToRequest } from '../shared/models/JobToRequest';
import { ICandidateToInvite, ICandidateResponded } from '../shared/models/candidateToInvite';
import { ConfirmeFinal } from '../shared/models/ConfirmeFinal';
import { JobConformed } from '../shared/models/JobConformed';
import { IJobFinish } from '../shared/models/JobFinish';
import { CandidateJobInvite } from '../shared/models/candidate-jobs-invite';
import { ICandidatePhoto } from '../shared/models/CandidatePhoto';
import { ICandidateDocument } from '../shared/models/CandidateDocument';


@Injectable({
  providedIn: 'root'
})
export class HrManagerService {
  baseUrl = environment.apiUrl;
  private hrDashboardSB = new BehaviorSubject<HRDashboard>(null);
  hrDashboardSB$ = this.hrDashboardSB.asObservable();

  private agencyBS = new BehaviorSubject<IAgency>(null);
  agencyBS$ = this.agencyBS.asObservable();
  agency: IAgency;


  private hrDashboard: HRDashboard;
  private hragencies: IAgency[] = [];
  private hrcandidates: ICandidate[] = [];
  // agency
  jobsRequests: IJobrequest[] = [];
  InsertedjobsRequests: IJobrequest[] = [];
  JobRequestPagination = new JobRequestPagination();
  jobRequestParams = new JobRequestParams();
  AgencyLoops: IAgencyLoops;
  candidateBookes: ICandidateBooked[] = [];
  clientLocations: IClientLocation[] = [];

  private candidateFinishSB = new BehaviorSubject<IJobFinish[]>([]);
  candidateFinish$ = this.candidateFinishSB.asObservable();
  private candidateFinish: IJobFinish[] = [];

  private candidatejobRequestSB = new BehaviorSubject<CandidateJobInvite[]>([]);
  candidatejobRequest$ = this.candidatejobRequestSB.asObservable();

  private candidatejobConformedSB = new BehaviorSubject<JobConformed[]>([]);
  candidatejobConformed$ = this.candidatejobConformedSB.asObservable();

  private candidateSB = new BehaviorSubject<ICandidate>(null);
  candidate$ = this.candidateSB.asObservable();

  private candidatePhotosSB = new BehaviorSubject<ICandidatePhoto[]>([]);
  candidatePhotos$ = this.candidatePhotosSB.asObservable();
  private candidatePhotos: ICandidatePhoto[] = [];

  private candidateDocumentsSB = new BehaviorSubject<ICandidateDocument[]>([]);
  candidateDocuments$ = this.candidateDocumentsSB.asObservable();
  private candidateDocuments: ICandidateDocument[] = [];

  // jobConformed
  constructor(private http: HttpClient, private router: Router) { }

  getDashborde() {
    return this.http.get<HRDashboard>(this.baseUrl + `HRManager/GetHRDashboard`)
    .pipe(
      map(hrdashbord => {
        this.hrDashboard = hrdashbord;
        return hrdashbord;
      }),
      tap(hrdashbord => this.hrDashboardSB.next(hrdashbord))
    );
  }
  getAgency(id: number) {
    return this.http.get<IAgency>(this.baseUrl + 'HRManager/GetAgeny/' + id)
    .pipe(
      map(agency => {
        this.agency =  agency;
        return agency;
      }),
      tap(agency => this.agencyBS.next(agency))
    );
     // tap(photos => this.agencyPhotosSB.next(photos))
    // /api/HRManager/GetAgeny/{id}
  }
  getAgencies() {
    return this.http.get<IAgency[]>(this.baseUrl + 'HRManager/GetAgencis')
    .pipe(
      map(agencies => {
        this.hragencies =  agencies;
        return this.hragencies;
      })
    );
  }
  getCandidates(){
    return this.http.get<ICandidate[]>(this.baseUrl + `HRManager/GetCandidates`)
    .pipe(
      map(candidates => {
        this.hrcandidates = candidates;
        return this.hrcandidates;
      })
    );
  }
  getJobRequest() {
    // tslint:disable-next-line:no-debugger
    // debugger;
    let params = new HttpParams();

    if (this.jobRequestParams.search) {
      params = params.append('search', this.jobRequestParams.search);
    }
    if (this.jobRequestParams.dateFrom) {
      params = params.append('dateFrom', this.jobRequestParams.dateFrom);
      params = params.append('dateTo', this.jobRequestParams.dateTo);
    }
    params = params.append('sort', this.jobRequestParams.sort);
    params = params.append('agencyId', this.jobRequestParams.agencyId);

    params = params.append('pageNumber', this.jobRequestParams.pageNumber.toString());
    params = params.append('pageSize', this.jobRequestParams.pageSize.toString());

    // return this.http.get<IJobRequestPagination>(this.baseUrl + 'Agencies/GetJobToRequests?pageSize=15&sort=dateDesc')
    return this.http.get<IJobRequestPagination>(this.baseUrl + 'HRManager/GetJobRequestByAgency', { observe: 'response', params })
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
  getAgencyLoop(agencyId: string) {
    return this.http.get<IAgencyLoops>(this.baseUrl + `HRManager/GetHRAgencyLoops/${agencyId}`)
    .pipe(
      map(results => {
        this.AgencyLoops = results;
        console.log(this.AgencyLoops);
        return this.AgencyLoops;
      })
    );
  }
  InsertJobsRequest(jobRequest: JobToRequest) {
    return this.http.post<IJobrequest[]>(this.baseUrl + 'HRManager/CreateJobRequest', jobRequest)
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
    // /api/HRManager/GetJobConfirmedByJobId/{id}
    return this.http.get<IJobrequest>(this.baseUrl + 'HRManager/GetJobToRequestById/' + id)
    .pipe(
      map(results => results)
    );
  }
  getCanditestToInvite(gradeId: number) {
    return this.http.get<ICandidateToInvite[]>(this.baseUrl + `HRManager/GetCandidateForInvite/${gradeId}`)
    .pipe(
      map(results => results)
    );
  }
  sendCanditestToInvite(candidatesId: number[], jobToRequestId: number) {
    const postInvite = {candidatesId, jobToRequestId};
    return this.http.post<ICandidateToInvite[]>(this.baseUrl + 'HRManager/InsertInvitedCandidate', postInvite)
    .pipe(
      map(results => results)
    );
  }
  getCanditestInprogress(jobRequestId: number) {
    /// api/Agencies/GetCandidateInProgress/{id}
    return this.http.get<ICandidateToInvite[]>(this.baseUrl + `HRManager/GetCandidateInProgress/${jobRequestId}`)
    .pipe(
      map(results => {
        console.log(results);
        return results;
      })
    );
  }
  getCanditestResponded(jobRequestId: number) {
    /// api/Agencies/GetCandidateInProgress/{id}
    return this.http.get<ICandidateResponded[]>(this.baseUrl + `HRManager/GetCandidateResponded/${jobRequestId}`)
    .pipe(
      map(results => {
        console.log(results);
        return results;
      })
    );
  }
  getCanditestBooked(jobRequestId: number) {
    const url = this.baseUrl + `HRManager/GetCandidateBooked/${jobRequestId}`;
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
    const url = this.baseUrl + `HRManager/GetCandidateBooked/${jobRequestId}`;
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
    return this.http.put<JobConformed[]>(this.baseUrl + 'HRManager/FinalizedJobConfirmed', confirmeFinal)
    .pipe(
      map(results => {
        console.log(results);
      })
    );
  }
  // Candidates
  getCandidate(id: number) {
    return this.http.get<ICandidate>(this.baseUrl + `HRManager/GetCandidate/${id}`)
    .pipe(
      tap(candidate => this.candidateSB.next(candidate))
    );
  }
  getJobFinish(id: number) {

    return this.http.get<IJobFinish[]>(this.baseUrl + `HRManager/GetJobFinish/${id}`)
    .pipe(
      map(candidateFinish => {
        this.candidateFinish = candidateFinish;
        return candidateFinish;
      }),
      tap(candidateFinish => this.candidateFinishSB.next(candidateFinish))
    );
  }
  getListJobs(id: number) {
    return this.http.get<CandidateJobInvite[]>(this.baseUrl + `HRManager/GetCandidateInvited/${id}`)
    .pipe(
      map(results => {
        console.log(results);
        return results;
      }),
      tap(jobreq =>  this.candidatejobRequestSB.next(jobreq))
    );
  }

  getJobConformed(id: number) {

    return this.http.get<JobConformed[]>(this.baseUrl + `HRManager/GetJobConforme/${id}`)
    .pipe(
      tap(jobConforms => this.candidatejobConformedSB.next(jobConforms))
    );
  }
  acceptJob(jobId: number) {
    // /api/Candidates/AcceptJob/{id}
    return this.http.get(this.baseUrl + `HRManager/AcceptJob/${jobId}`)
    .pipe(
      map(results => {
        console.log(results);
        return results;
      }),
    );
  }
  rejectJob(jobId: number) {
    return this.http.get(this.baseUrl + `HRManager/RejectJob/${jobId}`)
    .pipe(
      map(results => {
        console.log(results);
        return results;
      })
    );
  }
  uploadCandidatePhoto(fd: FormData) {
    return this.http.post(this.baseUrl + 'HRManagerPhotos/AddHRCandidatePhotoForUser', fd);
  }
  getCandidatePhotos(id: number) {
    return this.http.get<ICandidatePhoto[]>(this.baseUrl + `HRManagerPhotos/GetHRCandidatePhotos/${id}`)
    .pipe(
      map(photos => {
        this.candidatePhotos = photos;
        return photos;
      }),
      tap(photos => this.candidatePhotosSB.next(photos))
    );
  }

  setCandidatePhotoMain(id: number, candidateId: number) {
    const sentMainPhoto = {id, candidateId};
    return this.http.put(this.baseUrl + `HRManagerPhotos/setHRCandidateMain/`, sentMainPhoto)
    .pipe(
      map(() => {
          const findMinPhoto = this.candidatePhotos.find(p => p.isMain === true);
          findMinPhoto.isMain = false;
          const findNewPhoto = this.candidatePhotos.find(p => p.id === id);
          findNewPhoto.isMain = true;
          const photoIndex = this.candidatePhotos.findIndex(cp => cp.id === findNewPhoto.id);
          this.candidatePhotos[photoIndex] = {...findNewPhoto};
          this.candidatePhotosSB.next([...this.candidatePhotos]);
          return 'Done';
      })
    );
  }
  deleteCandidatePhoto(id: number) {
 // /api/CandidatePhoto/{id}
    return this.http.delete(this.baseUrl + `CandidatePhoto/DeletePhoto/${id}`)
      .pipe(
        map(() => {
          const photos = [...this.candidatePhotos.filter(cp => cp.id !== id)];
          this.candidatePhotos = photos;
          this.candidatePhotosSB.next(photos);
          return 'Done';
        })
    );
  }
  UpdateCandidatePhoto(id: number, description: string) {
    const photo = this.candidatePhotos.find(cp => cp.id === id);
    photo.description = description;
    const photoIndex = this.candidatePhotos.findIndex(cp => cp.id === id);
    this.candidatePhotos[photoIndex] = {...photo};
    const photos = [...this.candidatePhotos];
    this.candidatePhotos = photos;
    this.candidatePhotosSB.next(photos);
  }
  updateAfterInsert(candidate: ICandidatePhoto) {
    this.candidatePhotos.push(candidate);
    this.candidatePhotosSB.next(this.candidatePhotos);
  }
  updateAfterInsertPdf(document: ICandidateDocument) {
    this.candidateDocuments.push(document);
    this.candidatePhotosSB.next(this.candidatePhotos);
  }
  getCandidateDocuments(id: number){
    return this.http.get<ICandidateDocument[]>(this.baseUrl + `HRManagerDocuments/GetCandidateDocuments/${id}`)
    .pipe(
      map(documents => {
        this.candidateDocuments = documents;
        return documents;
      }),
      tap(documents => this.candidateDocumentsSB.next(documents))
    );
  }
  getCandidateDocument(id: number) {
    const doc = this.candidateDocuments.find(candDoc => candDoc.candidateId === id);
    if (doc) {
      return of(doc);
    }
    return this.http.get<ICandidateDocument>(this.baseUrl + `CandidateDocument/GetCandidateDocument/${id}`);
  }
  uploadCandidateDocument(fd: FormData) {
    return this.http.post(this.baseUrl + 'HRManagerDocuments/AddCandidateDocument', fd);
  }

}
