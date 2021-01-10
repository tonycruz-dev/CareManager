import { Component, OnInit, AfterContentInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICandidatePhoto } from '../../shared/models/CandidatePhoto';
import { CandidateService } from '../candidates.service';
import { AccountService } from 'src/app/account/account.service';
import swal from 'sweetalert2';
import { ICandidateDocument } from 'src/app/shared/models/CandidateDocument';

@Component({
  selector: 'app-upload-document',
  templateUrl: './upload-document.component.html',
  styleUrls: ['./upload-document.component.css']
})
export class UploadDocumentComponent implements OnInit, AfterContentInit {
  files: File[] = [];
  candidateDocuments$: Observable<ICandidateDocument[]>;
  message: string;
  deleteDocument: ICandidateDocument;

  constructor(
    private http: HttpClient,
    private accountService: AccountService,
    private candidateService: CandidateService) { }

  ngOnInit(): void {
    this.candidateDocuments$ = this.candidateService.candidateDocuments$;
  }
  ngAfterContentInit(): void {
    this.candidateService.getCandidateDocuments().subscribe();
  }
  onSelect(event) {
    console.log('seleted file', event);
    this.files.push(...event.addedFiles);
    this.onFileUpload();
  }
  onFileUpload() {
    const fd = new FormData();
    fd.append('File', this.files[0], this.files[0].name);

    this.http.post('https://localhost:44327/api/CandidateDocument', fd)
      .subscribe((res: ICandidateDocument) =>  {
      console.log(res);
      this.onRemove1();
      this.refreshPhotos(res);
    }
    );
  }
  refreshPhotos(item: ICandidateDocument){
    this.candidateService.updateAfterInsertPdf(item);
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
