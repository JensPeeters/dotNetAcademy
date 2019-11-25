import { IProducten } from './IProducten';
import { IKlant } from './IKlant';

export interface IBestelling {
    id: number;
    datum: Date;
    producten: IProducten[];
    totaalPrijs: number;
    klant: IKlant;
}