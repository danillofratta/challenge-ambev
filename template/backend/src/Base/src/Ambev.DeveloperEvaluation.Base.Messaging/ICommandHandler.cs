namespace Ambev.DeveloperEvaluation.Base.Messaging;
public interface ICommandHandler<in T> where T : ICommand
{
    Task HandleAsync(T command, CancellationToken cancellationToken = default);
}