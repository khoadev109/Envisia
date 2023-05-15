import {
  ChangeDetectorRef,
  Component,
  HostBinding,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { Subscription } from 'rxjs';
import { TranslationService } from '../../../../../../modules/i18n';
import { AuthService, UserType } from '../../../../../../modules/auth';
import { AccountService } from 'src/app/modules/account/services/account.service';
import { AppUser } from 'src/app/modules/account/models/user.model';

@Component({
  selector: 'app-user-inner',
  templateUrl: './user-inner.component.html',
})
export class UserInnerComponent implements OnInit, OnDestroy {
  @HostBinding('class')
  class = `menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-600 menu-state-bg menu-state-primary fw-bold py-4 fs-6 w-275px`;
  @HostBinding('attr.data-kt-menu') dataKtMenu = 'true';

  language: LanguageFlag;
  // user$: Observable<UserType>;

  langs = languages;

  profile: AppUser = {
    displayName: '',
    email: '',
    firstName: '',
    lastName: '',
    userIdentifier: '',
  };

  private unsubscribe: Subscription[] = [];

  constructor(
    private changeDetectorRef: ChangeDetectorRef,
    private translationService: TranslationService,
    private authService: AuthService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.setLanguage(this.translationService.getSelectedLanguage());
    this.getProfile();
  }

  getProfile() {
    this.accountService.getProfile().subscribe((appUser: AppUser) => {
      this.profile = appUser;
      this.changeDetectorRef.detectChanges();
    });
  }

  logout() {
    this.authService.logout();
  }

  selectLanguage(lang: string) {
    this.translationService.setLanguage(lang);
    this.setLanguage(lang);
    // document.location.reload();
  }

  setLanguage(lang: string) {
    this.langs.forEach((language: LanguageFlag) => {
      if (language.lang === lang) {
        language.active = true;
        this.language = language;
      } else {
        language.active = false;
      }
    });
  }

  ngOnDestroy() {
    this.unsubscribe.forEach((sb) => sb.unsubscribe());
  }
}

interface LanguageFlag {
  lang: string;
  name: string;
  flag: string;
  active?: boolean;
}

const languages = [
  {
    lang: 'en',
    name: 'English',
    flag: './assets/media/flags/united-states.svg',
  },
  {
    lang: '	nl',
    name: 'Dutch',
    flag: './assets/media/flags/netherlands.svg',
  },

  // {
  //   lang: 'zh',
  //   name: 'Mandarin',
  //   flag: './assets/media/flags/china.svg',
  // },
  // {
  //   lang: 'es',
  //   name: 'Spanish',
  //   flag: './assets/media/flags/spain.svg',
  // },
  // {
  //   lang: 'ja',
  //   name: 'Japanese',
  //   flag: './assets/media/flags/japan.svg',
  // },
  // {
  //   lang: 'de',
  //   name: 'German',
  //   flag: './assets/media/flags/germany.svg',
  // },
  // {
  //   lang: 'fr',
  //   name: 'French',
  //   flag: './assets/media/flags/france.svg',
  // },
];
