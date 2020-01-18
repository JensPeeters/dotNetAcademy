import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IWinkelmand } from '../Interfaces/IWinkelmand';

@Injectable({
  providedIn: 'root'
})
export class WinkelmandService {

  domain: string = environment.domain;
  private messageSource = new BehaviorSubject('0');
  aantalItems = this.messageSource.asObservable();

  constructor(private http: HttpClient) {

  }

  ChangeAantal(aantal: string) {
    this.messageSource.next(aantal);
  }

  GetWinkelmand(UserId: string) {
    return this.http.get<IWinkelmand>(`${this.domain}/winkelwagen/${UserId}`);
  }

  DeleteFromWinkelmand(UserId: string, ProdId: number) {
    return this.http.delete<IWinkelmand>(`${this.domain}/winkelwagen/${UserId}/product/${ProdId}`);
  }

  UpdateAantalProduct(UserId: string, ProdId: number, Aantal: number) {
    return this.http.put<IWinkelmand>(`${this.domain}/winkelwagen/${UserId}/product/${ProdId}/${Aantal}`, null);
  }

  AddToWinkelmand(UserId: string, Type: string, ProdId: number, Aantal: number) {
    return this.http.post<IWinkelmand>(`${this.domain}/winkelwagen/${UserId}/product/${Type}/${ProdId}/${Aantal}`, null);
  }
}
