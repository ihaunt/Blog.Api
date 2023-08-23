

namespace Crud.Blog.Entities
{
    public class Blogger
    {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public virtual Uri SiteUri { get; set; }

    public ICollection<Post> Posts { get; }
    }    
}