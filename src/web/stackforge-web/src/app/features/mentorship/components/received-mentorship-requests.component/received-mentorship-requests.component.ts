import { Component, inject, OnInit, signal } from '@angular/core';
import { GetReceivedMentorshipRequestResponse } from '../../models/GetReceivedMentorshipRequest/get-received-mentorship-request.response';
import { MentorshipService } from '../../service/mentorship.service';
import { RequestItemCardComponent } from '../ui/request-item-card.component/request-item-card.component';

@Component({
  selector: 'app-received-mentorship-requests',
  imports: [RequestItemCardComponent],
  templateUrl: './received-mentorship-requests.component.html',
  styleUrl: './received-mentorship-requests.component.scss',
})
export class ReceivedMentorshipRequestsComponent implements OnInit {
  private readonly _mentorshipService = inject(MentorshipService);

  requests = signal<GetReceivedMentorshipRequestResponse[]>([]);
  isLoading = signal(false);
  errorMessage = signal<string | null>(null);

  ngOnInit(): void{
      this.loadReceivedRequests();
  }

  loadReceivedRequests(): void {
    this.isLoading.set(true);
    this.errorMessage.set(null);

    this._mentorshipService.getReceivedMentorshipRequests().subscribe({
      next: (requests) => {
        this.requests.set(requests);
        this.isLoading.set(false);
      },
      error: () => {
        this.requests.set([]);
        this.errorMessage.set('Não foi possível carregar suas solicitações.');
        this.isLoading.set(false);
      }
    });
  }
}
