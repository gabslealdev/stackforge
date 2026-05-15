import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { SearchStackRequest } from '../models/SearchStack/search-stack.request';
import { Observable } from 'rxjs';
import { SearchStackResponse } from '../models/SearchStack/search-stack.response';
import { SearchMentorByStackRequest } from '../models/SearchMentorByStacks/search-mentor-by-stacks.request';
import { SearchMentorByStacksResponse } from '../models/SearchMentorByStacks/search-mentor-by-stacks.response';

@Injectable({
  providedIn: 'root',
})
export class MentorshipService {
  private readonly _http = inject(HttpClient);
  private readonly _apiUrl = environment.apiUrl; 

  searchStack(request: SearchStackRequest): Observable<SearchStackResponse[]> {
    return this._http.post<SearchStackResponse[]>(`${this._apiUrl}/mentorship/search/stack`, request)
  }

  searchMentorByStacks(request: SearchMentorByStackRequest): Observable<SearchMentorByStacksResponse[]>{
    return this._http.post<SearchMentorByStacksResponse[]>(`${this._apiUrl}/mentorship/search/mentor`, request);
  }
}
