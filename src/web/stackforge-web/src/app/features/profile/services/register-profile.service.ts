import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { RegisterLearnerRequest } from '../models/Learner/register-learner.request';
import { Observable } from 'rxjs';
import { RegisterLearnerResponse } from '../models/Learner/register-learnet.response';
import { RegisterMentorRequest } from '../models/Mentor/register-mentor.request';
import { RegisterMentorResponse } from '../models/Mentor/register-mentor.response';

@Injectable({
  providedIn: 'root',
})
export class RegisterProfileService {
  private readonly _http = inject(HttpClient)
  private readonly _apiUrl = environment.apiUrl

  registerLearner(request: RegisterLearnerRequest): Observable<RegisterLearnerResponse> {
    return this._http.post<RegisterLearnerResponse>(`${this._apiUrl}/api/profile/learner`, request)
  }

  registerMentor(request: RegisterMentorRequest): Observable<RegisterMentorResponse> {
    return this._http.post<RegisterMentorResponse>(`${this._apiUrl}/api/profile/mentor`, request)
  }
}
