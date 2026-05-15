import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { Observable } from 'rxjs';
import { GetCurrentLearnerResponse } from '../models/Learner/learner-profile.response';

@Injectable({
  providedIn: 'root',
})
export class LearnerProfileServiceTs {
  private readonly _http = inject(HttpClient)
  private readonly _apiUrl = environment.apiUrl

  getCurrentLearner(): Observable<GetCurrentLearnerResponse> {
    return this._http.get<GetCurrentLearnerResponse>(`${this._apiUrl}/api/profile/learner/me`)
  }
}
