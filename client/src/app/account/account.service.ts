import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ReplaySubject, of } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { IUser } from '../shared/models/user';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  occupation: string;
  currentUser: IUser;
  private currentUserSource: ReplaySubject<IUser> = new ReplaySubject<IUser>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  loadCurrentUser(token: string) {
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get(this.baseUrl + 'account', {headers}).pipe(
      map((user: IUser) => {
        if (user) {
          this.currentUser = user;
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          this.occupation = user.occupation;
          this.currentUserSource.next(user);
        }
      })
    );

  }

  login(values: any) {
    return this.http.post(this.baseUrl + 'account/login', values).pipe(
      map((user: IUser) => {
        if (user) {
          this.currentUser = user;
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          this.occupation = user.occupation;
          this.currentUserSource.next(user);
        }
      })
    );
  }

  registerClient(values: any) {
    const data = {
      displayName : values.contactName,
      email: values.email,
      password: values.password
    };
    return this.http.post(this.baseUrl + 'Account/registerClient', data).pipe(
      map((user: IUser) => {
        if (user) {
          this.currentUser = user;
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          return user;
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUser = null;
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }
  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }
  checkEmailExists(email: string) {
    return this.http.get(this.baseUrl + 'account/emailexists?email=' + email);
  }
  changeMemberPhoto(photoUrl: string) {
    this.currentUser.avatar = photoUrl;
    this.currentUserSource.next(this.currentUser);
  }
  registerCandidates(values: any) {
    const data = {
      displayName : values.firstName + ' ' + values.lastName,
      email: values.email,
      password: values.password
    };
    return this.http.post(this.baseUrl + 'Account/registerCandidate', data).pipe(
      map((user: IUser) => {
        if (user) {
          this.currentUser = user;
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          return user;
        }
      })
    );
  }

  roleMatch(allowedRoles): boolean {
    let isMatch = false;
    const userRoles = this.decodedToken.role as Array<string>;
    allowedRoles.forEach(element => {
      if (userRoles.includes(element)) {
        isMatch = true;
        return;
      }
    });
    return isMatch;
  }

}
