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

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        [HttpPost]
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

        [HttpPost]
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

        // GET: mostrar el formulario de edición
        [HttpGet]
        public IActionResult Edit(int cedula)
        {
            var current = _context.Personas.Find(cedula);
            if (current == null)
            {
                TempData["Mensaje"] = $"No se encontró la persona con cédula {cedula}.";
                return RedirectToAction(nameof(Index));
            }

            return View(current);
        }

        // POST: guardar los cambios enviados desde la vista Edit
        [HttpPost]
        public IActionResult Edit(Persona model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existing = _context.Personas.Find(model.Cedula);
            if (existing == null)
            {
                TempData["Mensaje"] = $"No se encontró la persona con cédula {model.Cedula}.";
                return RedirectToAction(nameof(Index));
            }

            // Actualiza los valores y guarda
            _context.Entry(existing).CurrentValues.SetValues(model);
            _context.SaveChanges();

            TempData["Mensaje"] = $"Persona '{model.Nombre}' actualizada correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
