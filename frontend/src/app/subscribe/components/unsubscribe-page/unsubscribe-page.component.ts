import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { first, map } from 'rxjs';
import { SubscriberService } from '../../services/subscriber.service';
import { EmailModel } from '../../models/models';

@Component({
  selector: 'app-unsubscribe-page',
  templateUrl: './unsubscribe-page.component.html',
  styleUrls: ['./unsubscribe-page.component.css']
})
export class UnsubscribePageComponent implements OnInit {

  isUnSubscribed = false;
  email!: string
  isValidId = false;
  emailInvalid = false;
  constructor(public _activeRoute: ActivatedRoute, private _subscriberService: SubscriberService, private _router: Router) { }

  ngOnInit(): void {
    this._subscriberService.getEmailById(this._activeRoute.snapshot.params['id']).pipe(map<any, EmailModel>(resp => resp.result)).subscribe(response => {
      this.email = response.email;
      this.isValidId = true;
    }, err => {
      this.isValidId = false;
    });
  }

  onUnsubscribeId() {
    this.isUnSubscribed = true;
    this._subscriberService.deleteSubscriberById(this._activeRoute.snapshot.params['id']).pipe(first()).subscribe(resp => {
      this.isUnSubscribed = true;
    }, err => {
      this.emailInvalid = true;
    });
  }

  onUnsubscribeEmail(email: string) {
    this._subscriberService.deleteSubscriberByEmail(email).pipe(first()).subscribe(resp => {
      this.isUnSubscribed = true;
    }, err => {
      this.emailInvalid = true;
    });
  }
}
