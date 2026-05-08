using Microsoft.Extensions.DependencyInjection;
using StackForge.Application.Abstractions.Messaging;

namespace StackForge.Infrastructure.Messaging;

public sealed class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
    {
        var commandType = command.GetType();
        
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(commandType, typeof(TResponse));
        
        var handler = _serviceProvider.GetRequiredService(handlerType);
        
        var method = handlerType.GetMethod("HandleAsync");
        
        if (method is null)
            throw new InvalidOperationException($"Handler {handlerType} not found");
        
        if (method.Invoke(handler, [command]) is not Task<TResponse> task)
            throw new InvalidOperationException($"Handler {handler.GetType().Name} not found");
        
        return await task;
    }

    public async Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
    {
        var queryType = query.GetType();

        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResponse));
        
        var handler =  _serviceProvider.GetRequiredService(handlerType);

        var method = handlerType.GetMethod("HandleAsync");
        
        if (method is null)
            throw new InvalidOperationException($"Handler {handlerType} not found");
        
        if (method.Invoke(handler, [query]) is not Task<TResponse> task)
            throw new InvalidOperationException($"Handler {handler.GetType().Name} not found");
        
        return await task;
    }
}
