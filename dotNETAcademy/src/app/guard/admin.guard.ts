import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { MsalService } from '../services/msal.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  constructor(private msalService: MsalService, private router: Router) {}

  canActivate() {
    return true;
    if (this.msalService.isLoggedIn()) {
      if (this.msalService.admin) {
        return true;
      } else {
        this.router.navigateByUrl('no-admin');
        return false;
      }
    }
    this.msalService.login();
    return false;
  }
}
