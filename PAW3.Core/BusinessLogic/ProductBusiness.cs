using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using PAW3.Models.Entities;
using PAW3.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAW3.Models.DTO;

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
    Task<ProductDTO> GetProducts(int? id);
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
    public async Task<ProductDTO> GetProducts(int? id)
    {
        var hasId = id.HasValue;
        var productDto = new ProductDTO();

        var products = !hasId
            ? await repositoryProduct.ReadAsync()
            : [await repositoryProduct.FindAsync((int)id)];

        if (!hasId && products != null && products.Any())
        {
            productDto.Summaries.AddRange(products.Select(x => new
            {
                Id = x.ProductId,
                Name = x.ProductName,
                x.Rating
            })
            .GroupBy(y => y.Rating)
            .SelectMany(g => g.Select(sub => new ProductSummary
            {
                Id = sub.Id,
                Name = sub.Name,
                Rating = sub.Rating,
                Count = g.Count()
            })).OrderByDescending(x => x.Count));

            // Big Operation N^2 example

            /*foreach (var item in items)
            {
                foreach (var subItem in item)
                {
                    productDto.Summaries.Add(new ProductSummary()
                    {
                        Id = item.Key,
                        Name = subItem.Name,
                        Rating = subItem.Rating
                    });
                }
            }*/
        }

        productDto.Products = products;
        return productDto;
    }
}

