using Bloggie.Data;
using Bloggie.Models.Domain;
using Bloggie.Models.ViewModels;
using Bloggie.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Controllers;

public class AdminTagsController : Controller
{
    private readonly ITagRepository _tagRepository;

    public AdminTagsController(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
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
        var addedTag = await _tagRepository.AddAsync(new Tag()
        {
            Name = request.Name,
            DisplayName = request.DisplayName
        });
        return RedirectToAction("GetTags");
    }

    [HttpGet]
    public async Task<IActionResult> GetTags()
    {
        var tags = await _tagRepository.GetAllAsync();
        return View(tags);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        Guid tagId = new Guid(id);
        var tag = await _tagRepository.GetAsync(tagId);
        return View(tag);
    }

    [HttpPost]
    [ActionName("Edit")]
    public async Task<IActionResult> Edit(Tag tag)
    {
        var updatedTag = await _tagRepository.UpdateAsync(tag);
        if (updatedTag != null)
        {
            return RedirectToAction("GetTags");
        }
        else
        {
            return RedirectToAction("Edit", new
            {
                Id = tag.Id
            });
        }
    }
    
    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> Delete(string id)
    {
        Guid tagId = new Guid(id);
        var tag = await _tagRepository.DeleteAsync(tagId);
        if (tag != null)
        {
            return RedirectToAction("GetTags");
        }
        else
        {
            return RedirectToAction("Edit", new { Id = id });
        }
        
    }
    
}