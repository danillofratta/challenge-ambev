namespace Ambev.DeveloperEvaluation.Base.Messaging;

public interface ICommand
{
    Guid Id { get; }
    DateTime Timestamp { get; }
}

