using Bloggie.Models.Domain;

namespace Bloggie.Models.ViewModels;

public class HomeViewModel
{
    public IEnumerable<BlogPost> Blogs { get; set; }
    public IEnumerable<Tag> Tags { get; set; }
}