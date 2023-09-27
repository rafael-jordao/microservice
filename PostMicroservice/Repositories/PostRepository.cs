using PostMicroservice.Data;
using Microsoft.EntityFrameworkCore;
using PostMicroservice.Models;

namespace PostMicroservice.Repositories
{
    public class PostRepository
    {
        private readonly PostContext _context;

        public PostRepository(PostContext context)
        {
            _context = context;
        }

        public async Task Create(Post post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> Index()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> FindById(int id)
        {
            return await _context.Posts.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Update(Post post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(u => u.Id == id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

    }
}
