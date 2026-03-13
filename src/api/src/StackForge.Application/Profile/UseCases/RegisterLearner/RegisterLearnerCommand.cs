namespace StackForge.Application.Profile.UseCases.RegisterLearner
{
    public sealed record RegisterLearnerCommand
    {
        public Guid UserId { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public DateOnly BirthDate {  get; init; }
    }
}
