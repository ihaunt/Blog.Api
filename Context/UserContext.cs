using Crud.Blog.Entities;
using Microsoft.EntityFrameworkCore;



namespace Crud.User.Context
{
    public class UserContext : DbContext
    {
        public UserContext (DbContextOptions<UserContext>options) : base(options)
        {
            
        }    
        public DbSet <Post> Posts { get; set; }
        public DbSet <Blogger> Bloggers { get; set; }
        
        private const string connString = "Server=localhost\\SQLEXPRESS01;Initial Catalog=ApiBlog; Integrated Security=True;TrustServerCertificate=True; Encrypt=False";
    }                                      
}