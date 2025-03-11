namespace Ambev.DeveloperEvaluation.Base.Messaging;

public interface IEventHandler<in T> where T : IEvent
{
    Task HandleAsync(T @event, CancellationToken cancellationToken = default);
}