import { Injectable } from '@angular/core';
import { LoginUserResponse } from '../../features/identity/models/response/login-user.response';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly _tokenKey = 'accessToken';
  private readonly expiresAt = 'expiresAt';
  private readonly profileType = 'profileType';

  public saveSession(response: LoginUserResponse): void {
    localStorage.setItem(this._tokenKey, response.accessToken);
    localStorage.setItem(this.expiresAt, response.expiresAt);
    localStorage.setItem(this.profileType, response.profileType);
  }

  public getToken(): string | null {
    return localStorage.getItem(this._tokenKey);
  }

  public getProfileType(): string | null {
    return localStorage.getItem(this.profileType);
  }

  public getExpiresAt(): string | null {
    return localStorage.getItem(this.expiresAt);
  }

  public clearSession(): void {
    localStorage.removeItem(this._tokenKey);
    localStorage.removeItem(this.expiresAt);
    localStorage.removeItem(this.profileType);
  }

  public isAuthenticated(): boolean {
    const token = this.getToken();
    const expiresAt = this.getExpiresAt();

    if (!token || !expiresAt){
      return false;
    }

    return new Date(expiresAt) > new Date();
  }
}
