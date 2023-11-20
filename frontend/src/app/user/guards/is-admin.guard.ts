import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { first, Observable } from 'rxjs';
import { UserService } from '../services/user.service';

@Injectable({
  providedIn: 'root'
})
export class IsAdminGuard implements CanActivate {
  constructor(private _userService: UserService, private _router: Router) { }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    this._userService.getUser().pipe(first()).subscribe(res => {
      if (res.role !== route.data['role']) {
        localStorage.removeItem('authToken');
        this._router.navigate(['user/login']);

      }
    }
      , err => {
        localStorage.removeItem('authToken');
        this._router.navigate(['user/login']);
      })
    return true;
  }

}
