import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RegisterUserService } from '../../services/register-user.service';
import { RegistrationFlowService } from '../../../../shared/services/registration-flow.service';
import { Router } from '@angular/router';
import { Header } from '../../../../layout/header/header';
import { FormButtonComponent } from '../../../../shared/ui/components/form-button.component/form-button.component';
import { ProfileType } from '../../models/enums/profile-type.enum';


@Component({
  selector: 'app-register-user.page',
  imports: [Header, ReactiveFormsModule, FormButtonComponent],
  templateUrl: './register-user.page.html',
  styleUrl: './register-user.page.scss',
})
export class RegisterUserPage {
  private readonly _formBuilder = inject(FormBuilder);
  private readonly _registerUserService = inject(RegisterUserService)
  private readonly _registrationFlowService = inject(RegistrationFlowService)
  private readonly _router = inject(Router)
  

  protected registerUserForm = this._formBuilder.nonNullable.group({
    email: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(80)]],
    password: ['', [Validators.required, Validators.minLength(8)]],
    confirmPassword: ['', [Validators.required, Validators.minLength(8)]]
  }); 

  protected get email() {
    return this.registerUserForm.get('email');
  }

  protected get password() {
    return this.registerUserForm.get('password');
  }

  protected get confirmPassword() {
    return this.registerUserForm.get('confirmPassword');
  }

  protected passwordsMatch(): boolean{
    const password = this.password?.value;
    const confirmPassword = this.confirmPassword?.value;

    if (!password || !confirmPassword){
      return true;
    }

    return password === confirmPassword
  }

  protected onSubmit(): void {
    const selectedProfileType = this._registrationFlowService.getSelectedProfileType();

    if (!selectedProfileType){
      this._router.navigate(['select-profile']);
      return;
    }

    if (this.registerUserForm.invalid) {
      this.registerUserForm.markAsUntouched();
      return;
    }

    const request = {
      email: this.registerUserForm.getRawValue().email,
      password: this.registerUserForm.getRawValue().password,
      selectedProfileType
    };

    this._registerUserService.registerUser(request).subscribe({
      next: (response) => {
        this._registrationFlowService.setUserId(response.userId)
        console.log(response)

        if(selectedProfileType == ProfileType.Learner){
          this._router.navigate(['register/user/learner'])
        }

        if(selectedProfileType == ProfileType.Mentor){
          this._router.navigate(['register/user/mentor'])
        }

        // enviar para rota de mentor se for mentor
      } ,
      error: (error) => {
        console.error('Error registering user', error)
      }
    });
  }


}
