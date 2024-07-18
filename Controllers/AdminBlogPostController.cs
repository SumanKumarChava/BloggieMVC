using Bloggie.Models.ViewModels;
using Bloggie.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Controllers;

public class AdminBlogPostController : Controller
{
    private readonly ITagRepository _tagRepository;

    public AdminBlogPostController(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var tags = await _tagRepository.GetAllAsync();
        var model = new AddBlogPostRequest();
        model.Tags = tags.Select(t => new SelectListItem() { Text = t.Name, Value = t.Id.ToString() });
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddBlogPostRequest request)
    {
        var a = request;
        return View("Add");
    }
    
}