import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { SubscriberService } from '../../services/subscriber.service';
import { AnswerData } from '../../models/models';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
})
export class FeedbackComponent {

  jobs = ['Investor', 'Entrepreneur', 'Employee', 'Unemployed', 'Student', 'Others'];

  feedbackForm = new FormGroup({
    job: new FormControl('', [Validators.required]),
    age: new FormControl(null, [Validators.required, Validators.max(110), Validators.min(8)])
  });
  isInvalid = false;
  isConfirmed = false;

  constructor(private _activatedRoute: ActivatedRoute, private _subscriberService: SubscriberService) { }

  onSendQuestionnaire() {
    if (this.feedbackForm.valid) {
      const subscriberId = this._activatedRoute.snapshot.params['id'];
      const occAnswer = this.feedbackForm.controls.job.value;
      const ageAnswer = this.feedbackForm.controls.age.value;
      const answer: AnswerData = { subscriberId: <string>subscriberId, occQuestion: <string>occAnswer, ageQuestion: ageAnswer }
      this._subscriberService.addAnswer(answer).subscribe(resp => {
        this.isConfirmed = true;
      }
        , err => {
          this.isInvalid = true;
        });
    }
    else {
      this.isInvalid = true;
    }
  }
}
