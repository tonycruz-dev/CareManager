import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CandidateDocument } from '../../shared/models/CandidateDocument';
import { HrManagerService } from '../hr-manager.service';

@Component({
  selector: 'app-candidate-document-view',
  templateUrl: './candidate-document-view.component.html',
  styleUrls: ['./candidate-document-view.component.css']
})
export class CandidateDocumentViewComponent implements OnInit {

  candidateDocument: CandidateDocument;

  pdfSrc = '';
  constructor(
    private route: ActivatedRoute,
    private hrServices: HrManagerService) { }

  ngOnInit(): void {

    this.hrServices.getCandidateDocument(+this.route.snapshot.paramMap.get('id'))
    .subscribe(dataResult => {
      this.candidateDocument = dataResult;
      this.pdfSrc = this.candidateDocument.url;
    });
  }

}
