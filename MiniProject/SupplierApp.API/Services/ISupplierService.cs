using System.Collections.Generic;
using System.Threading.Tasks;
using SupplierApp.API.Models;

namespace SupplierApp.API.Services
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> SearchByNameAsync(string name);
        Task<Supplier?> GetByIdAsync(int id);
        Task<int> AddAsync(Supplier supplier);
    }
}
