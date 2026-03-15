import { ProfileType } from "../../domain/enums/profile-type.enum";

export interface RegisterUserRequest {
    email: string;
    password: string;
    selectedProfileType: ProfileType;
}