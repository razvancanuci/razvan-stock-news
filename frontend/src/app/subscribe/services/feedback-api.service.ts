import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AnswerData } from '../models/models';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FeedbackApiService {

  constructor(private _httpClient: HttpClient) { }

  addAnswer(answer: AnswerData) {
    return this._httpClient.post(`${environment.subscriberApi}/api/Answer`, answer);
  }
}
