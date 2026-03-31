export interface RegisterMentorRequest {
    firstName: string;
    lastName: string;
    birthDate: string;
    courseName: string;
    institution: string;
    educationStatus: number;
    conclusionDate: string;
    bio: string | null;
}