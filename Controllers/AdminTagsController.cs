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
        return RedirectToAction("GetTags");
    }

    [HttpGet]
    public IActionResult GetTags()
    {
        var tags = _bloggieDbContext.Tags.ToList();
        return View(tags);
    }
    
    [HttpGet]
    public IActionResult Edit(string Id)
    {
        Guid tagId = new Guid(Id);
        var tag = _bloggieDbContext.Tags.ToList().FirstOrDefault(t => t.Id == tagId);
        return View(tag);
    }

    [HttpPost]
    [ActionName("Edit")]
    public IActionResult Edit(Tag tag)
    {
        var dbTag = _bloggieDbContext.Tags.FirstOrDefault((t => t.Id == tag.Id));
        if (dbTag != null)
        {
            dbTag.DisplayName = tag.DisplayName;
            dbTag.Name = tag.Name;
            _bloggieDbContext.SaveChanges();
            return RedirectToAction("GetTags");
        }

        return RedirectToAction("Edit", new
        {
            Id = tag.Id
        });
    }
    
    [HttpPost]
    [ActionName("Delete")]
    public IActionResult Delete(string Id)
    {
        Guid tagId = new Guid(Id);
        var tag = _bloggieDbContext.Tags.ToList().FirstOrDefault(t => t.Id == tagId);
        if (tag != null)
        {
            _bloggieDbContext.Tags.Remove(tag);
            _bloggieDbContext.SaveChanges();
            return RedirectToAction("GetTags");
        }
        return RedirectToAction("Edit", new { Id = Id });
    }
    
}