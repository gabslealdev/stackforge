import { Component } from '@angular/core';
import { LucideUserRound, LucideGraduationCap } from '@lucide/angular';
import { ProfileType } from '../../models/enums/profile-type.enum';
import { RegistrationFlowService } from '../../../../shared/services/registration-flow.service';
import { Router } from '@angular/router';
import { Header } from '../../../../layout/header/header';

@Component({
  selector: 'app-select-profile.page',
  imports: [LucideUserRound, LucideGraduationCap, Header],
  templateUrl: './select-profile.page.html',
  styleUrl: './select-profile.page.scss',
})
export class SelectProfilePage {
constructor(
  private readonly router: Router,
  private readonly registrationFlowService: RegistrationFlowService
){}

 
  

  selectProfile(profileType: ProfileType): void{
    this.registrationFlowService.setSelectedProfileType(profileType);
    this.router.navigate(['register/user'])
    console.log(profileType)
  }

  protected readonly profileType = ProfileType;
}
