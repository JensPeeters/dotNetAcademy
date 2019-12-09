import { ICursus } from './ICursus';

export interface ITraject {
    trajectId: number;
    titel: string;
    type: string;
    beschrijving: string;
    langeBeschrijving: string;
    fotoURLCard: string;
    cursussen: ICursus[];
    prijs: number;
    categorie: string;
}