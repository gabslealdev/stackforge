import { Component, inject } from '@angular/core';
import { Header } from '../../../../../layout/header/header';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RegistrationFlowService } from '../../../../../shared/services/registration-flow.service';
import { Router } from '@angular/router';
import { RegisterProfileService } from '../../../services/register-profile.service';
import { FormButtonComponent } from '../../../../../shared/ui/components/form-button.component/form-button.component';

@Component({
  selector: 'app-register-mentor.page',
  imports: [Header, ReactiveFormsModule, FormButtonComponent],
  templateUrl: './register-mentor.page.html',
  styleUrl: './register-mentor.page.scss',
})
export class RegisterMentorPage {
  private readonly _formBuilder = inject(FormBuilder);
  private readonly _registrationFlowService = inject(RegistrationFlowService);
  private readonly _router = inject(Router);
  private readonly _registerProfileService = inject(RegisterProfileService);

  protected registerMentorForm = this._formBuilder.nonNullable.group({
    firstName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(80)]],
    lastName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(80)]],
    birthDate: ['', [Validators.required]],
    courseName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
    institution: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
    educationStatus: ['0',[Validators.required]],
    conclusionDate: ['', [Validators.required]],
    bio: ['', [Validators.maxLength(200)]]
  })

  protected get firstName(){
    return this.registerMentorForm.get('firstName');
  }

  protected get lastName(){
    return this.registerMentorForm.get('lastName');
  }

  protected get birthDate(){
    return this.registerMentorForm.get('birthDate');
  }

  protected get courseName(){
    return this.registerMentorForm.get('courseName');
  }

  protected get institution(){
    return this.registerMentorForm.get('institution');
  }

  protected get educationStatus(){
    return this.registerMentorForm.get('educationStatus');
  }

  protected get conclusionDate(){
    return this.registerMentorForm.get('conclusionDate');
  }

  protected get bio(){
    return this.registerMentorForm.get('bio');
  }

  protected onSubmit(): void {
    const userId = this._registrationFlowService.getUserId();

    if(!userId){
      this._router.navigate(['register/user']);
    }

    if(this.registerMentorForm.invalid){
      this.registerMentorForm.markAllAsTouched();
      return;
    }

    const request = {
      userId,
      firstName: this.registerMentorForm.getRawValue().firstName,
      lastName: this.registerMentorForm.getRawValue().lastName,
      birthDate: this.registerMentorForm.getRawValue().birthDate,
      courseName: this.registerMentorForm.getRawValue().courseName,
      institution: this.registerMentorForm.getRawValue().institution,
      educationStatus: Number(this.registerMentorForm.getRawValue().educationStatus),
      conclusionDate: this.registerMentorForm.getRawValue().conclusionDate,
      bio: this.registerMentorForm.getRawValue().bio.trim() === '' ? null : this.registerMentorForm.getRawValue().bio 
    };

    this._registerProfileService.registerMentor(request).subscribe({
      next: (response) => {
        this._registrationFlowService.clear();
        console.log(response)

        // enviar para rota de login
      },
      error: (error) => {
        console.error('Error registering mentor', error)
      }
    });

  }
}

