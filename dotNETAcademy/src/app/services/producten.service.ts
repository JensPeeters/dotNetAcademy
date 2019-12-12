import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ICursus } from '../Interfaces/ICursus';
import { ITraject } from '../Interfaces/ITraject';
import { IProduct } from '../Interfaces/IProduct';

@Injectable({
  providedIn: 'root'
})
export class ProductenService {

  constructor(private http: HttpClient) { }

  domain: string = environment.domain;
  apiPageFilter: APIPageFilter = new APIPageFilter();

  public GetBuyableCursussen(filter?: string) {
    return this.http
    .get<IProduct[]>(`${this.domain}/cursus/buyable?${filter}&pageSize=${this.apiPageFilter.pageSize}&sortBy=${this.apiPageFilter.sortBy}&direction=${this.apiPageFilter.direction}&pageNumber=${this.apiPageFilter.pageNumber}`)
    .toPromise();
  }
  public GetBuyableTrajecten(filter?: string) {
    return this.http
    .get<IProduct[]>(`${this.domain}/traject/buyable?${filter}&pageSize=${this.apiPageFilter.pageSize}&sortBy=${this.apiPageFilter.sortBy}&direction=${this.apiPageFilter.direction}&pageNumber=${this.apiPageFilter.pageNumber}`)
    .toPromise();
  }
  public UpdateProduct(product : IProduct){
    if(product.categorie == "Cursus"){
      return this.http.put<IProduct>(`${this.domain}/cursus/${product.id}`, product);
    }
    else {
      return this.http.put<IProduct>(`${this.domain}/traject/${product.id}`, product);
    }
  }

  public AddProduct(product : IProduct){
    if(product.categorie == "Cursus"){
      return this.http.post<IProduct>(`${this.domain}/cursus`, product);
    }
    else {
      return this.http.post<IProduct>(`${this.domain}/traject`, product);
    }

  }

  public GetCursussenAdmin(filter?: string) {
    return this.http
    .get<IProduct[]>(`${this.domain}/cursus?${filter}&pageSize=${this.apiPageFilter.pageSize}&sortBy=${this.apiPageFilter.sortBy}&direction=${this.apiPageFilter.direction}&pageNumber=${this.apiPageFilter.pageNumber}`)
    .toPromise();
  }
  public GetTrajectenAdmin(filter?: string) {
    return this.http
    .get<IProduct[]>(`${this.domain}/traject?${filter}&pageSize=${this.apiPageFilter.pageSize}&sortBy=${this.apiPageFilter.sortBy}&direction=${this.apiPageFilter.direction}&pageNumber=${this.apiPageFilter.pageNumber}`)
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

  DeleteProduct(product : IProduct){
    return this.http.delete(`${this.domain}/${product.categorie}/${product.id}`);
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