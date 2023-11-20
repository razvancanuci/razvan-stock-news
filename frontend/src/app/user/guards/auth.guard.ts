import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { first, Observable } from 'rxjs';
import { UserService } from '../services/user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private _router: Router, private _userService: UserService) { }
  canActivate() {
    if (localStorage.getItem('authToken')) {
      this._userService.getUser().pipe(first()).subscribe(res => { }
        , err => {
          localStorage.removeItem('authToken');
          this._router.navigate(['user/login']);
        })
      return true;
    }
    this._router.navigate(['user/login']);
    return false;


  }

}
