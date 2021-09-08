using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaBuscador.Repositories;

namespace SistemaBuscador.Pages
{
    public class ActualizarUsuarioModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }
        [Display(Name = "Nombre usuario")]
        [BindProperty]
        [Required(ErrorMessage = "El campo nombre usuario es requerido")]
        public string NombreUsuario { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "El campo nombres es requerido")]
        public string Nombres { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "El campo apellidos es requerido")]
        public string Apellidos { get; set; }
        [Display(Name = "Rol")]
        [BindProperty]
        [Required(ErrorMessage = "Debe seleccionar una opción")]
        public int? RolId { get; set; }
        [Display(Name = "Contraseña")]
        [BindProperty]
        [Required(ErrorMessage = "El campo Contraseña es requerido")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        [RegularExpression("^(?=\\w*\\d)(?=\\w*[A-Z])(?=\\w*[a-z])\\S{8,16}$", ErrorMessage = "La contraseña debe tener al menos una letra mayuscula,numeros y minusculas")]
        public string Password { get; set; }
        [Display(Name = "Repetir contraseña")]
        [BindProperty]
        [Required(ErrorMessage = "El campo repetir contraseña es requerido")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        [RegularExpression("^(?=\\w*\\d)(?=\\w*[A-Z])(?=\\w*[a-z])\\S{8,16}$", ErrorMessage = "La contraseña debe tener al menos una letra mayuscula,numeros y minusculas")]
        public string RePassword { get; set; }

        public IActionResult OnGet(int id)
        {
           if (string.IsNullOrEmpty(HttpContext.Session.GetString("sessionId")))
            {
                return RedirectToPage("./Index");
            }

            if (id == 0)
            {
                return RedirectToPage("./Usuarios");
            }

            var repo = new UsuariosRepository();
            var usuario = repo.ObtenerUsuarioPorId(id);
            this.Id = usuario.Id;
            this.NombreUsuario = usuario.NombreUsuario;
            this.Nombres = usuario.Nombres;
            this.Apellidos = usuario.Apellidos;
            this.RolId = usuario.RolId;
            this.Password = usuario.Password;
            this.RePassword = usuario.Password;

            //Ir a buscar el registro a la BD

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

                //repo.InsertUsuario(this.Nombres, this.Apellidos, this.NombreUsuario, (int)this.RolId, this.Password);
                repo.UpdateUsuario(this.Id, this.Nombres, this.Apellidos, (int)this.RolId, this.Password);
                return RedirectToPage("./Usuarios");
            }
            return Page();
        }

    }
}
