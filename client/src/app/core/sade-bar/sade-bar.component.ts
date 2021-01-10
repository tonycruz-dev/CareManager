import {Component, OnInit, AfterContentInit, ViewChild, ElementRef, TemplateRef, HostBinding } from '@angular/core';
import { IUser } from 'src/app/shared/models/user';
import { AccountService } from 'src/app/account/account.service';
import { Observable } from 'rxjs';
declare const $: any;

@Component({
  selector: 'app-sade-bar',
  templateUrl: './sade-bar.component.html',
  styleUrls: ['./sade-bar.component.css']
})
export class SadeBarComponent implements OnInit, AfterContentInit {
  currentUser$: Observable<IUser>;
  userDetails: IUser;
  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.currentUser$ = this.accountService.currentUser$;
    this.accountService.currentUser$.subscribe(user => {
      this.userDetails = user;
    });
  }
  ngAfterContentInit() {
  }

  getMenus() {
     // tslint:disable-next-line:one-variable-per-declaration
  }

}
