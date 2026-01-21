using _04._MVC_Product_site.Data;
using Microsoft.AspNetCore.Mvc;

namespace _04._MVC_Product_site.Controllers;

public class ProductsController : Controller
{
    private readonly ProductsContext _context;
    public ProductsController(ProductsContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Create()
    {
        return View();
    }
    public IActionResult Details()
    {
        return View();
    }
}
