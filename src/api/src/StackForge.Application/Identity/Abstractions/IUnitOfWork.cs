namespace StackForge.Application.Identity.Abstractions
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
