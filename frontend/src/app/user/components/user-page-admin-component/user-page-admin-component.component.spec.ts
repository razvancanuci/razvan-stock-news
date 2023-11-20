import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPageAdminComponentComponent } from './user-page-admin-component.component';

describe('UserPageAdminComponentComponent', () => {
  let component: UserPageAdminComponentComponent;
  let fixture: ComponentFixture<UserPageAdminComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserPageAdminComponentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserPageAdminComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
