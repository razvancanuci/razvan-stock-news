import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPageUserComponentComponent } from './user-page-user-component.component';

describe('UserPageUserComponentComponent', () => {
  let component: UserPageUserComponentComponent;
  let fixture: ComponentFixture<UserPageUserComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserPageUserComponentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserPageUserComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
