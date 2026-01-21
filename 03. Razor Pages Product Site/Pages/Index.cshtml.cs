using _03._Razor_Pages_Product_Site.Models;
using _03._Razor_Pages_Product_Site.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _03._Razor_Pages_Product_Site.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProductService _service;

        public IndexModel(ProductService service)
        {
            _service = service;
        }

        public void OnGet(Product product)
        {
            _service.AddProduct(product);
        }
    }
}
