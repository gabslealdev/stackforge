import { Component, input, Input, output } from '@angular/core';
import { LucideSquareX } from "@lucide/angular";
import { Stack } from '../../models/Stacks/stacks.response';

@Component({
  selector: 'app-stack-chip',
  imports: [LucideSquareX],
  templateUrl: './stack-chip.component.html',
  styleUrl: './stack-chip.component.scss',
})
export class StackChipComponent {
  stack = input.required<Stack>();
  selected = input(false);
  saved = input(false);
  disabled = input(false);

  stackSelected = output<Stack>();
  stackDeselected = output<Stack>();

  onClick(): void{
    if (this.disabled()){
      return;
    }

    this.stackSelected.emit(this.stack());
  }

  onRemoveClick(event: MouseEvent): void{
    event.stopPropagation();
    this.stackDeselected.emit(this.stack());
  }
}
