import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  urlAPI = 'https://dotnetacademy-api.azurewebsites.net/api';
  //urlAPI = 'https://localhost:44315/api';

  saveUserInDb(UserId) {
    return this.http.post(`${this.urlAPI}/klant/${UserId}`, null);
  }

  isadmin(UserId) {
    return this.http.get<IAdmin>(`${this.urlAPI}/admin/${UserId}`);
  }
}

export interface IAdmin {
  azureId: string;
}
