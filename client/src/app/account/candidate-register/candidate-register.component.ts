import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { IUser } from '../../shared/models/user';
import { CandidateService } from '../../candidates/candidates.service';
import { Candidate, ICandidate } from '../../shared/models/Candidate';

@Component({
  selector: 'app-candidate-register',
  templateUrl: './candidate-register.component.html',
  styleUrls: ['./candidate-register.component.css']
})
export class CandidateRegisterComponent implements OnInit {

  registerForm: FormGroup;
  returnUrl: string;
  currentUser$: Observable<IUser>;
  constructor(
    private accountService: AccountService,
    private candidateService: CandidateService,
    private router: Router,
    private fb: FormBuilder,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl;
    this.createLoginForm();
  }

  createLoginForm() {
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      address1: ['', Validators.required],
      address2: ['', Validators.required],
      address3: ['', Validators.required],
      address4: ['', Validators.required],
      address5: [null],
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onSubmit() {

    this.accountService.registerCandidates(this.registerForm.value).subscribe((user) => {
      console.log(user);
      // tslint:disable-next-line:no-debugger
      debugger;
      this.saveNewCandidate(this.registerForm.value, user);

    }, error => {
      console.log(error);
    });
  }
  saveNewCandidate(value: any, user: any) {
    const candidate =  new Candidate();
    candidate.firstName = value.firstName;
    candidate.lastName = value.lastName;
    candidate.address1 = value.address1;
    candidate.address2 = value.address2;
    candidate.address3 = value.address3;
    candidate.address4 = value.address4;
    candidate.address5 = value.address5;
    candidate.contactNumber = 'change number';
    candidate.email = value.email;
    candidate.accoutNumber = 'update number';
    candidate.accoutName = 'update number';
    candidate.sortCode = 'update sort code';
    candidate.gradeId = 1;
    candidate.photoUrl = user.avatar;
    this.candidateService.saveCandidate(candidate).subscribe((result) => {
      console.log(result);
      this.router.navigateByUrl('/candidate/photo');
    });
  }

}
