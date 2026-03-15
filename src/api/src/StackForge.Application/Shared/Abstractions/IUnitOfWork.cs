namespace StackForge.Application.Shared.Abstractions
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
