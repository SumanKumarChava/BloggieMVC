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
            if (request.SelectedTags!= null && request.SelectedTags.Any(x => x == item.Id.ToString()))
            {
                tags.Add(item);
            }
        }

        blogpost.Tags = tags;    
        var blogPost = await _blogRepositiory.AddAsync(blogpost);
        return RedirectToAction("List");
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var blogPosts = await _blogRepositiory.GetAllAsync();
        return View(blogPosts);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var blogPost = await _blogRepositiory.GetAsync(id);
        if (blogPost != null)
        {
            var blog = new AddBlogPostRequest()
            {
                Id = id,
                Author = blogPost.Author,
                ShortDescription = blogPost.ShortDescription,
                PublishedDate = blogPost.PublishedDate,
                UrlHandle = blogPost.UrlHandle,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PageTitle = blogPost.PageTitle,
                Content = blogPost.Content,
                Heading = blogPost.Heading,
            };
            
            // Assign all available tags
            ICollection<SelectListItem> tags = new List<SelectListItem>();
            foreach (var item in await _tagRepository.GetAllAsync())
            {
                tags.Add(new SelectListItem(){ Text = item.Name, Value = item.Id.ToString()});
            }
            blog.Tags = tags;
            
            // Highlight only selectedTags
            List<string> selectedTags = new List<string>();
            foreach (var item in tags)
            {
                if (blogPost.Tags.Any(x => x.Id.ToString() == item.Value))
                {
                    selectedTags.Add(item.Value);
                }
            }
            blog.SelectedTags = selectedTags.ToArray();
            return View(blog);
        }
        return View("Edit");
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(AddBlogPostRequest request)
    {
        var blogPost = await _blogRepositiory.GetAsync(request.Id);
        if (blogPost != null)
        {
            blogPost.Author = request.Author;
            blogPost.ShortDescription = request.ShortDescription;
            blogPost.PublishedDate = request.PublishedDate;
            blogPost.UrlHandle = request.UrlHandle;
            blogPost.FeaturedImageUrl = request.FeaturedImageUrl;
            blogPost.IsVisible = request.IsVisible;
            blogPost.PageTitle = request.PageTitle;
            blogPost.Content = request.Content;
            blogPost.Heading = request.Heading;
            
            // Assign all available tags
            ICollection<Tag> tags = new List<Tag>();
            foreach (var item in await _tagRepository.GetAllAsync())
            {
                if (request.SelectedTags != null && request.SelectedTags.Any(t => t == item.Id.ToString()))
                {
                    tags.Add(item);
                }
            }
            blogPost.Tags = tags;
            await _blogRepositiory.UpdateAsync(blogPost);
            return RedirectToAction("List");
        }
        return View("Edit");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var blog = await _blogRepositiory.DeleteAsync(id);
        if (blog != null)
        {
            return RedirectToAction("List");
        }
        return View("List");
    }
    
}