import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { RegisterUserRequest } from '../models/request/register-user.request';
import { RegisterUserResponse } from '../models/response/register-user.response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RegisterUserService {
  private readonly _http = inject(HttpClient)
  private _apiUrl = environment.apiUrl

  registerUser(request: RegisterUserRequest): Observable<RegisterUserResponse> {
    return this._http.post<RegisterUserResponse>(`${this._apiUrl}/api/identity/user`, request)
  }
}
