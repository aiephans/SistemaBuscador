using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaBuscador.Repositories;

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
        [Required(ErrorMessage = "Debe seleccionar una opción")]
        public int? RolId { get; set; }
        [Display(Name = "País")]
        [BindProperty]
        [Required(ErrorMessage = "Debe seleccionar una opción")]
        public int? PaisId { get; set; }
        [Display(Name ="Contraseña")]
        [BindProperty]
        [Required(ErrorMessage = "El campo Contraseña es requerido")]
        [MinLength(8,ErrorMessage ="La contraseña debe tener al menos 8 caracteres")]
        [RegularExpression("^(?=\\w*\\d)(?=\\w*[A-Z])(?=\\w*[a-z])\\S{8,16}$",ErrorMessage ="La contraseña debe tener al menos una letra mayuscula,numeros y minusculas")]
        public string Password { get; set; }
        [Display(Name = "Repetir contraseña")]
        [BindProperty]
        [Required(ErrorMessage = "El campo repetir contraseña es requerido")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        [RegularExpression("^(?=\\w*\\d)(?=\\w*[A-Z])(?=\\w*[a-z])\\S{8,16}$", ErrorMessage = "La contraseña debe tener al menos una letra mayuscula,numeros y minusculas")]
        public string RePassword { get; set; }
        public SelectList Roles { get; set; }
        public SelectList Paises { get; set; }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("sessionId")))
            {
                return RedirectToPage("./Index");
            }

            var rolRepo = new RolRepositorio();
            var list = rolRepo.ObtenerRoles();
            Roles = new SelectList(list,"Id","Nombre");
            var paisRepo = new PaisRepositorio();
            var listPais = paisRepo.ObtenerPaises();
            this.Paises = new SelectList(listPais, "Id", "Nombre");

            return Page();
        }

        public IActionResult Onpost()
        {
            if (ModelState.IsValid)
            {
                var repo = new UsuariosRepository();
                string password = this.Password;
                string password2 = this.RePassword;

                if (password != password2)
                {
                    ModelState.AddModelError(string.Empty, "Las contraseñas no coinciden");
                    return Page();
                }
                //Consultar si existe en nombre usuario
                if (repo.NombreUsuarioExiste(this.NombreUsuario))
                {
                    ModelState.AddModelError(string.Empty, "El nombre de usuario ya se encuentra registrado en la base de datos");
                    return Page();
                }

                //Guardar el usuario en la BD
                
                repo.InsertUsuario(this.Nombres, this.Apellidos, this.NombreUsuario, (int)this.RolId, (int)this.PaisId,this.Password);
                return RedirectToPage("./Usuarios");
            }
            return Page();
        }
    }
}
