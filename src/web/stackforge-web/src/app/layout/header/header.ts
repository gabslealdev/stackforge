import { Component, inject } from '@angular/core';
import { RouterLink } from "@angular/router";
import { LoginUserService } from '../../features/identity/services/login-user.service';

@Component({
  selector: 'app-header',
  imports: [RouterLink],
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class Header {
  private readonly _loginUserService = inject(LoginUserService)

  readonly isAuthenticated = this._loginUserService.isAuthenticated;

  logout(): void {
  this._loginUserService.logout();
}
}
