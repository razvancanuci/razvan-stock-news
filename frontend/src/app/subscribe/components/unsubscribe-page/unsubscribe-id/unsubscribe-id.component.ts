import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-unsubscribe-id',
  templateUrl: './unsubscribe-id.component.html',
  styleUrls: ['./unsubscribe-id.component.css']
})
export class UnsubscribeIdComponent implements OnInit {

  @Input()
  email!: string;

  @Input()
  emailInvalid!: boolean;

  @Output()
  unsubscribeEmitter = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  onUnsubscribe() {
    this.unsubscribeEmitter.emit();
  }

}
