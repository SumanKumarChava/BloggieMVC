using Bloggie.Data;
using Bloggie.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Repositories;

public class BlogRepository : IBlogRepositiory
{
    private readonly BloggieDbContext _bloggieDbContext;

    public BlogRepository(BloggieDbContext bloggieDbContext)
    {
        _bloggieDbContext = bloggieDbContext;
    }
    public async Task<IEnumerable<BlogPost>> GetAllAsync()
    {
        return  await _bloggieDbContext.BlogPosts.ToListAsync();
    }

    public async Task<BlogPost?> GetAsync(Guid id)
    {
        var blogPost = await _bloggieDbContext.BlogPosts.FirstOrDefaultAsync(t => t.Id == id);
        return blogPost;
    }

    public async Task<BlogPost> AddAsync(BlogPost blogPost)
    {
        await _bloggieDbContext.BlogPosts.AddAsync(blogPost);
        await _bloggieDbContext.SaveChangesAsync();
        return blogPost;
    }

    public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
    {
        var dbBlogPost = await GetAsync(blogPost.Id);
        if (dbBlogPost != null)
        {
            dbBlogPost.Author = blogPost.Author;
            dbBlogPost.ShortDescription = blogPost.ShortDescription;
            dbBlogPost.Content = blogPost.Content;
            dbBlogPost.Heading = blogPost.Heading;
            dbBlogPost.PageTitle = blogPost.PageTitle;
            dbBlogPost.IsVisible = blogPost.IsVisible;
            dbBlogPost.PublishedDate = blogPost.PublishedDate;
            dbBlogPost.UrlHandle = blogPost.UrlHandle;
            dbBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
            dbBlogPost.Tags = blogPost.Tags;
            await _bloggieDbContext.SaveChangesAsync();
            return dbBlogPost;
        }
        return null;
    }

    public async Task<BlogPost?> DeleteAsync(Guid id)
    {
        var blogPost = await GetAsync(id);
        if (blogPost != null)
        {
            _bloggieDbContext.BlogPosts.Remove(blogPost);
            await _bloggieDbContext.SaveChangesAsync();
            return blogPost;
        }

        return null;

    }
}