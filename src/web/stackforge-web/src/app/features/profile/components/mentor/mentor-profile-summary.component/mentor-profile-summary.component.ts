import { Component, effect, input } from '@angular/core';
import { LucideSquareUserRound } from '@lucide/angular';
import { StackChipComponent } from '../../stack-chip.component/stack-chip.component';
import { GetCurrentMentorResponse } from '../../../models/Mentor/mentor-profile.response';

@Component({
  selector: 'app-mentor-summary',
  imports: [LucideSquareUserRound, StackChipComponent],
  templateUrl: './mentor-profile-summary.component.html',
  styleUrl: './mentor-profile-summary.component.scss',
})
export class MentorProfileSummaryComponent {
  mentor = input.required<GetCurrentMentorResponse>();

}


