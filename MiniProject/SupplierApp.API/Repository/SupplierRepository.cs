using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using SupplierApp.API.Models;

namespace SupplierApp.API.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly string _connectionString;

        public SupplierRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Supplier>> SearchByNameAsync(string name)
        {
            using var db = Connection;
            return await db.QueryAsync<Supplier>(
                "SELECT * FROM Suppliers WHERE Name LIKE @Name ORDER BY Name",
                new { Name = $"%{name}%" });
        }

        public async Task<Supplier?> GetByIdAsync(int id)
        {
            using var db = Connection;
            return await db.QueryFirstOrDefaultAsync<Supplier>(
                "SELECT * FROM Suppliers WHERE Id = @Id", new { Id = id });
        }

        public async Task<int> AddAsync(Supplier supplier)
        {
            using var db = Connection;
            return await db.ExecuteScalarAsync<int>(
                "INSERT INTO Suppliers (Code, Name, Telephone) VALUES (@Code, @Name, @Telephone); SELECT SCOPE_IDENTITY();",
                supplier);
        }
    }
}
