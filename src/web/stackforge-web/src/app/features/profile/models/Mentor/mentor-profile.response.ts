export interface MentorStackResponse{
    id: string;
    name: string;
    key: string;
}

export interface GetCurrentMentorResponse{
    userId: string;
    fullName: string;
    courseName: string;
    institution: string;
    bio: string;
    availability: string;
    stacks: MentorStackResponse[];
}