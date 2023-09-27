using PostMicroservice.Models;
using PostMicroservice.Repositories;

namespace PostMicroservice.Services
{
    public class PostService
    {
        private readonly PostRepository _postRepository;

        public PostService(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task CreatePost(Post post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            await _postRepository.Create(post);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _postRepository.Index();
        }

        public async Task<Post> GetPostById(int id)
        {
            var post = await _postRepository.FindById(id);

            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }
            return await _postRepository.FindById(id);
        }

        public async Task UpdateUser(Post post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            // Verifique se o usuário existe
            var existingPost = await _postRepository.FindById(post.Id);
            if (existingPost == null)
            {
                throw new InvalidOperationException("Post not found.");
            }

            await _postRepository.Update(post);
        }

        public async Task DeletePost(int id)
        {
            // Verifique se o usuário existe
            var existingPost = await _postRepository.FindById(id);
            if (existingPost == null)
            {
                throw new InvalidOperationException("Post not found.");
            }

            await _postRepository.Delete(id);
        }

    }
}
