import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MsalService } from '../services/msal.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnInit {

  constructor(private router: Router, private msalService: MsalService) { }
    searchParam: string = '';

  ngOnInit() {
  }

  Search() {
    this.router.navigateByUrl(`producten/zoekresultaten/${this.searchParam}`);
  }

  userfirstname() {
    return this.msalService.getUserFirstName();
  }

  useremail() {
    let useremail = this.msalService.getUserEmail();
    return useremail;
  }

  login() {
    this.msalService.login();
  }

  signup() {
    this.msalService.signup();
  }

  logout() {
    this.msalService.logout();
  }

  isUserLoggedIn() {
    return this.msalService.isLoggedIn();
  }

}
