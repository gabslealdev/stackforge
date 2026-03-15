import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RegistrationFlow } from '../../../data/services/registration-flow';
import { AuthApi } from '../../../data/services/auth-api';
import { ProfileType } from '../../../domain/enums/profile-type.enum';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register-user',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register-user.html',
  styleUrl: './register-user.css',
})
export class RegisterUser {
  private readonly formBuilder = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly registrationFlowService = inject(RegistrationFlow);
  private readonly authApi = inject(AuthApi);

  protected readonly form = this.formBuilder.nonNullable.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(8)]]
  });

  submit(): void {
    const selectedProfileType = this.registrationFlowService.getSelectedProfileType();

    if (!selectedProfileType) {
      this.router.navigate(['/register/select-profile']);
      return;
    }

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const request = {
      email: this.form.getRawValue().email,
      password: this.form.getRawValue().password,
      selectedProfileType
    };

    this.authApi.registerUser(request).subscribe({
      next: (response) => {
        this.registrationFlowService.setUserId(response.userId);

        if (selectedProfileType === ProfileType.Learner) {
          this.router.navigate(['/register/learner']);
          return;
        }

        if (selectedProfileType === ProfileType.Mentor) {
          this.router.navigate(['/register/mentor']);
        }
      },
      error: (error) => {
        console.error('Error registering user', error);
      }
    });
  }
}
