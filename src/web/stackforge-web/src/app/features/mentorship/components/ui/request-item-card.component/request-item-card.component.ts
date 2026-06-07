import { Component, input } from '@angular/core';
import { GetReceivedMentorshipRequestResponse } from '../../../models/GetReceivedMentorshipRequest/get-received-mentorship-request.response';
import { GetSentMentorshipRequestResponse } from '../../../models/GetSentMentorshipRequest/get-sent-mentorship-request.response';
import { LucideHourglass } from '@lucide/angular';
type RequestCardMode = 'received' | 'sent';

@Component({
  selector: 'app-request-item-card',
  imports: [LucideHourglass],
  templateUrl: './request-item-card.component.html',
  styleUrl: './request-item-card.component.scss',
})
export class RequestItemCardComponent {
 mode = input.required<RequestCardMode>();
 request = input.required<GetReceivedMentorshipRequestResponse | GetSentMentorshipRequestResponse>();

}
