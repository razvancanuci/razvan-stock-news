import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-unsubscribe-form',
  templateUrl: './unsubscribe-form.component.html',
  styleUrls: ['./unsubscribe-form.component.css']
})
export class UnsubscribeFormComponent implements OnInit {

  unsubscribeForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email])
  });

  @Input()
  emailInvalid!: boolean;

  @Output()
  unsubscribeEmitter = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  onUnsubscribe() {
    if (this.unsubscribeForm.valid) {
      this.unsubscribeEmitter.emit(<string>this.unsubscribeForm.controls.email.value);
    }
    else {
      this.emailInvalid = true;
    }

  }

}
