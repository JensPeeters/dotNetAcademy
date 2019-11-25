import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  domain: string = environment.domain;

  saveUserInDb(UserId) {
    return this.http.post(`${this.domain}/klant/${UserId}`, null);
  }

  isadmin(UserId) {
    return this.http.get<IAdmin>(`${this.domain}/admin/${UserId}`);
  }
}

export interface IAdmin {
  azureId: string;
}
