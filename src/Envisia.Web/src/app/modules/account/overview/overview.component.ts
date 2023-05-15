import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { AppUser } from '../models/user.model';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
})
export class OverviewComponent implements OnInit {
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
  ) {}

  ngOnInit(): void {
    this.getUserProfile();
  }

  getUserProfile() {
    this.accountService.getProfile().subscribe((appUser: AppUser) => {
      this.user = appUser;
      this.changeDetectorRef.detectChanges();
    });
  }
}
