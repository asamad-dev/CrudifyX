using System.Data;
using CrudifyX.Web.Models;
using Dapper;

namespace CrudifyX.Web.DataAccess
{
    public class ProductRepository
    {

        private readonly IDbConnection _db;

        public ProductRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _db.QueryAsync<Product>("sp_GetAllProducts", commandType: CommandType.StoredProcedure);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _db.QueryFirstOrDefaultAsync<Product>(
                "SELECT * FROM Products WHERE Id = @Id", new { Id = id });
        }

        public async Task InsertProductAsync(Product product)
        {
            await _db.ExecuteAsync("sp_InsertProduct",
                new { product.Name, product.Price, product.Quantity },
                commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _db.ExecuteAsync("sp_UpdateProduct",
                new { product.Id, product.Name, product.Price, product.Quantity },
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _db.ExecuteAsync("sp_DeleteProduct",
                new { Id = id }, commandType: CommandType.StoredProcedure);
        }
    }
}
