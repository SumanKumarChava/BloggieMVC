using Bloggie.Models.Domain;

namespace Bloggie.Repositories;

public interface IBlogRepositiory
{
    Task<IEnumerable<BlogPost>> GetAllAsync();
    
    Task<BlogPost?> GetAsync(Guid id);

    Task<BlogPost> AddAsync(BlogPost blogPost);

    Task<BlogPost?> UpdateAsync(BlogPost blogPost);

    Task<BlogPost?> DeleteAsync(Guid id);
}