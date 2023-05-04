using Microsoft.Identity.Client;
using ShareYourSword.Models;

namespace ShareYourSword.Assets.Repositories
{
    public class PostRepository
    {
        private readonly DatabaseContext _context;

        public PostRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts;
        }

        public Post Get(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void Edit(Post post)
        {
            var existingPost = Get(post.Id);

            if (existingPost != null)
            {
                existingPost.Image = post.Image;
                existingPost.Name = post.Name;
                existingPost.Mail = post.Mail;
                existingPost.Comment = post.Comment;

                _context.SaveChanges();
            }
        }

        public void Like(int id) {

        var existingPost = Get(id);

            if (existingPost != null)
            {
                existingPost.Likes += 1;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var post = Get(id);

            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Post> Filter(string name)
        {
            return _context.Posts.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).OrderByDescending(p => p.Date);
        }
    }
}
