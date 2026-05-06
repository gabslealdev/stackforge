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
    
    public async Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> command)
    {
        var commandType = command.GetType();
        
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(commandType, typeof(TResponse));
        
        var method = handlerType.GetMethod("HandleAsync");
        
        if(method is null)
            throw new InvalidOperationException($"Handler {handlerType} not found");
        
        var task = (Task<TResponse>)method.Invoke(command, [command])!;
        
        return await task;
    }

    public async Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query)
    {
        var queryType = query.GetType();

        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResponse));

        var method = handlerType.GetMethod("HandleAsync");
        
        if(method is null)
            throw new InvalidOperationException($"Handler {handlerType} not found");
        var task = (Task<TResponse>)method.Invoke(handlerType, [query])!;

        return await task;
    }
}