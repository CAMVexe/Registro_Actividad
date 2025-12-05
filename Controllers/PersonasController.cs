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
            TempData["Tipo"] = "success";
            return RedirectToAction(nameof(Index));
        }

        // Buscar por nombre
        [HttpPost]
        public IActionResult BuscarN(string nombre)
        {
            var resultado = _context.Personas.Where(p => p.Nombre.Contains(nombre)).ToList();

            if (resultado.Count == 0)
            {
                TempData["Mensaje"] = $"No se encontraron personas con el nombre '{nombre}'.";
                TempData["Tipo"] = "warning";
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
                TempData["Tipo"] = "warning";
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
                TempData["Tipo"] = "warning";
                return RedirectToAction(nameof(Index));
            }

            _context.Personas.Remove(persona);
            _context.SaveChanges();

            TempData["Mensaje"] = $"Persona con cédula {cedula} eliminada correctamente.";
            TempData["Tipo"] = "success";
            return RedirectToAction(nameof(Index));
        }

        // GET: mostrar el formulario de edición
        
        public IActionResult Edit(int cedula)
        {
            var current = _context.Personas.Find(cedula);
            if (current == null)
            {
                TempData["Mensaje"] = $"No se encontró la persona con cédula {cedula}.";
                TempData["Tipo"] = "warning";
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
                TempData["Tipo"] = "warning";
                return RedirectToAction(nameof(Index));
            }

            _context.Entry(existing).CurrentValues.SetValues(model); // Entry(r) = obtiene registro r | CurrentValues = valores actuales del registro | SetValues(m) = asigna los valores del modelo m
            _context.SaveChanges();

            TempData["Mensaje"] = $"Persona '{model.Nombre}' actualizada correctamente.";
            TempData["MensajeTipo"] = "success";
            return RedirectToAction(nameof(Index));
        }

        // Consultas LINQ

        public IActionResult OrderN()
        {
            var ordered = _context.Personas.OrderBy(p => p.Nombre).ToList();
            return View("Index", ordered);
        }

        public IActionResult AgeAvg()
        {
            var avgAge = _context.Personas.Average(p => p.Edad);
            TempData["Mensaje"] = $"La edad promedio es {avgAge:F2} años.";
            TempData["Tipo"] = "success";
            return RedirectToAction(nameof(Index));
        }
    }
}
