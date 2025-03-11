namespace Ambev.DeveloperEvaluation.Base.Messaging;

public interface IEvent
{
    Guid Id { get; }
    DateTime Timestamp { get; }
}