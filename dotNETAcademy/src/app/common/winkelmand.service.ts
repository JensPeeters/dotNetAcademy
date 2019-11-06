import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class WinkelmandService {

  domain:string = "https://localhost:44334/api";

  constructor(private http: HttpClient) { }

  GetWinkelmand(UserId : string){
      this.http.get<IWinkelmand>(`${this.domain}/winkelmand/${UserId}`);
  }

}

export interface IKlant {
  id: number;
  azureId: string;
}

export interface ICursussen {
  id: number;
  prijs: number;
  categorie: string;
  fotoURLCard: string;
  type: string;
  beschrijving: string;
  langeBeschrijving: string;
  titel: string;
}

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

export interface IProducten {
  id: number;
  product: IProduct;
  aantal: number;
}

export interface IWinkelmand {
  id: number;
  datum: Date;
  klant: IKlant;
  producten: IProducten[];
  totaalPrijs: number;
}

