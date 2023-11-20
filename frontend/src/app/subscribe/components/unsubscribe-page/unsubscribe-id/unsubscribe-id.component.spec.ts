import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnsubscribeIdComponent } from './unsubscribe-id.component';

describe('UnsubscribeIdComponent', () => {
  let component: UnsubscribeIdComponent;
  let fixture: ComponentFixture<UnsubscribeIdComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnsubscribeIdComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UnsubscribeIdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
