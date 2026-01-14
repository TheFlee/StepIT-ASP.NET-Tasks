using _03._Razor_Pages_Product_Site.Models;

namespace _03._Razor_Pages_Product_Site.Data;

public interface IProductRepository
{
    public Product AddProduct(Product product);
    public Task<Product> GetProductByIdAsync(int id);
    public Task<IEnumerable<Product>> GetProductsAsync();
}
