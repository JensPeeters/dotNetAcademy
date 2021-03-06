import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.scss']
})
export class CreateUserComponent implements OnInit {

  azureIdParam: string;
  userType: string;
  azureIdEmpty: boolean;
  createUserSucces: boolean;
  userAlreadyExists: boolean;

  constructor(private userService: UserService,
    private router: Router) { }

  ngOnInit() {
    this.ResetParameters();
  }

  ResetParameters() {
    this.azureIdParam = '';
    this.userType = 'klant';
    this.azureIdEmpty = false;
    this.createUserSucces = false;
    this.userAlreadyExists = false;
  }

  CreateNew() {
    this.ResetParameters();
  }

  CreateUser() {
    if (this.azureIdParam === '') {
      this.azureIdEmpty = true;
    } else {
      this.azureIdEmpty = false;
      if (this.userType === 'klant') {
        this.userService.saveKlantInDb(this.azureIdParam).subscribe(res => {
          this.createUserSucces = true;
          this.userAlreadyExists = false;
        },
          err => {
            this.createUserSucces = false;
            if (err.status === 409) {
              this.userAlreadyExists = true;
            }
          });
      } else if (this.userType === 'admin') {
        this.userService.saveAdminInDb(this.azureIdParam).subscribe(res => {
          this.createUserSucces = true;
          this.userAlreadyExists = false;
        },
          err => {
            this.createUserSucces = false;
            if (err.status === 409) {
              this.userAlreadyExists = true;
            }
          });
      }
    }
  }

}
