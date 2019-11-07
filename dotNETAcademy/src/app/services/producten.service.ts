import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductenService {

  constructor(private http: HttpClient) { }

  //domain:string = "https://localhost:44334/api";
  domain: string = 'https://dotnetacademy-api.azurewebsites.net/api';
  pageSize: number = 16;
  pageNumber: number = 0;
  sortBy: string = '';
  direction: string = 'asc';

  cursusTypes: string[] = ['Aanbevolen', '.NET', 'Web'];
  TrajectTypes: string[] = ['Aanbevolen', 'Full Stack', 'Visual Studio', 'Angular'];

  public GetCursussen(filter?: string) {
    return this.http
    .get<Cursus[]>(`${this.domain}/cursus?${filter}&pageSize=${this.pageSize}&sortBy=${this.sortBy}&direction=${this.direction}&pageNumber=${this.pageNumber}`)
    .toPromise();
  }
  public GetTrajecten(filter?: string) {
    return this.http
    .get<Traject[]>(`${this.domain}/traject?${filter}&pageSize=${this.pageSize}&sortBy=${this.sortBy}&direction=${this.direction}&pageNumber=${this.pageNumber}`)
    .toPromise();
  }

  GetCursusById(id: number) {
    return this.http
    .get<Cursus>(`${this.domain}/cursus/${id}`)
    .toPromise();
  }
  GetTrajectById(id: number) {
    return this.http
    .get<Traject>(`${this.domain}/traject/${id}`)
    .toPromise();
  }
}

export interface Cursus {
  cursusID: number;
  prijs: number;
  fotoURLCard: string;
  type: string;
  beschrijving: string;
  langeBeschrijving: string;
  titel: string;
  categorie: string;
}

export interface Traject {
  trajectId: number;
  titel: string;
  type: string;
  beschrijving: string;
  langeBeschrijving: string;
  fotoURLCard: string;
  cursussen: Cursus[];
  prijs: number;
  categorie: string;
}