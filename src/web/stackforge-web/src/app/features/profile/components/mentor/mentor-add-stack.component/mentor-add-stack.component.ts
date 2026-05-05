import { Component, inject, OnInit, output, signal } from '@angular/core';
import { StackService } from '../../../services/stack.service';
import { Stack } from '../../../models/Stacks/stacks.response';
import { StackChipComponent } from '../../stack-chip.component/stack-chip.component';
import { MentorProfileService } from '../../../services/mentor-profile.service';
import { AddStackToMentorResponse } from '../../../models/Stacks/add-stack-mentor.response';
import { FormButtonComponent } from "../../../../../shared/ui/components/form-button.component/form-button.component";
import { forkJoin } from 'rxjs';
import { ActionButtonComponent } from '../../../../../shared/ui/components/action-button.component/action-button.component';

@Component({
  selector: 'app-mentor-add-stack',
  imports: [StackChipComponent, ActionButtonComponent],
  templateUrl: './mentor-add-stack.component.html',
  styleUrl: './mentor-add-stack.component.scss',
})
export class MentorAddStackComponent implements OnInit {
  stackAdded = output<void>();

  private readonly _stackService = inject(StackService);
  private readonly _mentorProfileService = inject(MentorProfileService);

  stacks = signal<Stack[]>([]);
  selectedStacks = signal<string[]>([]);
  savedStacks = signal<string[]>([]);
  isSaving = signal(false);


  ngOnInit(): void {
    this.loadStacks();
  }

  loadStacks(): void {
    this._stackService.getAllStacks().subscribe({
      next: (response) => {
        this.stacks.set(response)
      }
    });
  }

  selectedStack(stack: Stack): void {
    const isSelected = this.selectedStacks().includes(stack.id);
    if (isSelected) return;

    this.selectedStacks.update(ids => [...ids, stack.id]);
  }

  deselectedStack(stack: Stack): void {
    this.selectedStacks.update(ids => ids.filter(id => id !== stack.id))
  }

  isSelected(stackId: string): boolean {
    return this.selectedStacks().includes(stackId);
  }

  isSaved(stackId: string): boolean {
    return this.savedStacks().includes(stackId);
  }

  submitSelectedStacks(): void {
    const stackIds = this.selectedStacks();

    if (stackIds.length === 0) return;

    this.isSaving.set(true);

    const requests = stackIds.map(stackId => this._mentorProfileService.addStackToMentor(stackId));

    forkJoin(requests).subscribe({
      next: () => {
        this.savedStacks.update(ids => [...ids, ...stackIds]);
        this.selectedStacks.set([]);
        this.isSaving.set(false);
        this.stackAdded.emit();
      },
      error: (error) => {
        console.error('Erro ao salvar stacks', error);
        this.isSaving.set(false);
      }
    });
  }


}
