import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MentorRegisterPage } from './mentor-register-page';

describe('MentorRegisterPage', () => {
  let component: MentorRegisterPage;
  let fixture: ComponentFixture<MentorRegisterPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MentorRegisterPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MentorRegisterPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
