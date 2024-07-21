using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bloggie.Models;
using Bloggie.Models.ViewModels;
using Bloggie.Repositories;

namespace Bloggie.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBlogRepositiory _blogRepository;
    private readonly ITagRepository _tagRepository;

    public HomeController(ILogger<HomeController> logger, IBlogRepositiory blogRepository,ITagRepository tagRepository)
    {
        _logger = logger;
        _blogRepository = blogRepository;
        _tagRepository = tagRepository;
    }

    public async Task<IActionResult> Index()
    {
        var allBlogs = await _blogRepository.GetAllAsync();
        var allTags = await _tagRepository.GetAllAsync();
        var model = new HomeViewModel();
        model.Tags = allTags;
        model.Blogs = allBlogs;
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}