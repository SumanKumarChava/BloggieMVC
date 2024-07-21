using Bloggie.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Controllers;

public class BlogController : Controller
{
    private readonly IBlogRepositiory _blogRepositiory;

    public BlogController(IBlogRepositiory blogRepositiory)
    {
        _blogRepositiory = blogRepositiory;
    }
    // GET
    public async Task<IActionResult> Index(string urlHandle)
    {
        var blog = (await _blogRepositiory.GetAllAsync()).FirstOrDefault(t => t.UrlHandle == urlHandle);
        return View(blog);
    }
}