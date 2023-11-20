import { Injectable } from '@angular/core';
import { AnswerData, Subscriber } from '../models/models';
import { SubscriberApiService } from './subscriber-api.service';
import { FeedbackApiService } from './feedback-api.service';

@Injectable({
  providedIn: 'root'
})
export class SubscriberService {

  constructor(private _subscriberApiService: SubscriberApiService, private _answerApiService: FeedbackApiService) { }

  getSubscribers() {
    return this._subscriberApiService.get();
  }

  getStats() {
    return this._subscriberApiService.getStats();
  }

  createSubscriber(subscriber: Subscriber) {
    return this._subscriberApiService.post(subscriber);
  }

  getEmailById(id: string) {
    return this._subscriberApiService.getById(id);
  }

  deleteSubscriberById(id: string) {
    return this._subscriberApiService.deleteById(id);
  }

  deleteSubscriberByEmail(email: string) {
    return this._subscriberApiService.deletebyEmail(email);
  }

  addAnswer(answer: AnswerData) {
    return this._answerApiService.addAnswer(answer);
  }

}
