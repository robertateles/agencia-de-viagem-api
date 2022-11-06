using Microsoft.AspNetCore.Mvc;
using teste.Models;

namespace teste.Controllers
{
    public class HomeEFController : Controller
    {
        private UsuarioDbContext _context;
        
        public HomeEFController(UsuarioDbContext context){
            _context = context;
        }

        public IActionResult Index(){
            var usuario = _context.Usuarios.ToList();
            return View(usuario);
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public IActionResult Create(Usuario usuario){
            _context.Add(usuario);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id){
            if(id == null){
                return NotFound("Id não encontrado");
            }

            var usuario = _context.Usuarios.SingleOrDefault(a => a.Id == id);

            if(usuario == null){
                return NotFound("Usuário não encontrado.");
            }

            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteUser(int id){
            var usuario = _context.Usuarios.SingleOrDefault(a => a.Id == id);
            if(usuario == null){
                return NotFound("Usuário não encontrado");
            }
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id){
            if(id == null){
                return NotFound("Id não encontrado");
            }

            var usuario = _context.Usuarios.SingleOrDefault(a => a.Id == id);

            if(usuario == null){
                return NotFound("Usuário não encontrado.");
            }

            return View(usuario);
        }

        [HttpPost]
        public IActionResult Edit(int id, Usuario usuario){
            if(id != usuario.Id){
                return NotFound("Id não encontrado");
            }
            if(ModelState.IsValid){
                _context.Update(usuario);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }
    }
}