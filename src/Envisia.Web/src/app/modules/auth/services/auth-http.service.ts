import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { LoginRequest, TokenResponse } from '../models/login.model';
import { getRefreshToken, getAccessToken } from '../token-helper';

const API_ACCOUNT_URL = `${environment.apiUrl}/account`;

@Injectable({
  providedIn: 'root',
})
export class AuthHTTPService {
  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<TokenResponse> {
    const request: LoginRequest = {
      userName: email,
      password: password
    };

    const body = JSON.stringify(request);

    return this.http.post<TokenResponse>(`${API_ACCOUNT_URL}/login`, body);
  }

  generateRefreshToken() {
    const payload = {
      accessToken: getAccessToken(),
      refreshToken: getRefreshToken()
    };

    const body = JSON.stringify(payload);

    return this.http.post<TokenResponse>(`${API_ACCOUNT_URL}/refreshtoken`, body);
  }
}
