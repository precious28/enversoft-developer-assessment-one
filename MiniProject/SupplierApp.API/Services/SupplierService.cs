using System.Collections.Generic;
using System.Threading.Tasks;
using SupplierApp.API.Models;
using SupplierApp.API.Repository;

namespace SupplierApp.API.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repo;

        public SupplierService(ISupplierRepository repo) => _repo = repo;

        public Task<IEnumerable<Supplier>> SearchByNameAsync(string name) =>
            _repo.SearchByNameAsync(name);

        public Task<Supplier?> GetByIdAsync(int id) =>
            _repo.GetByIdAsync(id);

        public Task<int> AddAsync(Supplier supplier) =>
            _repo.AddAsync(supplier);
    }
}
