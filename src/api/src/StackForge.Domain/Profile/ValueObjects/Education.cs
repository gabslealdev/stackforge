using StackForge.Domain.Profile.Enums;
using StackForge.Domain.Profile.Errors;
using StackForge.Domain.Shared.Exceptions;
using StackForge.Domain.Shared.ValueObjects;

namespace StackForge.Domain.Profile.ValueObjects
{
    public sealed record Education : ValueObject
    {
        private const int MinLength = 2;
        private const int MaxLength = 100;
        public string CourseName { get;  private set; }
        public string Institution { get;  private set; }
        public EducationStatus Status { get; private set; }
        public DateOnly ConclusionDate { get; private set; }

        private Education(string courseName, string institution, EducationStatus status, DateOnly conclusionDate )
        {
            CourseName = courseName;
            Institution = institution;
            Status = status;
            ConclusionDate = conclusionDate;
        }

        private Education() { }

        public static Education Create(string courseName, string institution, EducationStatus status, DateOnly conclusionDate)
        {
            var normalizedCourseName = Normalize(courseName);
            var normalizedInstitution = Normalize(institution);
            Validate(normalizedCourseName, normalizedInstitution);
            ValidateConclusionDate(status, conclusionDate);

            return new Education(normalizedCourseName, normalizedInstitution, status, conclusionDate);
        }

        private static string Normalize(string value) 
            => value?.Trim() ?? string.Empty;

        private static void Validate(string courseName, string institution)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(courseName), EducationError.EducationCourseRequired);
            DomainExceptionValidation.When(courseName.Length < MinLength, EducationError.EducationCourseTooShort);
            DomainExceptionValidation.When(courseName.Length > MaxLength, EducationError.EducationCourseTooLong);
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(institution), EducationError.EducationInstitutionRequired);
            DomainExceptionValidation.When(institution.Length < MinLength, EducationError.EducationInstitutionTooShort);
            DomainExceptionValidation.When(institution.Length > MaxLength, EducationError.EducationInstitutionTooLong);
        }

        private static void ValidateConclusionDate(EducationStatus status, DateOnly conclusionDate)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            if (status == EducationStatus.Completed)
            {
                DomainExceptionValidation.When(conclusionDate > today, EducationError.EducationConclusionDateInvalid);
            }
        }

    }
}
