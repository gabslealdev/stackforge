import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterMentorRequest } from '../../../mentor/data/models/register-mentor.request';
import { RegisterMentorResponse } from '../../../mentor/data/models/register-mentor.response';
import { RegisterLearnerRequest } from '../../../learner/data/models/register-learner.request';
import { RegisterLearnerResponse } from '../../../learner/data/models/register-learner.response';

@Injectable({
  providedIn: 'root',
})
export class ProfileApi {
  private readonly  http = inject(HttpClient);
  private readonly  apiUrl = "http://localhost:5106"

  registerMentor( request: RegisterMentorRequest): Observable<RegisterMentorResponse>{
    return this.http.post<RegisterMentorResponse>(`${this.apiUrl}/api/profile/mentor`, request)
  }

  registerLearner(request: RegisterLearnerRequest): Observable<RegisterLearnerResponse>{
    return this.http.post<RegisterLearnerResponse>(`${this.apiUrl}/api/profile/learner`, request)
  }
}
