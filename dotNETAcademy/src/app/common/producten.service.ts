import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductenService {

  constructor(private http: HttpClient) { }

  domain:string = "https://localhost:44334/api";

  public GetCursussen(){
    return this.http
    .get<Cursus[]>(`${this.domain}/cursus`)
    .toPromise();
  }
  public GetTrajecten(){
    return this.http
    .get<Traject[]>(`${this.domain}/traject`)
    .toPromise();
  }
}

export interface Cursus{
  cursusID: number;
  prijs: number;
  type: string;
  titel: string;
  beschrijving: string;
}

export interface Traject{
  trajectId: number;
  titel: string;
  type: string;
  cursussen: Cursus[];
  prijs: number;
  beschrijving: string;
}