import { HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
} from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError, switchMap, tap } from 'rxjs/operators';
import { getAccessToken, setTokenAndIdentifiers } from '../token-helper';
import { AuthHTTPService } from './auth-http.service';
import { AuthService } from './auth.service';
import { TokenResponse } from '../models/login.model';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(
    private authHttpService: AuthHTTPService,
    private authService: AuthService
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    let authRequest = request;

    if (request.url.includes('/auth/login')) {
      return next.handle(request);
    }

    authRequest = this.addHeader(request, getAccessToken());

    return next.handle(authRequest).pipe(
      catchError((error) => {
        if (error.status === 401) {
          return this.handleRefreshToken(request, next);
        }
        return throwError(error);
      })
    );
  }

  handleRefreshToken(request: HttpRequest<any>, next: HttpHandler) {
    return this.authHttpService.generateRefreshToken().pipe(
      tap((tokenResponse: TokenResponse) => {
        setTokenAndIdentifiers(tokenResponse);
      }),
      switchMap((tokenResponse: TokenResponse) => {
        return next.handle(this.addHeader(request, tokenResponse.accessToken));
      }),
      catchError((error) => {
        return throwError(error);
      })
    );
  }

  addHeader(request: HttpRequest<any>, token: any, fileUpload?: boolean) {
    return request.clone({
      setHeaders: {
        Authorization: 'Bearer ' + token,
        'Content-Type': 'application/json',
      },
    });
  }
}
