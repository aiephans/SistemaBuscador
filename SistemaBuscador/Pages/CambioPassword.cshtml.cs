using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaBuscador.Repositories;
using SistemaBuscador.Utilidades;

namespace SistemaBuscador.Pages
{
    public class CambioPasswordModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }
        [Display(Name = "Contraseņa")]
        [BindProperty]
        [Required(ErrorMessage = "El campo Contraseņa es requerido")]
        [MinLength(8, ErrorMessage = "La contraseņa debe tener al menos 8 caracteres")]
        [RegularExpression("^(?=\\w*\\d)(?=\\w*[A-Z])(?=\\w*[a-z])\\S{8,16}$", ErrorMessage = "La contraseņa debe tener al menos una letra mayuscula,numeros y minusculas")]
        public string Password { get; set; }
        [Display(Name = "Repetir contraseņa")]
        [BindProperty]
        [Required(ErrorMessage = "El campo repetir contraseņa es requerido")]
        [MinLength(8, ErrorMessage = "La contraseņa debe tener al menos 8 caracteres")]
        [RegularExpression("^(?=\\w*\\d)(?=\\w*[A-Z])(?=\\w*[a-z])\\S{8,16}$", ErrorMessage = "La contraseņa debe tener al menos una letra mayuscula,numeros y minusculas")]
        public string RePassword { get; set; }
        public IActionResult OnGet(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("sessionId")))
            {
                return RedirectToPage("./Index");
            }

            this.Id = id;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var repo = new UsuariosRepository();
                string password = this.Password;
                string password2 = this.RePassword;

                if (password != password2)
                {
                    ModelState.AddModelError(string.Empty, "Las contraseņas no coinciden");
                    return Page();
                }
                //Actualizar la contraseņa
                repo.UpdatePassword(this.Id,Security.Encrypt(this.Password));

                return RedirectToPage("./Usuarios");
            }

            return Page();
        
        }
    }
}
