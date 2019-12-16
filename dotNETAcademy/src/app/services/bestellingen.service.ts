import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IBestelling } from '../Interfaces/IBestelling';

@Injectable({
  providedIn: 'root'
})
export class BestellingenService {

  domain: string = environment.domain;

  constructor(private http: HttpClient) { }

  GetBestellingen(UserId: string) {
    return this.http
    .get<IBestelling[]>(`${this.domain}/bestelling/klant/${UserId}`)
    .toPromise();
  }

  GetBestellingById(bestellingId: number){
    return this.http
    .get<IBestelling>(`${this.domain}/bestelling/${bestellingId}`);
  }

  PostBestelling(UserId: string){
    return this.http
    .post<IBestelling>(`${this.domain}/bestelling/klant/${UserId}`,"");
  }
}