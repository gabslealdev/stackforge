export interface GetSentMentorshipRequestResponse {
  mentorshipRequestId: string;
  mentorId: string;
  mentorName: string;
  stackId: string;
  stackName: string;
  goal: string;
  status: string;
  createdAt: string;
  decidedAt: string | null;
}
