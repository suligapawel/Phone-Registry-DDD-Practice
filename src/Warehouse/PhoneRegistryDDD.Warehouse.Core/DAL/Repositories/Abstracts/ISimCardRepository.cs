using System.Threading.Tasks;
using PhoneRegistryDDD.Warehouse.Core.Entities;

namespace PhoneRegistryDDD.Warehouse.Core.DAL.Repositories.Abstracts;

public interface ISimCardRepository
{
    Task Add(SimCard simCard);
    Task<bool> ExistsAlready(string number);
}