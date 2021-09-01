using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SistemaBuscador.Pages
{
    public class FormularioUsuarioModel : PageModel
    {
        [Display(Name ="Nombre usuario")]
        [BindProperty]
        [Required(ErrorMessage ="El campo nombre usuario es requerido")]
        public string NombreUsuario { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "El campo nombres es requerido")]
        public string Nombres { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "El campo apellidos es requerido")]
        public string Apellidos { get; set; }
        [Display(Name ="Rol")]
        [BindProperty]
        [Required(ErrorMessage = "El campo Rol es requerido")]
        public int RolId { get; set; }
        [Display(Name ="Contraseña")]
        [BindProperty]
        [Required(ErrorMessage = "El campo Contraseña es requerido")]
        public string Password { get; set; }
        [Display(Name = "Repetir contraseña")]
        [BindProperty]
        [Required(ErrorMessage = "El campo reperir contraseña es requerido")]
        public string RePassword { get; set; }

        public void OnGet()
        {
        }

        public IActionResult Onpost()
        {
            if (ModelState.IsValid)
            {
                
            }
            return Page();
        }
    }
}
