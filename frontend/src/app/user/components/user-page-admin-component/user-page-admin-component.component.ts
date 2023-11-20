import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first } from 'rxjs';
import { Subscriber } from 'src/app/subscribe/models/models';
import { SubscriberService } from 'src/app/subscribe/services/subscriber.service';

@Component({
  selector: 'app-user-page-admin-component',
  templateUrl: './user-page-admin-component.component.html',
  styleUrls: ['./user-page-admin-component.component.css']
})
export class UserPageAdminComponentComponent implements OnInit {

  subscribers!: Subscriber[]

  constructor(private _router: Router, private _subscriberService: SubscriberService) { }

  ngOnInit(): void {
    this._subscriberService.getSubscribers().pipe(first()).subscribe((resposne) => {
      this.subscribers = resposne;

    });
  }
  goToAddUser() {
    this._router.navigate(['user/add-user']);
  }
  goToDeleteUser() {
    this._router.navigate(['user/delete-user']);
  }

}
