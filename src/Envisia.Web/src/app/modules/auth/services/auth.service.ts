import { Injectable, OnDestroy } from '@angular/core';
import { Observable, BehaviorSubject, Subscription } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserModel } from '../models/user.model';
import { AuthHTTPService } from './auth-http.service';
import { Router } from '@angular/router';
import { TokenResponse } from '../models/login.model';
import {
  removeTokensAndIdentifiers as removeTokensAndIdentifiers,
  setTokenAndIdentifiers,
  getAccessToken,
} from '../token-helper';

export type UserType = UserModel | undefined;

@Injectable({
  providedIn: 'root',
})
export class AuthService implements OnDestroy {
  // private fields
  private unsubscribe: Subscription[] = []; // Read more: => https://brianflove.com/2016/12/11/anguar-2-unsubscribe-observables/

  // public fields
  currentUser$: Observable<UserType>;
  isLoading$: Observable<boolean>;
  currentUserSubject: BehaviorSubject<UserType>;
  isLoadingSubject: BehaviorSubject<boolean>;

  get currentUserValue(): UserType {
    return this.currentUserSubject.value;
  }

  set currentUserValue(user: UserType) {
    this.currentUserSubject.next(user);
  }

  constructor(
    private router: Router,
    private jwtHelperService: JwtHelperService,
    private authHttpService: AuthHTTPService
  ) {
    this.isLoadingSubject = new BehaviorSubject<boolean>(false);
    this.currentUserSubject = new BehaviorSubject<UserType>(undefined);
    this.currentUser$ = this.currentUserSubject.asObservable();
    this.isLoading$ = this.isLoadingSubject.asObservable();
  }

  login(email: string, password: string): Observable<TokenResponse> {
    return this.authHttpService.login(email, password);
  }

  logout() {
    removeTokensAndIdentifiers();
    this.router.navigate(['/auth/login']);
  }

  isAuthenticated() {
    const accessToken: string | null = getAccessToken();
    if (accessToken && !this.jwtHelperService.isTokenExpired(accessToken)) {
      return true;
    } else {
      return false;
    }
  }

  ngOnDestroy() {
    this.unsubscribe.forEach((sb) => sb.unsubscribe());
  }
}
