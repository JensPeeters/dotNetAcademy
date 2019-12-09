import { Component, OnInit } from '@angular/core';
import { MsalService } from '../services/msal.service';

@Component({
  selector: 'app-profiel',
  templateUrl: './profiel.component.html',
  styleUrls: ['./profiel.component.scss']
})
export class ProfielComponent implements OnInit {

  userFirstName: string;
  userFamilyName: string;
  userEmail: string;

  constructor(private msalService: MsalService) { }

  editprofile() {
    return this.msalService.editProfile();
  }

  ngOnInit() {
    if (this.msalService.isLoggedIn())
    {
    this.userFirstName = this.msalService.getUserFirstName();
    this.userFamilyName = this.msalService.getUserFamilyName();
    this.userEmail = this.msalService.getUserEmail();
    }
  }
}
