import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FactorPageComponent } from './factor-page.component';

describe('FactorPageComponent', () => {
  let component: FactorPageComponent;
  let fixture: ComponentFixture<FactorPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FactorPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FactorPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
