import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as moment from 'moment';
import { tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { AuthResponse } from './auth-reponse.model';
import { User } from 'src/app/login/user.model';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  constructor(private http: HttpClient) {}

  login(userData: User) {

    return this.http
      .post<AuthResponse>(`${environment.studyApiEndpoint}/login`, userData)
      .pipe(tap(this.setSession));
  }

  setSession(loginResponse: AuthResponse) {
    const expiresAt = moment(new Date(loginResponse.expiresAt), 'second');
    localStorage.setItem('access_token', loginResponse.accessToken);
    localStorage.setItem('expires_at', JSON.stringify(expiresAt.valueOf()));

  }

  public isLoggedIn() {
    return this.getAccessToken() && moment().isBefore(this.getExpiration());
  }

  logoutWhenSessionExpired() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('expires_at');
  }

  logout() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('expires_at');
  }

  getExpiration() {
    const expiration = localStorage.getItem('expires_at');
    const expiresAt = JSON.parse(expiration);
    return moment(expiresAt);
  }

  getAccessToken() {
    return localStorage.getItem('access_token');
  }
}
