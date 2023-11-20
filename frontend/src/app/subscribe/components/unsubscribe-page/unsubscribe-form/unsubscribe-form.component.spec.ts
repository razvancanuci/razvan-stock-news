import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnsubscribeFormComponent } from './unsubscribe-form.component';

describe('UnsubscribeFormComponent', () => {
  let component: UnsubscribeFormComponent;
  let fixture: ComponentFixture<UnsubscribeFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnsubscribeFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UnsubscribeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
