using System.Collections.Generic;
using System.Threading.Tasks;
using SupplierApp.API.Models;

namespace SupplierApp.API.Repository
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> SearchByNameAsync(string name);
        Task<Supplier?> GetByIdAsync(int id);
        Task<int> AddAsync(Supplier supplier);
    }
}
