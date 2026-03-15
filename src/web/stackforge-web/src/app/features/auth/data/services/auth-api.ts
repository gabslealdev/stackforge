import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { RegisterUserRequest } from '../models/register-user.request';
import { Observable } from 'rxjs';
import { RegisterUserResponse } from '../models/register-user.response';

@Injectable({
  providedIn: 'root',
})
export class AuthApi {
  private readonly http = inject(HttpClient)
  private readonly apiUrl = 'http://localhost:5106'

  registerUser(request: RegisterUserRequest): Observable<RegisterUserResponse>{
    return this.http.post<RegisterUserResponse>(`${this.apiUrl}/api/identity/user`, request)
  }

}
