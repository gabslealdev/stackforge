import { Component, input } from '@angular/core';
import { LucideSquareUserRound } from '@lucide/angular';
import { GetCurrentLearnerResponse } from '../../../models/Learner/learner-profile.response';

@Component({
  selector: 'app-learner-profile',
  imports: [LucideSquareUserRound],
  templateUrl: './learner-profile-summary.component.html',
  styleUrl: './learner-profile-summary.component.scss',
})
export class LearnerProfileSummaryComponent {
  learner = input.required<GetCurrentLearnerResponse>();
}
