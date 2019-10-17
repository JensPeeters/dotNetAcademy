import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductenService {

  constructor(private http: HttpClient) { }

  domain:string = "https://localhost:44334/api";
  pageSize: number = 16;
  pageNumber: number = 0;
  sortBy: string = "";
  direction: string = "asc";

  public GetCursussen(filter?: string){
    return this.http
    .get<Cursus[]>(`${this.domain}/cursus?${filter}&pageSize=${this.pageSize}&sortBy=${this.sortBy}&direction=${this.direction}&pageNumber=${this.pageNumber}`)
    .toPromise();
  }
  public GetTrajecten(filter?: string){
    return this.http
    .get<Traject[]>(`${this.domain}/traject?${filter}&pageSize=${this.pageSize}&sortBy=${this.sortBy}&direction=${this.direction}&pageNumber=${this.pageNumber}`)
    .toPromise();
  }
}

export interface Cursus{
  cursusID: number;
  prijs: number;
  fotoURLCard: string;
  type: string;
  beschrijving: string;
  langeBeschrijving: string;
  titel: string;
}

export interface Traject{
  trajectId: number;
  titel: string;
  type: string;
  beschrijving: string;
  langeBeschrijving: string;
  fotoURLCard: string;
  cursussen: Cursus[];
  prijs: number;
}