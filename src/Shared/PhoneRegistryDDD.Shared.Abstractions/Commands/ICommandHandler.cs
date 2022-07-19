using System.Threading.Tasks;

namespace PhoneRegistryDDD.Shared.Abstractions.Commands;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task Handle(TCommand command);
}

public interface ICommandHandler<in TCommand, TResult>
    where TCommand : ICommand
{
    Task<TResult> Handle(TCommand command);
}