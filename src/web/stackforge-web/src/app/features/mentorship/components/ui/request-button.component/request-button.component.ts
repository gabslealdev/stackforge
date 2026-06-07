import { Component, Input, output } from '@angular/core';
import { LucidePlus, LucideSendHorizontal } from '@lucide/angular';

@Component({
  selector: 'app-request-button',
  imports: [LucideSendHorizontal],
  templateUrl: './request-button.component.html',
  styleUrl: './request-button.component.scss',
})
export class RequestButtonComponent {
  @Input() label: string = 'Enviar Pedido'

  clicked = output<void>();


  onClick(): void {
    this.clicked.emit();
  }
}
