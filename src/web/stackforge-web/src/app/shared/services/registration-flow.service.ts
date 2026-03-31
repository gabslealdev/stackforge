import { Injectable } from '@angular/core';
import { ProfileType } from '../../features/identity/models/enums/profile-type.enum';

@Injectable({
  providedIn: 'root',
})
export class RegistrationFlowService {
  private selectedProfileType: ProfileType | null = null;
  private userId: string | null = null;

  setSelectedProfileType(profileType: ProfileType): void {
    this.selectedProfileType = profileType
  }

  getSelectedProfileType(): ProfileType | null {
    return this.selectedProfileType;
  }

  setUserId(userId: string): void {
    this.userId = userId;
  }

  getUserId(): string | null {
    return this.userId;
  }

  clear(): void {
    this.selectedProfileType = null;
    this.userId = null;
  }
}
