using Microsoft.AspNetCore.Mvc;
using Registro_Actividad.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public IActionResult BuscarN(string nombre)
        {
            var resultado = _context.Personas.Where(p => p.Nombre.Contains(nombre)).ToList();

            if (resultado.Count == 0)
            {
                TempData["Mensaje"] = $"No se encontraron personas con el nombre '{nombre}'.";
                return RedirectToAction(nameof(Index));
            }

                return View("Index", resultado);
        }

        public IActionResult BuscarE(int edad)
        {
            var match = _context.Personas.Where(p => p.Edad == edad).ToList();

            if (match.Count == 0)
            {
                TempData["Mensaje"] = $"No se encontraron personas con {edad} años de edad.";
                return RedirectToAction(nameof(Index));
            }

            return View("Index", match);
        }

        public IActionResult Delete(int cedula)
        {
            var persona = _context.Personas.Find(cedula);

            if (persona == null)
            {
                TempData["Mensaje"] = $"No se encontró la persona con cédula {cedula}.";
                return RedirectToAction(nameof(Index));
            }

            _context.Personas.Remove(persona);
            _context.SaveChanges();

            TempData["Mensaje"] = $"Persona con cédula {cedula} eliminada correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
