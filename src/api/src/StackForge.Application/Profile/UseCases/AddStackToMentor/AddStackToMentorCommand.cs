using System.Security.Cryptography.X509Certificates;

namespace StackForge.Application.Profile.UseCases.AddStackToMentor
{
    public sealed record AddStackToMentorCommand(Guid UserId, Guid StackId);
}
 