namespace StackForge.Application.Abstractions.Persistance
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
