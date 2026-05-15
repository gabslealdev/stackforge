export interface SearchMentorByStacksResponse {
  mentorId: string;
  fullName: string;
  courseName: string;
  institution: string;
  stacks: MentorStackResponse[];
}

export interface MentorStackResponse {
  stackId: string;
  name: string;
  key: string;
}