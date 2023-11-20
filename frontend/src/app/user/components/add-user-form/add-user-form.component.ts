import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '../../models/models';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-add-user-form',
  templateUrl: './add-user-form.component.html',
  styleUrls: ['./add-user-form.component.css']
})
export class AddUserFormComponent implements OnInit {

  usernameInvalid = false;
  passwordInvalid = false;
  passwordInvalidBackend = false;
  usernameAlreadyTaken = false;

  userForm = new FormGroup({
    username: new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(20)]),
    password: new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(20)])
  });
  constructor(private _userService: UserService, private _router: Router) { }

  ngOnInit(): void { }

  addUser() {
    if (this.userForm.valid) {
      const user: User = { username: <string>this.userForm.controls.username.value, password: <string>this.userForm.controls.password.value };
      this._userService.createUser(user).subscribe(res => {
        this._router.navigate(['user']);
      }, err => {

        if (err.error.errors.Password) {
          this.passwordInvalidBackend = true;
        }
        if (err.error.errors['0']) {
          this.usernameAlreadyTaken = true;
        }

      });
    }
    else {
      if (this.userForm.controls.username.invalid) {
        this.usernameInvalid = true;
      }
      if (this.userForm.controls.password.invalid) {
        this.passwordInvalid = true;
      }
    }
  }

  goBack() {
    this._router.navigate(['user']);
  }

}
