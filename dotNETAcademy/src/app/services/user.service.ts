import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IKlant } from '../Interfaces/IKlant';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  domain: string = environment.domain;

  httpOptions = {
    headers: new HttpHeaders({
      'Authorization': 'Bearer ' + sessionStorage.getItem('b2c.access.token')
    })};

  saveKlantInDb(UserId) {
    return this.http.post(`${this.domain}/klant/${UserId}`, null, this.httpOptions);
  }

  saveAdminInDb(UserId) {
    return this.http.post(`${this.domain}/admin/${UserId}`, null, this.httpOptions);
  }

  deleteKlantInDb(UserId) {
    return this.http.delete(`${this.domain}/klant/${UserId}`, this.httpOptions);
  }

  deleteAdminInDb(UserId) {
    return this.http.delete(`${this.domain}/admin/${UserId}`, this.httpOptions);
  }

  updateAdminToKlant(UserId) {
    return this.http.put(`${this.domain}/admin/toklant/${UserId}`, null, this.httpOptions);
  }

  updateKlantToAdmin(UserId) {
    return this.http.put(`${this.domain}/klant/toadmin/${UserId}`, null, this.httpOptions);
  }

  isklant(UserId) {
    return this.http.get<IKlant>(`${this.domain}/klant/${UserId}`, this.httpOptions);
  }

  isadmin(UserId) {
    return this.http.get<IAdmin>(`${this.domain}/admin/${UserId}`, this.httpOptions);
  }
}

export interface IAdmin {
  azureId: string;
}
