import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { AccountService } from '../../../services/account.service';
import { AppUser, AppUserProfileRequest } from '../../../models/user.model';

@Component({
  selector: 'app-profile-details',
  templateUrl: './profile-details.component.html',
})
export class ProfileDetailsComponent implements OnInit {
  isLoading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  isLoading: boolean;
  private unsubscribe: Subscription[] = [];

  user: AppUser = {
    displayName: "",
    email: "",
    firstName: "",
    lastName: "",
    userIdentifier: ""
  }

  constructor(
    private changeDetectorRef: ChangeDetectorRef,
    private accountService: AccountService
  ) {
    const loadingSubscr = this.isLoading$
      .asObservable()
      .subscribe((res) => (this.isLoading = res));
    this.unsubscribe.push(loadingSubscr);
  }

  ngOnInit(): void {
    this.getUserProfile();
  }

  getUserProfile() {
    this.accountService.getProfile().subscribe((appUser: AppUser) => {
      this.user = appUser;
      this.changeDetectorRef.detectChanges();
    });
  }

  saveSettings() {
    this.isLoading$.next(true);

    const request: AppUserProfileRequest = {
      displayName: this.user.displayName,
      email: this.user.email,
      firstName: this.user.firstName,
      lastName: this.user.lastName,
      userIdentifier: this.user.userIdentifier
    }

    try {
      this.accountService.updateProfile(request).subscribe((appUser: AppUser) => {
        this.user = appUser;

        this.isLoading$.next(false);
        this.changeDetectorRef.detectChanges();
      });
    } catch (error) {
      console.log('Save profile error', error);
      this.isLoading$.next(false);
    }
  }

  ngOnDestroy() {
    this.unsubscribe.forEach((sb) => sb.unsubscribe());
  }
}
