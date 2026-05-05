using StackForge.Domain.Shared.Errors;

namespace StackForge.Domain.ProfileContext.Errors
{
    public static class EducationError
    {
        public readonly static DomainError EducationCourseRequired = new(Code: "Education.Course.Required", Message: "Course name is required for each education entry.");
        public readonly static DomainError EducationInstitutionRequired = new(Code: "Education.Institution.Required", Message: "Institution is required for each education entry.");
        public readonly static DomainError EducationConclusionDateInvalid = new(Code: "Education.ConclusionDate.Invalid", Message: "Conclusion date must be in the past for each education entry.");
        public readonly static DomainError EducationCourseTooShort = new(Code: "Education.CourseName.IsTooShort", Message: "Course name must be at least 3 characters long.");
        public readonly static DomainError EducationCourseTooLong = new(Code: "Education.CourseName.IsTooLong", Message: "Course name must be at most 100 characters long.");
        public readonly static DomainError EducationInstitutionTooShort = new(Code: "Education.Institution.IsTooShort", Message: "Institution name must be at least 3 characters long.");
        public readonly static DomainError EducationInstitutionTooLong = new(Code: "Education.Institution.IsTooLong", Message: "Institution name must be at most 100 characters long.");
    }
}
