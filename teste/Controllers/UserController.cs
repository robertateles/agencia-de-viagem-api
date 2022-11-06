using Microsoft.AspNetCore.Mvc;
using teste.Models;
using teste.Repository;

namespace teste.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;

        public UserController(IUsuarioRepository repository){
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var usuarios = await _repository.GetUsuarios();
            return usuarios.Any() ? Ok(usuarios) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id){
            var usuario = await _repository.GetUsuarioById(id);
            return usuario != null
            ? Ok(usuario) : NotFound("Usuário não encontrado");
        }

        [HttpPost]
        public async Task<IActionResult> Save(Usuario usuario){
            _repository.AddUsuario(usuario);
            return await _repository.SaveChangeAsync()
             ? Ok("Usuário cadastrado") : BadRequest("Algo deu errado");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Usuario usuario){
            var usuarioExiste = await _repository.GetUsuarioById(id);
            if(usuarioExiste == null) return NotFound("Usuário não encontrado.");

            usuarioExiste.Nome = usuario.Nome ?? usuarioExiste.Nome;
            usuarioExiste.Email = usuario.Email ?? usuarioExiste.Email;
            usuarioExiste.Senha = usuario.Senha ?? usuarioExiste.Senha;

            _repository.AtualizarUsuario(usuarioExiste);

            return await _repository.SaveChangeAsync()
            ? Ok("Usuário atualizado.") : BadRequest("Algo deu errado. Tente novamente!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){
            var usuarioExiste = await _repository.GetUsuarioById(id);
            if(usuarioExiste == null) return NotFound("Usuário não encontrado.");

            _repository.DeletarUsuario(usuarioExiste);

            return await _repository.SaveChangeAsync()
            ? Ok("Usuário deletado.") : BadRequest("Algo deu errado. Tente novamente!");
        }
    }
}