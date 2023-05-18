import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnInit,
  ViewChild,
} from '@angular/core';
import { SafeUrl } from '@angular/platform-browser';
import { Feed } from 'src/app/models/feed.model';
import { Formula } from 'src/app/models/formula.model';
import { Organisation } from 'src/app/models/organisation.model';
import { Store } from 'src/app/models/store.model';
import { CommonService } from 'src/app/services/common.service';

declare const google: any;

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit, AfterViewInit {
  map: any;
  @ViewChild('mapElement') mapElement: any;

  public feedNewsList: Feed[];
  public page: number = 1;
  public count: number = 0;
  public tableSize: number = 3;
  public tableSizes: any = [3, 6, 9, 12];

  organisations: Organisation[];
  formulas: Formula[];
  stores: Store[];

  logoSrc: string | SafeUrl = '';

  constructor(
    private commonService: CommonService,
    private changeDetectorRef: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadSelectList();
    this.loadAllFeedNews();
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

  loadAllFeedNews() {
    this.commonService.getAllFeedNews().subscribe((result: Feed[]) => {
      this.feedNewsList = result;
      this.changeDetectorRef.detectChanges();
    });
  }

  onTableDataChange(event: any) {
    this.page = event;
    this.loadAllFeedNews();
  }

  onTableSizeChange(event: any): void {
    this.tableSize = event.target.value;
    this.page = 1;
    this.loadAllFeedNews();
  }
}
