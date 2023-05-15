import { Logo } from "./logo.model";

export interface Organisation {
    id?: number;
    name?: string;
    logoId?: number;
    logo?: Logo;
};
