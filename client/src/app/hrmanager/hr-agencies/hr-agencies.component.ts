import { Component, OnInit } from '@angular/core';
import { HrManagerService } from '../hr-manager.service';
import { IAgency } from '../models/HRDashboard';

@Component({
  selector: 'app-hr-agencies',
  templateUrl: './hr-agencies.component.html',
  styleUrls: ['./hr-agencies.component.css']
})
export class HrAgenciesComponent implements OnInit {

 agencies: IAgency[] = [];

  constructor(private hrservice: HrManagerService) { }

  ngOnInit(): void {
    this.hrservice.getAgencies().subscribe(results => {
      console.log(results);
      this.agencies = results;
    });

  }

}
