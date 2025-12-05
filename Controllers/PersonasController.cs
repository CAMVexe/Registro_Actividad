using Microsoft.AspNetCore.Mvc;
using Registro_Actividad.Models;

namespace Registro_Actividad.Controllers
{
    public class PersonasController : Controller
    {
        private readonly PeopleContext _context;
        public PersonasController(PeopleContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var personas = _context.Personas.ToList();
            return View(personas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Persona model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _context.Personas.Add(model);
            _context.SaveChanges();

            TempData["Mensaje"] = $"Persona '{model.Nombre}' registrada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        
    }
}
