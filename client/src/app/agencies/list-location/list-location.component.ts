import { Component, OnInit } from '@angular/core';
import { ClientLocation, IClientLocation } from '../../shared/models/ClientLocation';
import { AgencyService } from '../agency.service';

@Component({
  selector: 'app-list-location',
  templateUrl: './list-location.component.html',
  styleUrls: ['./list-location.component.css']
})
export class ListLocationComponent implements OnInit {
  clientLocation = new ClientLocation();
  clientLcations: IClientLocation[] = [];
  constructor(private agencyService: AgencyService) { }

  ngOnInit(): void {
    this.getClientLocation();
  }
  saveServices(){
    console.log(this.clientLocation);
    this.agencyService.saveClientLocation(this.clientLocation)
    .subscribe(() => this.getClientLocation());
  }
  getClientLocation(){
    this.agencyService.getClientLocations().subscribe(response => {
      this.clientLcations = response;
      console.log(this.clientLcations);
    });
  }

}
