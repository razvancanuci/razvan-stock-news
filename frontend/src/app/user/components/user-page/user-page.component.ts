import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first } from 'rxjs';
import { UserRole } from '../../models/models';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css']
})
export class UserPageComponent implements OnInit {

  userData!: UserRole

  constructor(private _router: Router, private _userService: UserService) { }

  ngOnInit(): void {
    this._userService.getUser().pipe(first()).subscribe((response) => {
      this.userData = response;
    })
  }


  logout() {
    localStorage.removeItem('authToken');
    this._router.navigate(['user/login']);

  }
}
