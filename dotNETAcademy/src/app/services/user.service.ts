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
    this.httpOptions.headers =
    this.httpOptions.headers.set('Authorization', 'Bearer ' + sessionStorage.getItem('b2c.access.token'));
    return this.http.post(`${this.domain}/klant/${UserId}`, null);
  }

  saveAdminInDb(UserId) {
    this.httpOptions.headers =
    this.httpOptions.headers.set('Authorization', 'Bearer ' + sessionStorage.getItem('b2c.access.token'));
    return this.http.post(`${this.domain}/admin/${UserId}`, null);
  }

  deleteKlantInDb(UserId) {
    this.httpOptions.headers =
    this.httpOptions.headers.set('Authorization', 'Bearer ' + sessionStorage.getItem('b2c.access.token'));
    return this.http.delete(`${this.domain}/klant/${UserId}`);
  }

  deleteAdminInDb(UserId) {
    this.httpOptions.headers =
    this.httpOptions.headers.set('Authorization', 'Bearer ' + sessionStorage.getItem('b2c.access.token'));
    return this.http.delete(`${this.domain}/admin/${UserId}`);
  }

  updateAdminToKlant(UserId) {
    this.httpOptions.headers =
    this.httpOptions.headers.set('Authorization', 'Bearer ' + sessionStorage.getItem('b2c.access.token'));
    return this.http.put(`${this.domain}/admin/toklant/${UserId}`, null);
  }

  updateKlantToAdmin(UserId) {
    this.httpOptions.headers =
    this.httpOptions.headers.set('Authorization', 'Bearer ' + sessionStorage.getItem('b2c.access.token'));
    return this.http.put(`${this.domain}/klant/toadmin/${UserId}`, null);
  }

  isklant(UserId) {
    this.httpOptions.headers =
    this.httpOptions.headers.set('Authorization', 'Bearer ' + sessionStorage.getItem('b2c.access.token'));
    return this.http.get<IKlant>(`${this.domain}/klant/${UserId}`, this.httpOptions);
  }

  isadmin(UserId) {
    /*console.log(sessionStorage.getItem('b2c.access.token'));
    const token = 'Bearer ' + sessionStorage.getItem('b2c.access.token');
    console.log(token);
    this.httpOptions.headers =
    this.httpOptions.headers.set('Authorization', token);*/
    console.log(this.httpOptions.headers);
    return this.http.get<IAdmin>(`${this.domain}/admin/${UserId}`, this.httpOptions);
  }
}

export interface IAdmin {
  azureId: string;
}
