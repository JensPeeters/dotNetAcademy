import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProducten, IKlant } from '../common/winkelmand.service';

@Injectable({
  providedIn: 'root'
})
export class BestellingenService {

  //domain: string = "https://localhost:44334/api";
  domain: string = 'https://dotnetacademy-api.azurewebsites.net/api';

  constructor(private http: HttpClient) { }

  GetBestellingen(UserId: string) {
    return this.http
    .get<IBestelling[]>(`${this.domain}/bestelling/${UserId}`)
    .toPromise();
  }
}
export interface IBestelling {
    id: number;
    datum: Date;
    producten: IProducten[];
    totaalPrijs: number;
    klant: IKlant;
}