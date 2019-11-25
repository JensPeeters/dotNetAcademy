import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ICursus } from '../Interfaces/ICursus';
import { ITraject } from '../Interfaces/ITraject';

@Injectable({
  providedIn: 'root'
})
export class ProductenService {

  constructor(private http: HttpClient) { }

  domain: string = environment.domain;
  apiPageFilter: APIPageFilter = new APIPageFilter();

  public GetCursussen(filter?: string) {
    return this.http
    .get<ICursus[]>(`${this.domain}/cursus?${filter}&pageSize=${this.apiPageFilter.pageSize}&sortBy=${this.apiPageFilter.sortBy}&direction=${this.apiPageFilter.direction}&pageNumber=${this.apiPageFilter.pageNumber}`)
    .toPromise();
  }
  public GetTrajecten(filter?: string) {
    return this.http
    .get<ITraject[]>(`${this.domain}/traject?${filter}&pageSize=${this.apiPageFilter.pageSize}&sortBy=${this.apiPageFilter.sortBy}&direction=${this.apiPageFilter.direction}&pageNumber=${this.apiPageFilter.pageNumber}`)
    .toPromise();
  }

  GetCursusById(id: number) {
    return this.http
    .get<ICursus>(`${this.domain}/cursus/${id}`)
    .toPromise();
  }
  GetTrajectById(id: number) {
    return this.http
    .get<ITraject>(`${this.domain}/traject/${id}`)
    .toPromise();
  }

  GetCursusTypes(){
    return this.http.get<string[]>(`${this.domain}/cursus/types`);
  }
  GetTrajectTypes(){
    return this.http.get<string[]>(`${this.domain}/traject/types`);
  }
}
export class APIPageFilter{
  pageSize: number = 16;
  pageNumber: number = 0;
  sortBy: string = '';
  direction: string = 'asc';
}