import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { Observable } from 'rxjs';
import { GetCurrentMentorResponse} from '../models/Mentor/mentor-profile.response';
import { AddStackToMentorResponse } from '../models/Stacks/add-stack-mentor.response';

@Injectable({
  providedIn: 'root',
})
export class MentorProfileService {
  private readonly _http = inject(HttpClient)
  private readonly _apiUrl = environment.apiUrl

  getCurrentMentor(): Observable<GetCurrentMentorResponse> {
    return this._http.get<GetCurrentMentorResponse>(`${this._apiUrl}/api/profile/mentor/me`);
  }

  addStackToMentor(stackId: string): Observable<AddStackToMentorResponse> {
    return this._http.post<AddStackToMentorResponse>(`${this._apiUrl}/api/profile/mentor/stacks`, { stackId });
  }

  updateMentorAvailability(isAvailable: boolean): Observable<void>{
      return this._http.patch<void>(`${this._apiUrl}/api/profile/mentor/availability`, { isAvailable })
  }
}
