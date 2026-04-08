import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { LoginUserRequest } from '../models/request/login-user.request';
import { Observable } from 'rxjs';
import { LoginUserResponse } from '../models/response/login-user.response';

@Injectable({
  providedIn: 'root',
})
export class LoginUserService {
  private readonly _httpClient = inject(HttpClient)
  private readonly _apiUrl = environment.apiUrl

  login(request: LoginUserRequest): Observable<LoginUserResponse>{
    return this._httpClient.post<LoginUserResponse>(`${this._apiUrl}/api/identity/login`, request)
  }
}
