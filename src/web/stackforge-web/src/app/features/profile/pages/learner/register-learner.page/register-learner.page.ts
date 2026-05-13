import { Component, inject } from '@angular/core';
import { Header } from '../../../../../layout/header/header';
import { FormButtonComponent } from '../../../../../shared/ui/components/form-button.component/form-button.component';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RegistrationFlowService } from '../../../../../shared/services/registration-flow.service';
import { Router } from '@angular/router';
import { RegisterProfileService } from '../../../services/register-profile.service';

@Component({
  selector: 'app-register-learner.page',
  imports: [Header, FormButtonComponent, ReactiveFormsModule],
  templateUrl: './register-learner.page.html',
  styleUrl: './register-learner.page.scss',
})
export class RegisterLearnerPage {
  private readonly _formBuilder = inject(FormBuilder);
  private readonly _registrationFlowService = inject(RegistrationFlowService);
  private readonly _router = inject(Router);
  private readonly _registerProfileService = inject(RegisterProfileService);

  protected registerLearnerForm = this._formBuilder.nonNullable.group({
    firstName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(80)]],
    lastName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(80)]],
    birthDate: ['', Validators.required]
  })

  protected get firstName(){
    return this.registerLearnerForm.get('firstName');
  }

  protected get lastName(){
    return this.registerLearnerForm.get('lastName');
  }

  protected get birthDate(){
    return this.registerLearnerForm.get('birthDate');
  }

  protected onSubmit(): void {
    const userId = this._registrationFlowService.getUserId();

    if(!userId){
      this._router.navigate(['register/user']);
    }

    if(this.registerLearnerForm.invalid){
      this.registerLearnerForm.markAllAsTouched();
      return;
    }

    const request = {
      userId,
      firstName: this.registerLearnerForm.getRawValue().firstName,
      lastName: this.registerLearnerForm.getRawValue().lastName,
      birthDate: this.registerLearnerForm.getRawValue().birthDate      
    }

    this._registerProfileService.registerLearner(request).subscribe({
      next: (response) => {
        this._registrationFlowService.clear();
        console.log(response)

        this._router.navigate(['login'])
      },
      error: (error) => {
        console.error('Error registering learner', error)
      }
    })
  }

}
