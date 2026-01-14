using _03._Razor_Pages_Product_Site.Models;
using _03._Razor_Pages_Product_Site.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _03._Razor_Pages_Product_Site.Pages;

public class ProductsModel : PageModel
{
    private readonly ProductService _service;

    public ProductsModel(ProductService service)
    {
        _service = service;
    }
    public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();

    public async Task OnGet()
    {
        Products = await _service.GetProductsAsync();
    }
}
