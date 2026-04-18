import { Component, Input, output } from '@angular/core';

@Component({
  selector: 'app-action-button',
  imports: [],
  templateUrl: './action-button.component.html',
  styleUrl: './action-button.component.scss',
})
export class ActionButtonComponent {
  @Input() label: string = 'Button';
  @Input() disabled: boolean = false;

  clicked = output<void>();

  onClick(): void {
    if (this.disabled) return;

    this.clicked.emit();
  }
}
