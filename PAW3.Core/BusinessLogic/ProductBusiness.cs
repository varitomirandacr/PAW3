using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using PAW3.Data.Models;
using PAW3.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAW3.Core.BusinessLogic;

public interface IProductBusiness
{
    /// <summary>
    /// Deletes the product associated with the product id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteProductAsync(int id);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IEnumerable<Product>> GetProducts(int? id);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Task<bool> SaveProductAsync(Product product);
}

public class ProductBusiness(IRepositoryProduct repositoryProduct) : IProductBusiness
{
    /// </inheritdoc>
    public async Task<bool> SaveProductAsync(Product product)
    {
        // que tengan mas de 5 quantity
        // sabado o domingo solo puedo salvar de 8 a 12
        return await repositoryProduct.UpdateAsync(product);
    }

    /// </inheritdoc>
    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await repositoryProduct.FindAsync(id);
        return await repositoryProduct.DeleteAsync(product);
    }

    /// </inheritdoc>
    public async Task<IEnumerable<Product>> GetProducts(int? id)
    {
        return id == null
            ? await repositoryProduct.ReadAsync()
            : [await repositoryProduct.FindAsync((int)id)];
    }
}

