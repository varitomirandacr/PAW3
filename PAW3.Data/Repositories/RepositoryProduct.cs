using Microsoft.EntityFrameworkCore;
using PAW3.Data.Models;

namespace PAW3.Data.Repositories;

public interface IRepositoryProduct
{
    Task<bool> UpsertAsync(Product entity, bool isUpdating);
    Task<bool> CreateAsync(Product entity);
    Task<bool> DeleteAsync(Product entity);
    Task<IEnumerable<Product>> ReadAsync();
    Task<Product> FindAsync(int id);
    Task<bool> UpdateAsync(Product entity);
    Task<bool> UpdateManyAsync(IEnumerable<Product> entities);
    Task<bool> ExistsAsync(Product entity);
    Task<bool> CheckBeforeSavingAsync(Product entity);
    //Task<IEnumerable<ProductViewModel>> FilterAsync(Expression<Func<Product, bool>> predicate);
}

public class RepositoryProduct : RepositoryBase<Product>, IRepositoryProduct
{
    public async Task<bool> CheckBeforeSavingAsync(Product entity)
    {
        var exists = await ExistsAsync(entity);
        if (exists)
        {
            // algo mas 
        }

        return await UpsertAsync(entity, exists);
    }

    public async new Task<bool> ExistsAsync(Product entity) 
    {
        return await DbContext.Products.AnyAsync(x => x.ProductId == entity.ProductId);
    }
}
