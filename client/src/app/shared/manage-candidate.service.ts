import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { map, tap } from 'rxjs/operators';
import { CandidateJobInvite } from '../shared/models/candidate-jobs-invite';
import { BehaviorSubject } from 'rxjs';
import { IJobFinish } from './models/JobFinish';
import { JobConformed } from './models/JobConformed';

@Injectable({
  providedIn: 'root'
})
export class ManageCandidateService {
  baseUrl = environment.apiUrl;

  private candidateFinishSB = new BehaviorSubject<IJobFinish[]>([]);
  candidateFinish$ = this.candidateFinishSB.asObservable();
  private candidateFinish: IJobFinish[] = [];

  private candidateJobRequestBS = new BehaviorSubject<CandidateJobInvite[]>([]);
  candidateJobRequestBS$ = this.candidateJobRequestBS.asObservable();

  private candidateJobConformedBS = new BehaviorSubject<JobConformed[]>([]);
  candidateJobConformedBS$ = this.candidateJobConformedBS.asObservable();

  constructor(private http: HttpClient) { }

  setJobFinish(listJobFinish: IJobFinish[]) {
    this.candidateFinish = [...listJobFinish];
    this.candidateFinishSB.next(this.candidateFinish);
  }
   setJobRequest(listJobRequest: CandidateJobInvite[]) {
    this.candidateJobRequestBS.next(listJobRequest);
   }
   setJobConformed(listJobRequest: JobConformed[]) {
    this.candidateJobConformedBS.next(listJobRequest);
   }
}
