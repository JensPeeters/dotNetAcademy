import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WinkelmandService {

  domain: string = "https://localhost:44334/api";
  //domain: string = 'https://dotnetacademy-api.azurewebsites.net/api';
  private messageSource = new BehaviorSubject('0');
  aantalItems = this.messageSource.asObservable();

  constructor(private http: HttpClient) {

  }

  ChangeAantal(aantal: string) {
    this.messageSource.next(aantal)
  }

  GetWinkelmand(UserId: string) {
    return this.http.get<IWinkelmand>(`${this.domain}/winkelwagen/${UserId}`);
  }

  AddToWinkelmand(UserId: string, Type: string, ProdId: number, Aantal: number){
    return this.http.post<IWinkelmand>(`${this.domain}/winkelwagen/${UserId}/product/${Type}/${ProdId}/${Aantal}`,null);
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

