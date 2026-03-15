import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LucideAngularModule, User, GraduationCapIcon } from 'lucide-angular';
import { RegistrationFlow } from '../../../data/services/registration-flow';
import { ProfileType } from '../../../domain/enums/profile-type.enum';

@Component({
  selector: 'app-select-profile-type',
  imports: [LucideAngularModule],
  templateUrl: './select-profile-type.html',
  styleUrl: './select-profile-type.css',
})
export class SelectProfileType {
    constructor(
     private readonly router: Router,
     private readonly registrationFlowService: RegistrationFlow
  ){}

  readonly User = User;
  readonly GraduationCap = GraduationCapIcon;

  selectProfile(profileType: ProfileType): void{
    this.registrationFlowService.setSelectedProfileType(profileType);
    this.router.navigate(['/register/user'])
    console.log("escolheu" + profileType);
  }

  protected readonly profileType = ProfileType;
}
