using Bloggie.Data;
using Bloggie.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Repositories;

public class TagRepository : ITagRepository
{
    private readonly BloggieDbContext _bloggieDbContext;

    public TagRepository(BloggieDbContext bloggieDbContext)
    {
        _bloggieDbContext = bloggieDbContext;
    }
    public async Task<IEnumerable<Tag>> GetAllAsync()
    {
        return  await _bloggieDbContext.Tags.ToListAsync();
    }

    public async Task<Tag?> GetAsync(Guid id)
    {
        var tag = await _bloggieDbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
        return tag;
    }

    public async Task<Tag> AddAsync(Tag tag)
    {
        var dbTag = new Tag()
        {
            Name = tag.Name,
            DisplayName = tag.DisplayName
        };
        await _bloggieDbContext.Tags.AddAsync(dbTag);
        await _bloggieDbContext.SaveChangesAsync();
        return dbTag;
    }

    public async Task<Tag?> UpdateAsync(Tag tag)
    {
        var dbTag = await GetAsync(tag.Id);
        if (dbTag != null)
        {
            dbTag.DisplayName = tag.DisplayName;
            dbTag.Name = tag.Name;
            await _bloggieDbContext.SaveChangesAsync();
            return dbTag;
        }
        return null;
    }

    public async Task<Tag?> DeleteAsync(Guid id)
    {
        var tag = await GetAsync(id);
        if (tag != null)
        {
            _bloggieDbContext.Tags.Remove(tag);
            await _bloggieDbContext.SaveChangesAsync();
            return tag;
        }

        return null;

    }
}