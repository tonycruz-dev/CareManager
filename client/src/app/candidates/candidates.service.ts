import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { map, tap } from 'rxjs/operators';
import { CandidateJobInvite } from '../shared/models/candidate-jobs-invite';
import { JobConformed } from '../shared/models/JobConformed';
import { BehaviorSubject, of } from 'rxjs';
import { ICandidatePhoto } from '../shared/models/CandidatePhoto';
import { Candidate, ICandidate } from '../shared/models/Candidate';
import { ICandidateDocument } from '../shared/models/CandidateDocument';
import { IJobFinish } from '../shared/models/JobFinish';

@Injectable({
  providedIn: 'root'
})
export class CandidateService {
  baseUrl = environment.apiUrl;
  // tslint:disable-next-line:variable-name
  private _jobRequest = new BehaviorSubject<CandidateJobInvite[]>([]);
  // tslint:disable-next-line:variable-name
  private _jobConformed = new BehaviorSubject<JobConformed[]>([]);

  private candidatePhotosSB = new BehaviorSubject<ICandidatePhoto[]>([]);
  candidatePhotos$ = this.candidatePhotosSB.asObservable();
  private candidatePhotos: ICandidatePhoto[] = [];

  private candidateDocumentsSB = new BehaviorSubject<ICandidateDocument[]>([]);
  candidateDocuments$ = this.candidateDocumentsSB.asObservable();
  private candidateDocuments: ICandidateDocument[] = [];


  private candidateFinishSB = new BehaviorSubject<IJobFinish[]>([]);
  candidateFinish$ = this.candidateFinishSB.asObservable();
  private candidateFinish: IJobFinish[] = [];

  get jobRequest() {
    return this._jobRequest.asObservable();
  }
  get jobconformed() {
    return this._jobConformed.asObservable();
  }
  get listCandidatePhotos() {
    return this.candidatePhotosSB.asObservable();
  }

  constructor(private http: HttpClient, private router: Router) { }

  getListJobs() {
    return this.http.get<CandidateJobInvite[]>(this.baseUrl + `Candidates/GetCandidateInvited`)
    .pipe(
      map(results => {
        console.log(results);
        return results;
      }),
      tap(jobreq =>  this._jobRequest.next(jobreq))
    );
  }

  getJobConformed() {

    return this.http.get<JobConformed[]>(this.baseUrl + `Candidates/GetJobConforme`)
    .pipe(
      tap(jobConforms => this._jobConformed.next(jobConforms))
    );
  }
  getJobFinish() {

    return this.http.get<IJobFinish[]>(this.baseUrl + `Candidates/GetJobFinish`)
    .pipe(
      map(candidateFinish => {
        this.candidateFinish = candidateFinish;
        return candidateFinish;
      }),
      tap(candidateFinish => this.candidateFinishSB.next(candidateFinish))
    );
  }
  acceptJob(jobId: number) {
    // /api/Candidates/AcceptJob/{id}
    return this.http.get(this.baseUrl + `Candidates/AcceptJob/${jobId}`)
    .pipe(
      map(results => {
        console.log(results);
        return results;
      }),
    );
  }
  rejectJob(jobId: number) {
    return this.http.get(this.baseUrl + `Candidates/RejectJob/${jobId}`)
    .pipe(
      map(results => {
        console.log(results);
        return results;
      })
    );
  }
  getCandidateDocuments(){
    return this.http.get<ICandidateDocument[]>(this.baseUrl + `CandidateDocument/GetCandidateDocuments`)
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
  getCandidatePhotos(){
    return this.http.get<ICandidatePhoto[]>(this.baseUrl + `CandidatePhoto`)
    .pipe(
      map(photos => {
        this.candidatePhotos = photos;
        return photos;
      }),
      tap(photos => this.candidatePhotosSB.next(photos))
    );
  }
  setCandidatePhotoMain(id: number) {
    return this.http.put(this.baseUrl + `CandidatePhoto/setMain/${id}`, {id})
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
  saveCandidate(candidate: Candidate) {
    /// api/Candidates/AddCandidate
    return this.http.post<ICandidate>(this.baseUrl + 'Candidates/AddCandidate', candidate)
    .pipe(
      map(result => {
        console.log(result);
        return result;
      })
    );
  }

}
