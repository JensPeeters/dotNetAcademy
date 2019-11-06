import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MsalService } from '../services/msal.service';
import { WinkelmandService } from '../common/winkelmand.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnInit {

  constructor(private router: Router, private msalService: MsalService,
    private winkelmandService: WinkelmandService) { }
  searchParam: string = '';
  aantalItems: number = 0;
  UserId: string;
  ngOnInit() {
    if(this.msalService.isLoggedIn()){
      this.GetUserObjectId();
    }
    this.winkelmandService.GetWinkelmand(this.UserId).subscribe(res => {
        this.aantalItems = res.producten.length;
    })
  }
  GetUserObjectId(){
    this.UserId = this.msalService.getUserObjectId();
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
