using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Models;
using UserMicroservice.Services;

namespace UserMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            try
            {
                await _userService.CreateUser(user);

                return Ok(new { Message = "Usuário registrado com sucesso!" });
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
                return StatusCode(500, new { Message = "Um erro foi encontrado ao tentar registrar um usuário." });
            }
        }

        [HttpGet]
        [Authorize] // Aplica a autorização apenas a esta rota GET
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Você pode lidar com erros aqui, como registrar ou retornar um erro personalizado
                return StatusCode(401, $"Acesso negado: {ex.Message}");
            }
        }
    }
}
