import { Component, computed, input, output, signal } from '@angular/core';
import { SearchMentorByStacksResponse } from '../../models/SearchMentorByStacks/search-mentor-by-stacks.response';
import { StackChipComponent } from "../../../profile/components/stack-chip.component/stack-chip.component";
import { Stack } from '../../../profile/models/Stacks/stacks.response';
import { SendMentorshipRequestRequest } from '../../models/SendMentorshipRequest/send-mentorship-request.request';

@Component({
  selector: 'app-mentorship-request',
  imports: [StackChipComponent],
  templateUrl: './mentorship-request.component.html',
  styleUrl: './mentorship-request.component.scss',
})
export class MentorshipRequestComponent {
  mentor = input.required<SearchMentorByStacksResponse>();
  selectedStackId = signal<string | null>(null);
  goal = signal('');
  closed = output<void>();
  sendMentorshipRequestSubmitted = output<SendMentorshipRequestRequest>();

  close(): void {
    this.closed.emit();
  }

  selectStack(stack: Stack): void {
    this.selectedStackId.set(stack.id);
  }

  submit(): void {
    const stackId = this.selectedStackId();
    const goal = this.goal().trim();
    
    if (!stackId || !goal) {
      return;
    }

    this.sendMentorshipRequestSubmitted.emit({
      mentorId: this.mentor().mentorId,
      stackId,
      goal
    });

    console.log({
      mentorId: this.mentor().mentorId,
      stackId,
      goal
    });
    
    this.close();
  }

    mentorStacks = computed<Stack[]>(() =>
    this.mentor().stacks.map(stack => ({
      id: stack.stackId,
      name: stack.name,
      key: stack.key
    }))
  );
}
