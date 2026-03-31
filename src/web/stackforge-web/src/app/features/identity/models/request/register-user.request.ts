import { ProfileType } from "../enums/profile-type.enum";

export interface RegisterUserRequest{
    email: string,
    password: string,
    selectedProfileType: ProfileType
}