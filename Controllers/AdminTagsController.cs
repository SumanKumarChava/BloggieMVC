using Bloggie.Data;
using Bloggie.Models.Domain;
using Bloggie.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Controllers;

public class AdminTagsController : Controller
{
    private readonly BloggieDbContext _bloggieDbContext;

    public AdminTagsController(BloggieDbContext bloggieDbContext)
    {
        _bloggieDbContext = bloggieDbContext;
    }
    // GET
    //[HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    [ActionName("Add")]
    public IActionResult Add(AddTagRequest request)
    {
        _bloggieDbContext.Tags.Add(new Tag()
        {
            Name = request.Name,
            DisplayName = request.DisplayName
        });
        _bloggieDbContext.SaveChanges();
        return View("Add");
    }

    [HttpGet]
    public IActionResult GetTags()
    {
        var tags = _bloggieDbContext.Tags.ToList();
        return View(tags);
    }
    
    
}