import { Component, inject } from '@angular/core';
import { RegistrationFlow } from '../../../auth/data/services/registration-flow'
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ProfileApi } from '../../../auth/data/services/profile-api';

@Component({
  selector: 'app-mentor-register-page',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './mentor-register-page.html',
  styleUrl: './mentor-register-page.css',
})
export class MentorRegisterPage {
  private readonly formBuilder = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly registrationFlowService = inject(RegistrationFlow);
  private readonly profileApi = inject(ProfileApi)

  protected readonly form = this.formBuilder.nonNullable.group({
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
    birthDate: ['', [Validators.required]],
    courseName: ['', [Validators.required]],
    institution: ['', [Validators.required]],
    educationStatus: ['', [Validators.required]],
    conclusionDate: ['', [Validators.required]],
    bio: ['', []]
  });

  submit(): void {
    const userId = this.registrationFlowService.getUserId();

    if (!userId) {
      this.router.navigate(['/register/user']);
      return;
    }

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const formValue = this.form.getRawValue();

    const request = {
      userId,
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      birthDate: formValue.birthDate,
      courseName: formValue.courseName,
      institution: formValue.institution,
      educationStatus: Number(formValue.educationStatus),
      conclusionDate: formValue.conclusionDate,
      bio: formValue.bio.trim() ? formValue.bio : null
    };

    this.profileApi.registerMentor(request).subscribe({
      next: (response) => {
        console.log('Mentor registered successfully', response);
        this.registrationFlowService.clear();
        this.router.navigate(['/login']);
      },
      error: (error) => {
        console.error('Error registering mentor', error);
      }
    });

  }
}
