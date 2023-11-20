import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscriber } from '../../../models/models';

@Component({
  selector: 'app-subscriber-form',
  templateUrl: './subscriber-form.component.html',
  styleUrls: ['./subscriber-form.component.css']
})
export class SubscriberFormComponent implements OnInit {

  @Input()
  emailAlreadySubscribed!: boolean;

  emailInvalid = false;
  nameInvalid = false;
  phoneInvalid = false;

  subscriberForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.pattern("^[a-zA-Z]+$")]),
    email: new FormControl('', [Validators.required, Validators.email]),
    phoneNumber: new FormControl('', [Validators.pattern("^07[0-8]{1}[0-9]{7}$")])
  });

  @Output()
  formEmitter = new EventEmitter<Subscriber>()
  constructor() { }

  ngOnInit(): void {
  }

  onSubscribe() {
    if (this.subscriberForm.valid) {
      const subscriber = {
        name: <string>this.subscriberForm.controls.name.value,
        email: <string>this.subscriberForm.controls.email.value,
        phoneNumber: this.subscriberForm.controls.phoneNumber.value
      }
      this.formEmitter.emit(subscriber);
    }
    else {
      if (this.subscriberForm.controls.email.invalid) {
        this.emailInvalid = true;
      }
      if (this.subscriberForm.controls.name.invalid) {
        this.nameInvalid = true;
      }
      if (this.subscriberForm.controls.phoneNumber.invalid) {
        this.phoneInvalid = true;
      }
    }

  }

}
