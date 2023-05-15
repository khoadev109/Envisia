import { Logo } from "./logo.model";

export interface Store {
    id?: number;
    storeName?: string;
    address?: string;
    logoId?: number;
    logo?: Logo;
};
