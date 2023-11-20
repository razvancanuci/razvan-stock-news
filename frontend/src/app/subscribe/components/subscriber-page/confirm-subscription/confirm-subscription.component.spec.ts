import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmSubscriptionComponent } from './confirm-subscription.component';

describe('ConfirmSubscriptionComponent', () => {
  let component: ConfirmSubscriptionComponent;
  let fixture: ComponentFixture<ConfirmSubscriptionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfirmSubscriptionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfirmSubscriptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
