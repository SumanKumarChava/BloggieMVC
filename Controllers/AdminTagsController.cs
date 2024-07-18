using Bloggie.Data;
using Bloggie.Models.Domain;
using Bloggie.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> Add(AddTagRequest request)
    {
        await _bloggieDbContext.Tags.AddAsync(new Tag()
        {
            Name = request.Name,
            DisplayName = request.DisplayName
        });
        await _bloggieDbContext.SaveChangesAsync();
        return RedirectToAction("GetTags");
    }

    [HttpGet]
    public async Task<IActionResult> GetTags()
    {
        var tags = await _bloggieDbContext.Tags.ToListAsync();
        return View(tags);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        Guid tagId = new Guid(id);
        var tag = (await _bloggieDbContext.Tags.ToListAsync()).FirstOrDefault(t => t.Id == tagId);
        return View(tag);
    }

    [HttpPost]
    [ActionName("Edit")]
    public async Task<IActionResult> Edit(Tag tag)
    {
        var dbTag = await _bloggieDbContext.Tags.FirstOrDefaultAsync((t => t.Id == tag.Id));
        if (dbTag != null)
        {
            dbTag.DisplayName = tag.DisplayName;
            dbTag.Name = tag.Name;
            await _bloggieDbContext.SaveChangesAsync();
            return RedirectToAction("GetTags");
        }

        return RedirectToAction("Edit", new
        {
            Id = tag.Id
        });
    }
    
    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> Delete(string id)
    {
        Guid tagId = new Guid(id);
        var tag = (await _bloggieDbContext.Tags.ToListAsync()).FirstOrDefault(t => t.Id == tagId);
        if (tag != null)
        {
            _bloggieDbContext.Tags.Remove(tag);
            await _bloggieDbContext.SaveChangesAsync();
            return RedirectToAction("GetTags");
        }
        return RedirectToAction("Edit", new { Id = id });
    }
    
}