import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs';
import { Subscriber } from '../../models/models';
import { SubscriberService } from '../../services/subscriber.service';

@Component({
  selector: 'app-subscriber-page',
  templateUrl: './subscriber-page.component.html',
  styleUrls: ['./subscriber-page.component.css']
})
export class SubscriberPageComponent implements OnInit {

  emailSubscribed = false;
  isSubscribed = false;
  constructor(private _subscriberService: SubscriberService) { }

  ngOnInit(): void {

  }

  createSubscriber(subscriber: Subscriber) {

    this._subscriberService.createSubscriber(subscriber).pipe(first()).subscribe(response => {
      this.isSubscribed = true;
    }, err => {
      console.log(err);
      if (err.error.errors['0']) {
        this.emailSubscribed = true;
      }
    });

  }

}
