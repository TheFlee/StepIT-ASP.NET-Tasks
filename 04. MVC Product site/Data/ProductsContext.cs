using Microsoft.EntityFrameworkCore;

namespace _04._MVC_Product_site.Data;

public class ProductsContext : DbContext
{
    public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
    {
    }
}
