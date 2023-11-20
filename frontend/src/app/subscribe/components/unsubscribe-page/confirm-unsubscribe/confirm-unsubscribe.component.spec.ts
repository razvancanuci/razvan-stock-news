import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmUnsubscribeComponent } from './confirm-unsubscribe.component';

describe('ConfirmUnsubscribeComponent', () => {
  let component: ConfirmUnsubscribeComponent;
  let fixture: ComponentFixture<ConfirmUnsubscribeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfirmUnsubscribeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfirmUnsubscribeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
