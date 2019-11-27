import { ICursussen } from './ICursussen';

export interface IProduct {
    cursussen: ICursussen[];
    id: number;
    prijs: number;
    categorie: string;
    fotoURLCard: string;
    type: string;
    beschrijving: string;
    langeBeschrijving: string;
    titel: string;
}