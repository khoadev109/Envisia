import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnInit,
  ViewChild,
} from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { Formula } from 'src/app/models/formula.model';
import { Organisation } from 'src/app/models/organisation.model';
import { Store } from 'src/app/models/store.model';
import { CommonService } from 'src/app/services/common.service';

declare const google: any;

type Notification = {
  date: string;
  subject?: string;
  content?: string;
};

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit, AfterViewInit {
  map: any;
  @ViewChild('mapElement') mapElement: any;

  organisations: Organisation[];
  formulas: Formula[];
  stores: Store[];

  notifications: Notification[] = [
    {
      date: '11/5/2023',
      subject: 'Abdijstraat 2, KAPELLE, NL',
      content: 'Start bouw Aldi supermarkt. Projectnummer: 552291.',
    },
    {
      date: '1/5/2023',
      subject: 'Prins Bernhardstraat 9, KOUDEKERK AAN DEN RIJN, NL',
      content: 'Opening Hoogvliet in Koudekerk aan den Rijn.',
    },
    {
      date: '26/4/2023',
      content:
        'Carrefour heeft in het eerste kwartaal de omzet in België op vergelijkbare basis met 9,9 procent opgekrikt tot 1,087 miljard euro.',
    },
  ];

  formulaAutoListId = 'formula-auto';
  orgAutoListId = 'org-auto';
  storeAutoListId = 'store-auto';

  logoSrc: string | SafeUrl = '';

  constructor(
    private sanitizer: DomSanitizer,
    private commonService: CommonService,
    private changeDetectorRef: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadSelectList();
  }

  ngAfterViewInit(): void {
    this.initMap();
  }

  initMap() {
    google.maps.importLibrary('maps').then((response: any) => {
      this.map = new response.Map(this.mapElement.nativeElement, {
        center: { lat: 52.1326, lng: 5.2913 },
        zoom: 8,
      });
    });
  }

  loadSelectList() {
    this.commonService.getOrganisations().subscribe((result: Formula[]) => {
      this.organisations = result;
      this.changeDetectorRef.detectChanges();
    });

    this.commonService.getFormulas().subscribe((result: Formula[]) => {
      this.formulas = result;
      this.changeDetectorRef.detectChanges();
    });

    this.commonService.getStores().subscribe((result: Store[]) => {
      this.stores = result;
      this.changeDetectorRef.detectChanges();
    });
  }

  searchFormula(event: any) {
    const value = event.target.value || '';
    const formula = this.formulas.find(
      (x) => x?.name?.toLowerCase() === value.toLowerCase()
    );
  }

  searchOrg(event: any) {
    const value = event.target.value || '';
    const org = this.organisations.find(
      (x) => x?.name?.toLowerCase() === value.toLowerCase()
    );
  }

  searchStore(event: any) {
    const value = event.target.value || '';
    const store = this.stores.find(
      (x) => x?.storeName?.toLowerCase() === value.toLowerCase()
    );
  }
}
