import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { IBestelling } from 'src/app/Interfaces/IBestelling';
import { BestellingenService } from 'src/app/services/bestellingen.service';

@Component({
  selector: 'app-user-report',
  templateUrl: './user-report.component.html',
  styleUrls: ['./user-report.component.scss']
})
export class UserReportComponent implements OnInit {

  azureIdParam: string;
  userType: string;
  azureIdEmpty: boolean;
  userDoesntExists: boolean;
  Bestellingen: IBestelling[] = [];

  constructor(private userService: UserService, private bestService: BestellingenService) { }

  ngOnInit() {
    this.ResetParameters();
  }

  ResetParameters() {
    this.azureIdParam = '';
    this.userType = '';
    this.azureIdEmpty = false;
    this.userDoesntExists = false;
    this.Bestellingen = [];
  }

  SearchUser() {
    if (this.azureIdParam === '') {
      this.azureIdEmpty = true;
    } else {
      this.azureIdEmpty = false;
      this.userService.isadmin(this.azureIdParam).subscribe(res => {
        this.userType = 'admin';
        this.userDoesntExists = false;
      },
        err => {
          if (err.status === 404) {
            this.userService.isklant(this.azureIdParam).subscribe(res => {
              this.userType = 'klant';
              this.userDoesntExists = false;
              this.bestService.GetBestellingen(this.azureIdParam).subscribe(res => {
                this.Bestellingen = res;
              });
            },
              err => {
                if (err.status === 404) {
                  this.userDoesntExists = true;
                }
              });
          }
        });
    }
  }

}
