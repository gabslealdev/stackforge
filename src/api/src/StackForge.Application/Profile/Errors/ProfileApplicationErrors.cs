using StackForge.Application.Shared.Results;

namespace StackForge.Application.Profile.Errors
{
    public static class ProfileApplicationErrors
    {
        public static readonly Error UserNotFound = new("User.UserNotFound", "User was not found.");
        public static readonly Error MentorNotFound = new("Mentor.MentorNotFound", "Mentor was not found.");
        public static readonly Error UserRegistrationNotFound = new("Profile.ProfileTypeNotFound", "User reference not found.");
        public static readonly Error ProfileInvalid = new("Profile.InvalidProfileType", "Invalid profile type.");
        public static readonly Error ProfileAlreadyExist = new("ProfileAlreadyExists", "Profile already exist.");
        public static readonly Error StackNotFound = new("Stack.StackNotFound", "Stack was not found.");
    }
}
