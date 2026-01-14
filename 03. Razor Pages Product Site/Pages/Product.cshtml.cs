using _03._Razor_Pages_Product_Site.Models;
using _03._Razor_Pages_Product_Site.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _03._Razor_Pages_Product_Site.Pages;

public class ProductModel : PageModel
{
    private readonly ProductService _service;
    public Product? Product { get; set; }
    public ProductModel(ProductService service)
    {
        _service = service;
    }
    public async void OnGet(int id)
    {
        Product = await _service.GetProductByIdAsync(id);
    }
}
