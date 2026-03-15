import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegistrationFlow } from '../../../auth/data/services/registration-flow';
import { ProfileApi } from '../../../auth/data/services/profile-api';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-learner-register-page',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './learner-register-page.html',
  styleUrl: './learner-register-page.css',
})
export class LearnerRegisterPage {
  private readonly formBuilder = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly registrationFlowService = inject(RegistrationFlow);
  private readonly profileApi = inject(ProfileApi);
  protected readonly form = this.formBuilder.nonNullable.group({
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
    birthDate: ['', [Validators.required]]
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
      birthDate: formValue.birthDate
    }

    this.profileApi.registerLearner(request).subscribe({
      next: (response) => {
        console.log('Learner registered successfully', response);
        this.registrationFlowService.clear();
        this.router.navigate(['/login'])
      },
      error: (error) => {
        console.error('Error registering learner', error)
      }
    });

  }
}
