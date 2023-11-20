import { TestBed } from '@angular/core/testing';

import { IsFactorGuard } from './is-factor.guard';

describe('IsFactorGuard', () => {
  let guard: IsFactorGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(IsFactorGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
