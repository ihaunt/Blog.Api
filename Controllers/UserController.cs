using Crud.User.Context;
using Crud.Blog.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Crud.User.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase // entail 
    {
        private readonly UserContext _context;
        //Private (read-only) attribute of type UserContext to call it _context
        public UserController(UserContext context) // parameters 
        {
            _context = context;            
            //Constructor for connect the Controller and Context
        }

        [HttpPost("CreatUser")]
        public IActionResult CreateUser (Blogger blogger) // parameters 
        {
            _context.Add(blogger);
            _context.SaveChanges();
            return CreatedAtAction(nameof(blogger), new { id = blogger.Id }, blogger);
            // User creation
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser (int id, Blogger userContent)
        {
            var userAdded = _context.Bloggers.Find(id);
             if (userAdded == null)
                return NotFound();

                userAdded.Name = userContent.Name;
                userAdded.Email = userContent.Email;
                userAdded.Password = userContent.Password;

                _context.SaveChanges();
            return Ok(userAdded);    
        }
        [HttpDelete("{DeleteUser}")]
        public IActionResult DeleteUser(int id)
        {
            var userAdded = _context.Bloggers.Find(id);
            if (userAdded == null)
                return NotFound();

                _context.Bloggers.Remove(userAdded);
                _context.SaveChanges();    
            return NoContent();
        }

        [HttpPost("CreatePost")]
        public IActionResult CreatePost(Post post) // parameters 
        {
            _context.Add(post);
            _context.SaveChanges();
            return CreatedAtAction(nameof(TakeTitle), new { id = post.Id }, post);
            // Post creation
        }

            [HttpGet("SearchforTitle")]
        public IActionResult TakeTitle (string title) // parameters 
        {
            var takePosts = _context.Posts.Where(x => x.Title.Contains(title));
            return Ok(takePosts);            // Select with Title
        }

        [HttpGet("ListPostUser")] 
        public IActionResult ListPostsUser (string title)
        {
            var posts = _context.Posts
                .Include(post => post.Bloggers) // Include the Blogger relationship
                .Where(post => post.Title.Contains(title) && post.Content.Contains(title))
                .Select(post => new
                {
                    date = post.PublishedOn.Date,
                    post.Title,
                    post.Content,
                    BloggerName = post.Bloggers.Name // Include the Blogger's Name in the result
                })
                .ToList();

            if (posts == null) // Check for empty result list
                return NotFound();

            return Ok(posts);
        }

        [HttpPut("{EditPost}")]
        public ActionResult EditPost (int id, Post posted)
        {
            var existingPost = _context.Posts.Find(id);

            if (existingPost == null)
                return NotFound();

                existingPost.Content = posted.Content;
                existingPost.Title = posted.Title;
                existingPost.Archived = posted.Archived;
                            
                _context.Posts.Update(existingPost);
                _context.SaveChanges();

            return Ok(existingPost); 
        }

        [HttpDelete("DeletePost")]
        public IActionResult DeletePost (int id)
        {

            var existingPost = _context.Posts.Find(id); // Find the post with the id 
            if (existingPost == null)
                return NotFound();


                _context.Posts.Remove(existingPost);
                _context.SaveChanges();
            return NoContent(); 
        }

    }

}