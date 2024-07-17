using Bloggie.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Controllers;

public class AdminTagsController : Controller
{
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
        var name = request.Name;
        var displayName = request.DisplayName;
        return View("Add");
    }
    
    
}