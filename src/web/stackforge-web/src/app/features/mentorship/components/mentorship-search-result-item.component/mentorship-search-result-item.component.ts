import { Component, input, output, signal } from '@angular/core';
import { SearchMentorByStacksResponse } from '../../models/SearchMentorByStacks/search-mentor-by-stacks.response';
import { RequestButtonComponent } from '../ui/request-button.component/request-button.component';
import { LucideArrowDownFromLine, LucideChevronsDown } from '@lucide/angular';

@Component({
  selector: 'app-mentorship-search-result-item',
  imports: [RequestButtonComponent, LucideChevronsDown],
  templateUrl: './mentorship-search-result-item.component.html',
  styleUrl: './mentorship-search-result-item.component.scss',
})
export class MentorshipSearchResultItemComponent {

  mentor = input.required<SearchMentorByStacksResponse>();
  closed = output<void>();
  expanded = signal(false);

  close(): void {
    this.closed.emit();
  }

  requestMentorship = output<SearchMentorByStacksResponse>();

  toggleExpanded(): void{
    this.expanded.update(value => !value);
  }

  onRequestMentorship(): void{
    this.requestMentorship.emit(this.mentor());
  }

}
