import { Component, OnInit } from '@angular/core';
import { HrManagerService } from '../hr-manager.service';
import { ICandidate } from '../models/HRDashboard';

@Component({
  selector: 'app-hr-candidates',
  templateUrl: './hr-candidates.component.html',
  styleUrls: ['./hr-candidates.component.css']
})
export class HrCandidatesComponent implements OnInit {

  candidates: ICandidate[] = [];

  constructor(private hrservice: HrManagerService) { }

  ngOnInit(): void {
    this.hrservice.getCandidates().subscribe(results => {
      this.candidates =  results;
    });
  }

}
