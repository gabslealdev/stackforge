import { Component, inject, OnInit, signal } from '@angular/core';
import { MentorshipService } from '../../service/mentorship.service';
import { GetSentMentorshipRequestResponse } from '../../models/GetSentMentorshipRequest/get-sent-mentorship-request.response';
import { RequestItemCardComponent } from '../ui/request-item-card.component/request-item-card.component';

@Component({
  selector: 'app-sent-mentorship-requests',
  imports: [RequestItemCardComponent],
  templateUrl: './sent-mentorship-requests.component.html',
  styleUrl: './sent-mentorship-requests.component.scss',
})
export class SentMentorshipRequestsComponent implements OnInit {
  private readonly _mentorshipService = inject(MentorshipService);

  requests = signal<GetSentMentorshipRequestResponse[]>([]);
  isLoading = signal(false);
  errorMessage = signal<string | null>(null);

  ngOnInit(): void {
    this.loadSentRequests();
  }

  loadSentRequests(): void {
    this.isLoading.set(true);
    this.errorMessage.set(null);

    this._mentorshipService.getSentMentorshipRequests().subscribe({
      next: requests => {
        this.requests.set(requests);
      },
      error: () => {
        this.requests.set([]);
        this.errorMessage.set('Não foi possível carregar suas solicitações.');
        this.isLoading.set(false);
      },
      complete: () => {
        this.isLoading.set(false);
      }
    });
  }
}
