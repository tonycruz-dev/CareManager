import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/shared/models/user';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {
  loginForm: FormGroup;
  returnUrl: string;
  currentUser$: Observable<IUser>;
  constructor(private accountService: AccountService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl;
    this.createLoginForm();
  }

  createLoginForm() {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators
        .pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]),
      password: new FormControl('', Validators.required)
    });
  }

  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe(() => {
      if (!this.returnUrl) {
        console.log('not url');
        if (this.accountService.occupation === 'Client')
        {
          this.router.navigateByUrl('/agency/jobs');
        }
        if (this.accountService.occupation === 'Candidate')
        {
          this.router.navigateByUrl('/candidate');
        }
        if (this.accountService.occupation === 'HR')
        {
          this.router.navigateByUrl('/hr-manager');
        }

      } else {
        this.router.navigateByUrl(this.returnUrl);
      }

    }, error => {
      console.log(error);
    });
  }

}
