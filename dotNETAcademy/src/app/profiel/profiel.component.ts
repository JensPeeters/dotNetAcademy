import { Component, OnInit } from '@angular/core';
import { MsalService } from '../services/msal.service';

@Component({
  selector: 'app-profiel',
  templateUrl: './profiel.component.html',
  styleUrls: ['./profiel.component.scss']
})
export class ProfielComponent implements OnInit {

  constructor(private msalService: MsalService) { }

  isUserLoggedIn() {
    return this.msalService.isLoggedIn();
  }

  userfirstname() {
    return this.msalService.getUserFirstName();
  }

  userfamilyname() {
    return this.msalService.getUserFamilyName();
  }

  useremail() {
    return this.msalService.getUserEmail();
  }

  editprofile() {
    return this.msalService.editProfile();
  }

  ngOnInit() {
  }

}
