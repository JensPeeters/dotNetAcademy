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
    headers: new HttpHeaders()};

  constructor(private http: HttpClient) { }

  GetBestellingen(UserId: string) {
    this.httpOptions.headers =
    this.httpOptions.headers.set('Authorization', 'Bearer ' + sessionStorage.getItem('b2c.access.token'));
    return this.http
    .get<IBestelling[]>(`${this.domain}/bestelling/klant/${UserId}`);
  }

  GetBestellingById(bestellingId: number) {
    this.httpOptions.headers =
    this.httpOptions.headers.set('Authorization', 'Bearer ' + sessionStorage.getItem('b2c.access.token'));
    return this.http
    .get<IBestelling>(`${this.domain}/bestelling/${bestellingId}`);
  }

  PostBestelling(UserId: string) {
    this.httpOptions.headers =
    this.httpOptions.headers.set('Authorization', 'Bearer ' + sessionStorage.getItem('b2c.access.token'));
    return this.http
    .post<IBestelling>(`${this.domain}/bestelling/klant/${UserId}`,"");
  }
}