import { Component, OnInit, AfterViewInit } from '@angular/core';
import { HrManagerService } from '../hr-manager.service';
import { Observable } from 'rxjs';
import { HRDashboard } from '../models/HRDashboard';

@Component({
  selector: 'app-hr-home',
  templateUrl: './hr-home.component.html',
  styleUrls: ['./hr-home.component.css']
})
export class HrHomeComponent implements OnInit, AfterViewInit {
  dashboard$: Observable<HRDashboard>;
  constructor(private hrManagerService: HrManagerService) { }

  ngOnInit(): void {
    this.dashboard$ = this.hrManagerService.hrDashboardSB$;
  }
  ngAfterViewInit(): void {
    this.hrManagerService.getDashborde().subscribe();
  }

}
