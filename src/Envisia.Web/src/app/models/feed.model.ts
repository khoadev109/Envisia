import { News } from "./news.model";

export interface Feed {
  id?: number;
  lastModifiedDate?: Date;
  sourceUrl?: string;
  newsList: News[];
}
