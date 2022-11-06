using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using teste.Models;

namespace teste.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Usuarios()
    {
        return View(usuarios);
    }

    private static IList<Usuario> usuarios =
    new List<Usuario>(){
        new Usuario(){
            Id = 1,
            Nome = "Roberta Teles",
            Email = "roberta@email.com",
            Senha = "12345678"
        },
        new Usuario(){
            Id = 2,
            Nome = "Marcia Guimaraes",
            Email = "marcia@email.com",
            Senha = "12345678"
        },
        new Usuario(){
            Id = 3,
            Nome = "Lindemberg Teles",
            Email = "berg@email.com",
            Senha = "12345678"
        },
        new Usuario(){
            Id = 4,
            Nome = "Bruna Lins",
            Email = "bruna@email.com",
            Senha = "12345678"
        }
    };

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
