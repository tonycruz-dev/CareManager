import { Component, OnInit, AfterContentInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { HrManagerService } from '../hr-manager.service';
import { ICandidateDocument } from 'src/app/shared/models/CandidateDocument';
import { ICandidate } from '../../shared/models/Candidate';

@Component({
  selector: 'app-hr-manage-profile-documents',
  templateUrl: './hr-manage-profile-documents.component.html',
  styleUrls: ['./hr-manage-profile-documents.component.css']
})
export class HrManageProfileDocumentsComponent implements OnInit, AfterContentInit  {

  files: File[] = [];
  candidateDocuments$: Observable<ICandidateDocument[]>;
  message: string;
  deleteDocument: ICandidateDocument;
  candidate$: Observable<ICandidate>;
  candidateId: string;

  constructor(
    private route: ActivatedRoute,
    private hrServices: HrManagerService) { }

  ngOnInit(): void {
    this.candidateId = this.route.snapshot.paramMap.get('id');
    this.candidateDocuments$ = this.hrServices.candidateDocuments$;
    this.candidate$ = this.hrServices.getCandidate(+ this.candidateId);
  }
  ngAfterContentInit(): void {
    this.hrServices.getCandidateDocuments(+this.candidateId).subscribe();
  }
  onSelect(event) {
    console.log('seleted file', event);
    this.files.push(...event.addedFiles);
    this.onFileUpload();
  }
  onFileUpload() {
    const fd = new FormData();
    fd.append('File', this.files[0], this.files[0].name);
    fd.append('candidateId', this.route.snapshot.paramMap.get('id'));
    this.hrServices.uploadCandidateDocument(fd)
      .subscribe((res: ICandidateDocument) =>  {
      this.onRemove1();
      this.refreshPhotos(res);
    }
    );
  }
  refreshPhotos(item: ICandidateDocument){
    this.hrServices.updateAfterInsertPdf(item);
  }
  onRemove(event) {
    console.log('remove', event);
    console.log(this.files.indexOf(event));
    this.files.splice(this.files.indexOf(event), 1);
  }
  onRemove1() {
    this.files.splice(0, 1);
  }

}
