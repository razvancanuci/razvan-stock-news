import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, map } from 'rxjs';
import { SubscriberService } from '../services/subscriber.service';
import { EmailModel } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class SubscriberValidGuard implements CanActivate {

  constructor(private _subscriberService: SubscriberService, private _router: Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    this._subscriberService.getEmailById(route.params['id']).pipe(map<any, EmailModel>(r => r.result)).subscribe(resp => {
      if (resp.hasAnswered == true) {
        this._router.navigate(['/already-subscribed']);
      }
      return true;
    },
      err => {
        this._router.navigate(['/notfound']);
        return false;
      });
    return true;
  }

}
