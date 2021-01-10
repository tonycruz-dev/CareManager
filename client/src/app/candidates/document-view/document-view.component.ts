import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CandidateService } from '../candidates.service';
import { CandidateDocument } from '../../shared/models/CandidateDocument';
@Component({
  selector: 'app-document-view',
  templateUrl: './document-view.component.html',
  styleUrls: ['./document-view.component.css']
})
export class DocumentViewComponent implements OnInit {
  candidateDocument: CandidateDocument;

  pdfSrc = '';
  constructor(
    private route: ActivatedRoute,
    private candidateService: CandidateService) { }

  ngOnInit(): void {

    this.candidateService.getCandidateDocument(+this.route.snapshot.paramMap.get('id'))
    .subscribe(dataResult => {
      this.candidateDocument = dataResult;
      this.pdfSrc = this.candidateDocument.url;
    });
  }

}
