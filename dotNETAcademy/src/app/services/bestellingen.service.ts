import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IBestelling } from '../Interfaces/IBestelling';

@Injectable({
  providedIn: 'root'
})
export class BestellingenService {

  domain: string = environment.domain;

  httpOptions = {
    headers: new HttpHeaders({
      'Authorization': 'Bearer ' + sessionStorage.getItem('b2c.access.token')
    })};

  constructor(private http: HttpClient) { }

  GetBestellingen(UserId: string) {
    return this.http
    .get<IBestelling[]>(`${this.domain}/bestelling/klant/${UserId}`, this.httpOptions);
  }

  GetBestellingById(bestellingId: number) {
    return this.http
    .get<IBestelling>(`${this.domain}/bestelling/${bestellingId}`, this.httpOptions);
  }

  PostBestelling(UserId: string) {
    return this.http
    .post<IBestelling>(`${this.domain}/bestelling/klant/${UserId}`, '', this.httpOptions);
  }
}
