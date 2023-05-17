import { Feed } from './feed.model';
import { Formula } from './formula.model';
import { Store } from './store.model';

export interface News {
  dateTimeFrom?: Date;
  dateTimeTo?: Date;
  subject?: string;
  content?: string;
  sourceUrl?: string;
  feedId?: number;
  organisationId?: number;
  marketId?: number;
  storeId?: number;
  formulaId?: number;
  feed?: Feed;
  store?: Store;
  formula?: Formula;
}
