import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginGuard implements CanActivate {
  constructor(private _router: Router) { }
  canActivate() {
    if (!localStorage.getItem('authToken')) {
      return true;
    }
    this._router.navigate(['user']);
    return false;


  }

}
