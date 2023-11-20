import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs';
import { User } from '../../models/models';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css',],
  encapsulation: ViewEncapsulation.None
})
export class LoginFormComponent implements OnInit {

  loginFail = false;
  userForm = new FormGroup({
    username: new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(20)]),
    password: new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(20)])
  });
  constructor(private _userService: UserService, private _router: Router) { }

  ngOnInit(): void { }
  login() {
    if (this.userForm.valid) {
      const user: User = { username: <string>this.userForm.controls.username.value, password: <string>this.userForm.controls.password.value };
      this._userService.loginUser(user).pipe(first()).subscribe(response => {
        localStorage.setItem('authToken', response.token);
        if (response.role == "User") {

          this._router.navigate(['/user']);
        }
        else {
          this._router.navigate(['/user/2factor']);
        }

      }, err => {
        this.loginFail = true;
      });
    }
    else {
      this.loginFail = true;
    }

  }

}
