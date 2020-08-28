import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as moment from 'moment';
import { tap, map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { AuthResponse } from './auth-reponse.model';
import { User } from 'src/app/login/user.model';
import { BehaviorSubject, Observable, of } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  userLoggedIn = new BehaviorSubject(false);

  constructor(private http: HttpClient) {}

  login(userData: User) {
    this.userLoggedIn.next(true);
    return this.http
      .post<AuthResponse>(`${environment.studyApiEndpoint}/login`, userData)
      .pipe(map(this.setSession));
  }

  setSession(loginResponse: AuthResponse) {
    const expiresAt = moment(new Date(loginResponse.expiresAt), 'second');
    localStorage.setItem('access_token', loginResponse.accessToken);
    localStorage.setItem('expires_at', JSON.stringify(expiresAt.valueOf()));
    localStorage.setItem('user_role', loginResponse.user.role);
  }

  public isLoggedIn() {
    return this.getAccessToken() && moment().isBefore(this.getExpiration());
  }

  getUserRole() {
    return localStorage.getItem('user_role');
  }

  logoutWhenSessionExpired() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('expires_at');
    localStorage.removeItem('user_role');
    this.userLoggedIn.next(false);
    this.setLoggedIn(false);
  }

  logout() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('expires_at');
    localStorage.removeItem('user_role');
    this.userLoggedIn.next(false);
    this.setLoggedIn(false);
  }

  getExpiration() {
    const expiration = localStorage.getItem('expires_at');
    const expiresAt = JSON.parse(expiration);
    return moment(expiresAt);
  }

  getAccessToken() {
    return localStorage.getItem('access_token');
  }

  getLoggedIn(): Observable<boolean> {
    const logged = this.isLoggedIn();
    this.setLoggedIn(logged);
    return this.userLoggedIn.asObservable();
  }

  getLoggedInValue(): boolean {
    return this.userLoggedIn.getValue();
  }

  setLoggedIn(val: boolean) {
    this.userLoggedIn.next(val);
  }
}
