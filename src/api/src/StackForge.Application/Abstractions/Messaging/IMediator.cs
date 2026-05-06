namespace StackForge.Application.Abstractions.Messaging;

public interface IMediator
{
    Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> command);
    
    Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query);
}