import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Statistics, Subscriber } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class SubscriberApiService {

  constructor(private _httpClient: HttpClient) { }

  get() {
    return this._httpClient.get(`${environment.subscriberApi}/api/Subscriber`).pipe(
      map<any, Subscriber[]>(response => response.result)
    );
  }

  getStats() {
    return this._httpClient.get(`${environment.subscriberApi}/api/Subscriber/statistics`).pipe(
      map<any, Statistics>(resposne => resposne.result)
    );
  }

  post(subscriber: Subscriber) {
    return this._httpClient.post(`${environment.subscriberApi}/api/Subscriber`, subscriber);
  }

  getById(id: string) {
    return this._httpClient.get(`${environment.subscriberApi}/api/Subscriber/${id}`);
  }

  deleteById(id: string) {
    return this._httpClient.delete(`${environment.subscriberApi}/api/Subscriber/${id}`);
  }

  deletebyEmail(email: string) {
    return this._httpClient.delete(`${environment.subscriberApi}/api/Subscriber?subscriberEmail=${email}`);
    ;
  }
}
