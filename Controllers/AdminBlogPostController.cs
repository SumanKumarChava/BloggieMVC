using Bloggie.Models.Domain;
using Bloggie.Models.ViewModels;
using Bloggie.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Controllers;

public class AdminBlogPostController : Controller
{
    private readonly ITagRepository _tagRepository;
    private readonly IBlogRepositiory _blogRepositiory;

    public AdminBlogPostController(ITagRepository tagRepository, IBlogRepositiory blogRepositiory)
    {
        _tagRepository = tagRepository;
        _blogRepositiory = blogRepositiory;
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
        var blogpost = new BlogPost()
        {
            Author = request.Author,
            ShortDescription = request.ShortDescription,
            Content = request.Content,
            Heading = request.Heading,
            PageTitle = request.PageTitle,
            IsVisible = request.IsVisible,
            PublishedDate = request.PublishedDate,
            UrlHandle = request.UrlHandle,
            FeaturedImageUrl = request.FeaturedImageUrl,
        };

        ICollection<Tag> tags = new List<Tag>();
        foreach (var item in await _tagRepository.GetAllAsync())
        {
            if (request.SelectedTags.Any(x => x == item.Id.ToString()))
            {
                tags.Add(item);
            }
        }

        blogpost.Tags = tags;    
        var blogPost = await _blogRepositiory.AddAsync(blogpost);
        return RedirectToAction("Add");
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var blogPosts = await _blogRepositiory.GetAllAsync();
        return View(blogPosts);
    }
    
}