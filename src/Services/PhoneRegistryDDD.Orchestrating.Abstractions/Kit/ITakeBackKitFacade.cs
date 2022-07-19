using System.Threading.Tasks;
using PhoneRegistryDDD.Helpdesk.Core.Commands;

namespace PhoneRegistryDDD.Orchestrating.Abstractions.Kit;

public interface ITakeBackKitFacade
{
    Task<bool> TakeBack(TakeBackKitCommand takeBackKitCommand);
}