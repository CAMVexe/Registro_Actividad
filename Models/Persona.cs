using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Registro_Actividad.Models;

public partial class Persona
{
    [Required(ErrorMessage = "Para poder agregar a la persona es necesario ingresar el número de cédula")]
    [RegularExpression(@"^\d0\d{3}0\d{3}$", ErrorMessage = "El formato de la cédula debe ser con 0 y sin espacios")]
    public int Cedula { get; set; }

    [Required(ErrorMessage = "Para poder agregar a la persona es necesario ingresar el nombre")]
    [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 caracteres")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "Para poder agregar a la persona es necesario ingresar los apellidos")]
    [MinLength(3, ErrorMessage = "El apellido debe tener al menos 3 caracteres")]
    public string? Apellidos { get; set; }

    [Required(ErrorMessage = "Para poder agregar a la persona es necesario ingresar la edad")]
    [Range(1, 120, ErrorMessage = "La edad debe estar entre 0 y 120 años")]
    public int? Edad { get; set; }

    [Required(ErrorMessage = "Para poder agregar a la persona es necesario ingresar el teléfono")]
    [Range(10000000, 99999999, ErrorMessage = "El número debe tener exactamente 8 dígitos.")]
    public int? Telefono { get; set; }

    [Required(ErrorMessage = "Para poder agregar a la persona es necesario ingresar el correo electrónico")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.com$", ErrorMessage = "Recuerde utilizar @email.com siendo 'email' su dominio (ej. gmail)")]
    public string? Email { get; set; }
}
