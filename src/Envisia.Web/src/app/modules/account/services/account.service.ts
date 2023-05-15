import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { AppUser, AppUserProfileRequest } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private API_USERS_URL = `${environment.apiUrl}/account/`;

  constructor(private httpClient: HttpClient) {
  }

  getProfile(): Observable<AppUser> {
    return this.httpClient.get<AppUser>(this.API_USERS_URL + 'profile');
  }

  updateProfile(payload: AppUserProfileRequest): Observable<AppUser> {
    const body = JSON.stringify(payload);
    return this.httpClient.post<AppUser>(this.API_USERS_URL + 'profile', body);
  }
}
