import { DatePipe } from '@angular/common';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { first } from 'rxjs';
import { BarData, Statistics } from 'src/app/subscribe/models/models';
import { SubscriberService } from 'src/app/subscribe/services/subscriber.service';
import * as signalR from "@microsoft/signalr";
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user-page-user-component',
  templateUrl: './user-page-user-component.component.html',
  styleUrls: ['./user-page-user-component.component.css']
})
export class UserPageUserComponentComponent implements OnInit, OnDestroy {

  stats!: Statistics
  dates!: BarData[];
  connection!: signalR.HubConnection

  constructor(private _subscriberService: SubscriberService) { }

  ngOnInit(): void {
    this.connection = new signalR.HubConnectionBuilder().withUrl(`${environment.subscriberApi}/hub/stats`, {
      withCredentials: true
    }).build();
    this.connection.on("updateSubscriberStats", response => {
      this.updateStats(response);
    });
    this.connection.start();
    this.getStats();
  }

  ngOnDestroy(): void {
    if (this.connection && this.connection.state === 'Connected') {
      this.connection.stop();
    }
  }

  getStats() {
    this._subscriberService.getStats().pipe(first()).subscribe((response) => {
      this.updateStats(response);
    });
  }

  updateStats(response: Statistics | any) {
    this.stats = response;
    const datepipe = new DatePipe("en-US");
    const now = new Date();
    this.dates = this.stats.subscribedLast7D.map((value, index) => { return { date: <string>datepipe.transform(now.getTime() - (6 - index) * 86400000, "MM/dd"), value: value } });
  }

}
