using Bloggie.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Models.ViewModels;

public class AddBlogPostRequest
{
    public Guid Id { get; set; }
    public string Heading { get; set; }
    
    public string PageTitle { get; set; }
    
    public string Content { get; set; }
    
    public string ShortDescription { get; set; }
    
    public string FeaturedImageUrl { get; set; }
    
    public string UrlHandle { get; set; }
    
    public DateTime PublishedDate { get; set; }
    
    public string Author { get; set; }
    
    public bool IsVisible { get; set; }
    
    // Display tags list
    public IEnumerable<SelectListItem> Tags { get; set; }

    public string[] SelectedTags { get; set; }

}