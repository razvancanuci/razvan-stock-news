import { TestBed } from '@angular/core/testing';

import { SubscriberValidGuard } from './subscriber-valid.guard';

describe('SubscriberValidGuard', () => {
  let guard: SubscriberValidGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(SubscriberValidGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
