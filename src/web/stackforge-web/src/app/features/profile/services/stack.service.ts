import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { Observable } from 'rxjs';
import { Stack } from '../models/Stacks/stacks.response';

@Injectable({
  providedIn: 'root',
})
export class StackService {
  private readonly _http = inject(HttpClient);
  private readonly _apiUrl = environment.apiUrl;

  getAllStacks(): Observable<Stack[]> {
    return this._http.get<Stack[]>(`${this._apiUrl}/api/stacks`);
  }
}
