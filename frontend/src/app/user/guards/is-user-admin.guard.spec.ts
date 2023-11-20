import { TestBed } from '@angular/core/testing';

import { IsUserAdminGuard } from './is-user-admin.guard';

describe('IsUserAdminGuard', () => {
  let guard: IsUserAdminGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(IsUserAdminGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
