import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LearnerRegisterPage } from './learner-register-page';

describe('LearnerRegisterPage', () => {
  let component: LearnerRegisterPage;
  let fixture: ComponentFixture<LearnerRegisterPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LearnerRegisterPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LearnerRegisterPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
