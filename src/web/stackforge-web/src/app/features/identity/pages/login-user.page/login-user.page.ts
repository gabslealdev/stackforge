import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { FormButtonComponent } from '../../../../shared/ui/components/form-button.component/form-button.component';
import { LucideSquareUserRound } from "@lucide/angular";
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginUserService } from '../../services/login-user.service';
import { AuthService } from '../../../../shared/services/auth.service';
import { LoginUserRequest } from '../../models/request/login-user.request';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-user.page',
  imports: [FormButtonComponent, LucideSquareUserRound, ReactiveFormsModule],
  templateUrl: './login-user.page.html',
  styleUrl: './login-user.page.scss',
})
export class LoginUserPage {
  private readonly _formBuilder = inject(FormBuilder);
  private readonly _loginService = inject(LoginUserService)
  private readonly _authService = inject(AuthService)
  private readonly cdr = inject(ChangeDetectorRef);
  private readonly _router = inject(Router);

  protected apiErrorMessage: string | null = null;

  public readonly loginUserForm = this._formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]],
  });

  protected get email() {
    return this.loginUserForm.get('email'); 
  }

  protected get password() {
    return this.loginUserForm.get('password');
  }

  protected onSubmit(): void {
    if(this.loginUserForm.invalid){
      this.loginUserForm.markAllAsTouched();
      return;
    }

    const loginRequest: LoginUserRequest = {
      email: this.email?.value ?? '',
      password: this.password?.value ?? ''
    };

    this._loginService.login(loginRequest).subscribe({
      next: (response) => {
        this._authService.saveSession(response)
        this.loginUserForm.reset();

        this._router.navigate(['mentor/dashboard'])
      },
error: (error) => {
            console.error("Erro ao realizar login", error.status);

            if (error.status === 400) {
              this.apiErrorMessage = "Email ou Senha inválidos.";
            } else {
             this.apiErrorMessage = "Ocorreu um erro ao tentar realizar login. Por favor, tente novamente mais tarde.";
            }

            this.cdr.detectChanges();
          }     
    })
  }
}
