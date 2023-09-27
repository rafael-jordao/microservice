using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostMicroservice.Models;
using PostMicroservice.Services;

namespace PostMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            try
            {
                await _postService.CreatePost(post);

                return Ok(new { Message = "Post criado com sucesso!" });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { Message = "Dados incompletos" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = "Email já existente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Um erro foi encontrado ao tentar criar um post." });
            }
        }

        [HttpGet]
        // Aplica a autorização apenas a esta rota GET
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            try
            {
                var posts = await _postService.GetPosts();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                // Você pode lidar com erros aqui, como registrar ou retornar um erro personalizado
                return StatusCode(401, $"Acesso negado: {ex.Message}");
            }
        }
    }
}
