import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.scss']
})
export class UpdateUserComponent implements OnInit {

  azureIdParam: string;
  userType: string;
  azureIdEmpty: boolean;
  updateUserSucces: boolean;
  confirmUpdate: boolean;
  confirmNotChecked: boolean;
  userDoesntExists: boolean;

  constructor(private userService: UserService,
              private router: Router) { }

  ngOnInit() {
    this.ResetParameters();
  }

  ResetParameters() {
    this.azureIdParam = '';
    this.userType = '';
    this.azureIdEmpty = false;
    this.updateUserSucces = false;
    this.confirmUpdate = false;
    this.confirmNotChecked = false;
    this.userDoesntExists = false;
  }

  UpdateNew() {
    this.ResetParameters();
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

  UpdateUser() {
    if (!this.confirmUpdate) {
      this.confirmNotChecked = true;
    } else {
      this.confirmNotChecked = false;
      if (this.userType === 'klant') {
        this.MakeKlantAdmin();
      } else if (this.userType === 'admin') {
        this.MakeAdminKlant();
      }
    }
  }

  MakeAdminKlant() {
    this.userService.updateAdminToKlant(this.azureIdParam).subscribe(res => {
      this.updateUserSucces = true;
      this.userDoesntExists = false;
    },
      err => {
        this.updateUserSucces = false;
        if (err.status === 404) {
          this.userDoesntExists = true;
        }
      });
  }

  MakeKlantAdmin() {
    this.userService.updateKlantToAdmin(this.azureIdParam).subscribe(res => {
      this.updateUserSucces = true;
      this.userDoesntExists = false;
    },
      err => {
        this.updateUserSucces = false;
        if (err.status === 404) {
          this.userDoesntExists = true;
        }
      });
  }
}
