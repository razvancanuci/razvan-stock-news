import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs';
import { CodeModel } from '../../models/models';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-factor-page',
  templateUrl: './factor-page.component.html',
  styleUrls: ['./factor-page.component.css']
})
export class FactorPageComponent implements OnInit {

  factorForm = new FormGroup({
    code: new FormControl('', [Validators.required]),
  });
  factorFormInvalid = false;
  factorFormInvalidBackend = false;

  constructor(private _userService: UserService, private _router: Router) { }

  ngOnInit(): void {

  }

  login() {
    if (this.factorForm.valid) {
      const code: CodeModel = { code: <string>this.factorForm.controls.code.value };
      this._userService.loginAdmin(code).pipe(first()).subscribe(response => {
        localStorage.setItem('authToken', response.token);
        this._router.navigate(['/user']);
      }, err => {
        this.factorFormInvalidBackend = true;
      })
    }
    else {
      this.factorFormInvalid = true;
    }
  }



}
