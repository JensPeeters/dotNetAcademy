import { IKlant } from './IKlant';
import { IProducten } from './IProducten';

export interface IWinkelmand {
    id: number;
    datum: Date;
    klant: IKlant;
    producten: IProducten[];
    totaalPrijs: number;
}