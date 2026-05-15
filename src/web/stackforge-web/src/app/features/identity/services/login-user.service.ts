import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { LoginUserRequest } from '../models/request/login-user.request';
import { Observable, tap} from 'rxjs';
import { LoginUserResponse } from '../models/response/login-user.response';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class LoginUserService {
  private readonly _httpClient = inject(HttpClient);
  private readonly _apiUrl = environment.apiUrl;
  private readonly _router = inject(Router);
  private readonly _isAuthenticated = signal<boolean>(!!localStorage.getItem('accessToken'));

  readonly isAuthenticated = this._isAuthenticated.asReadonly();

  login(request: LoginUserRequest): Observable<LoginUserResponse>{
    return this._httpClient.post<LoginUserResponse>(`${this._apiUrl}/api/identity/login`, request).pipe(
      tap(response => {
        localStorage.setItem('accessToken', response.accessToken);
        this._isAuthenticated.set(true);
      })
    )
  }

  logout(): void {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('expiresAt');
    localStorage.removeItem('profileType');
    this._isAuthenticated.set(false);

    this._router.navigate(['/login'])
  }
}
