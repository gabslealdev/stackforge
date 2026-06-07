import { Component, inject, signal } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { LucideSearch } from '@lucide/angular';
import { MentorshipService } from '../../service/mentorship.service';
import { SearchStackRequest } from '../../models/SearchStack/search-stack.request';
import { StackChipComponent } from '../../../profile/components/stack-chip.component/stack-chip.component';
import { Stack } from '../../../profile/models/Stacks/stacks.response';
import { SearchMentorByStackRequest } from '../../models/SearchMentorByStacks/search-mentor-by-stacks.request';
import { SearchMentorByStacksResponse } from '../../models/SearchMentorByStacks/search-mentor-by-stacks.response';
import { ActionButtonComponent } from '../../../../shared/ui/components/action-button.component/action-button.component';
import { MentorshipSearchResultItemComponent } from '../mentorship-search-result-item.component/mentorship-search-result-item.component';
import { MentorshipRequestComponent } from '../mentorship-request.component/mentorship-request.component';
import { SendMentorshipRequestRequest } from '../../models/SendMentorshipRequest/send-mentorship-request.request';
import { SendMentorshipRequestResponse } from '../../models/SendMentorshipRequest/send-mentorship-request.response';

@Component({
  selector: 'app-search-mentor',
  imports: [LucideSearch, ReactiveFormsModule, StackChipComponent, ActionButtonComponent,
    MentorshipSearchResultItemComponent, MentorshipRequestComponent],
  templateUrl: './search-mentor.component.html',
  styleUrl: './search-mentor.component.scss',
})
export class SearchMentorComponent {
  private readonly _mentorshipService = inject(MentorshipService)

  searchStackControl = new FormControl<string>('', { nonNullable: true });

  stackList = signal<Stack[]>([]);
  mentorList = signal<SearchMentorByStacksResponse[]>([]);
  selectedStacks = signal<Stack[]>([]);

  isSearchingMentors = signal(false);
  isSearchingStack = signal(false);
  errorMessage = signal<string | null>(null);

  selectedMentor = signal<SearchMentorByStacksResponse | null>(null); 

  isSelected(stack: Stack): boolean {
    return this.selectedStacks().includes(stack);
  }

  onStackSelected(stack: Stack): void {
    this.selectedStacks.update(stacks => [... stacks, stack])
  }

  searchStacks(): void {
    const value = this.searchStackControl.value?.trim();

    console.log('buscando:', value);

    if (!value) return;

    this.isSearchingStack.set(true);

    const request: SearchStackRequest = {
      searchTerm: value
    };

    this._mentorshipService.searchStack(request).subscribe({
      next: (stacks) => {
        this.stackList.set(stacks.map(stack => ({
          id: stack.stackId,
          name: stack.name,
          key: stack.key
        })));
      },
      error: () => {
        this.stackList.set([]);
        this.isSearchingStack.set(false)
      },
      complete: () => {
        this.isSearchingStack.set(false)
      }
    });
  }

  searchMentorByStacks(): void {
  const stackIds = this.selectedStacks().map(stack => stack.id);

  if (stackIds.length === 0) {
    this.errorMessage.set('Selecione pelo menos uma stack.');
    return;
  }

  const request: SearchMentorByStackRequest = {
    stackIds
  };

  this.isSearchingMentors.set(true);
  this.errorMessage.set(null);

  this._mentorshipService.searchMentorByStacks(request)
    .subscribe({
      next: mentors => {
        this.mentorList.set(mentors);
        this.resetSearch();
      },
      error: () => {
        this.mentorList.set([]);
        this.errorMessage.set('Não foi possível buscar os mentores.');
        this.isSearchingMentors.set(false);
      },
      complete: () => {
        this.isSearchingMentors.set(false);
      }
    });
  }

  resetSearch(): void {
  this.searchStackControl.setValue('')
  this.selectedStacks.set([]);
  this.stackList.set([]);
  this.errorMessage.set(null);
  }

  onRequestMentorship(mentor: SearchMentorByStacksResponse): void{
    this.selectedMentor.set(mentor);
  }

    closeMentorshipRequest(): void {
    this.selectedMentor.set(null);
  }

  onMentorshipRequestSubmitted(request: SendMentorshipRequestRequest): void {
    this._mentorshipService.sendMentorshipRequest(request).subscribe({
      next: (response) => {
        alert('Pedido de mentoria enviado com sucesso!');
        console.log('Resposta do pedido de mentoria:', response);
        
      },
      error: () => {
        alert('Não foi possível enviar o pedido de mentoria. Tente novamente mais tarde.');
      }
    });
  }


}

