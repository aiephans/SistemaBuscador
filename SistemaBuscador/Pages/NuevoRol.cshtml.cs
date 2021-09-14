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
    public class NuevoRolModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage ="El campo Nombre es requerido")]
        public string Nombre { get; set; }
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("sessionId")))
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //Guardar en la BD
                var repo = new RolRepositorio();
                repo.InsertRol(this.Nombre);
                return RedirectToPage("./Roles");
            }

            return Page();
        }

    }
}
