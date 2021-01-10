import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/shared/models/user';
import { AgencyService } from 'src/app/agencies/agency.service';
import { Agency } from 'src/app/shared/models/Agency';

@Component({
  selector: 'app-agency-register',
  templateUrl: './agency-register.component.html',
  styleUrls: ['./agency-register.component.css']
})
export class AgencyRegisterComponent implements OnInit {

  registerForm: FormGroup;
  returnUrl: string;
  currentUser$: Observable<IUser>;
  constructor(
    private accountService: AccountService,
    private agencyService: AgencyService,
    private router: Router,
    private fb: FormBuilder,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl;
    this.createLoginForm();
  }

  createLoginForm() {
    this.registerForm = this.fb.group({
      companyName: ['', Validators.required],
      contactName: ['', Validators.required],
      address1: ['', Validators.required],
      address2: ['', Validators.required],
      address3: ['', Validators.required],
      address4: ['', Validators.required],
      address5: [''],
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onSubmit() {

    this.accountService.registerClient(this.registerForm.value).subscribe((user) => {
      console.log(user);
      this.saveNewAgency(this.registerForm.value, user);

    }, error => {
      console.log(error);
    });
  }
  saveNewAgency(value: any, user: any) {
    const agency =  new Agency();
    agency.name = value.companyName;
    agency.contactName = value.contactName;
    agency.address1 = value.address1;
    agency.address2 = value.address2;
    agency.address3 = value.address3;
    agency.address4 = value.address4;
    agency.address5 = value.address5;
    agency.contactNumber = 'change number';
    agency.email = value.email;
    agency.accoutNumber = 'update number';
    agency.accoutName = 'update number';
    agency.sortCode = 'update sort code';
    agency.logoUrl = user.avatar;
    this.agencyService.saveAgency(agency).subscribe((result) => {
      this.router.navigateByUrl('/agency/profile-photo');
      console.log(result);
    });
  }
}
