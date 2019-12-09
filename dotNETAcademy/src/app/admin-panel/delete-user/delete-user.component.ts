import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-delete-user',
  templateUrl: './delete-user.component.html',
  styleUrls: ['./delete-user.component.scss']
})
export class DeleteUserComponent implements OnInit {

  azureIdParam: string;
  confirmDelete: boolean;
  azureIdEmpty: boolean;
  confirmNotChecked: boolean;
  deleteUserSucces: boolean;
  userDoesntExists: boolean;

  constructor(private userService: UserService,
    private router: Router) { }

  ngOnInit() {
    this.ResetParameters();
  }

  ResetParameters() {
    this.azureIdParam = '';
    this.confirmDelete = false;
    this.azureIdEmpty = false;
    this.confirmNotChecked = false;
    this.deleteUserSucces = false;
    this.userDoesntExists = false;
  }

  DeleteAnother() {
    this.ResetParameters();
  }

  DeleteUser() {
    if (this.azureIdParam === '') {
      this.azureIdEmpty = true;
    } else {
      this.azureIdEmpty = false;
      if (!this.confirmDelete) {
        this.confirmNotChecked = true;
      } else {
        this.confirmNotChecked = false;

        this.userService.isadmin(this.azureIdParam).subscribe(res => {
          this.DeleteAdmin();
        },
          err => {
            if (err.status === 404) {
              this.DeleteKlant();
            }
          });
      }
    }
  }

  DeleteAdmin() {
    this.userService.deleteAdminInDb(this.azureIdParam).subscribe(res => {
      this.deleteUserSucces = true;
      this.userDoesntExists = false;
    },
      err => {
        this.deleteUserSucces = false;
        if (err.status === 404) {
          this.userDoesntExists = true;
        }
      });
  }

  DeleteKlant() {
    this.userService.deleteKlantInDb(this.azureIdParam).subscribe(res => {
      this.deleteUserSucces = true;
      this.userDoesntExists = false;
    },
      err => {
        this.deleteUserSucces = false;
        if (err.status === 404) {
          this.userDoesntExists = true;
        }
      });
  }
}
