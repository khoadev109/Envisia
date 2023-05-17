import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Formula } from '../models/formula.model';
import { Store } from '../models/store.model';
import { Organisation } from '../models/organisation.model';
import { News } from '../models/news.model';
import { Feed } from '../models/feed.model';

@Injectable({
  providedIn: 'root',
})
export class CommonService {
  private API_ORGANISATION_URL = `${environment.apiUrl}/organisation`;
  private API_FORMULA_URL = `${environment.apiUrl}/formula`;
  private API_STORE_URL = `${environment.apiUrl}/store`;
  private API_FEED_NEWS_URL = `${environment.apiUrl}/feedNews`;

  constructor(private httpClient: HttpClient) {
  }

  getOrganisations() : Observable<Organisation[]> {
    return this.httpClient.get<Organisation[]>(this.API_ORGANISATION_URL);
  }

  getFormulas() : Observable<Formula[]> {
    return this.httpClient.get<Formula[]>(this.API_FORMULA_URL);
  }

  getStores() : Observable<Store[]> {
    return this.httpClient.get<Store[]>(this.API_STORE_URL);
  }

  getAllFeedNews() : Observable<Feed[]> {
    return this.httpClient.get<Feed[]>(this.API_FEED_NEWS_URL);
  }
}
