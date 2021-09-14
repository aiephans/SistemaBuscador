using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaBuscador.Repositories;

namespace SistemaBuscador.Pages
{
    public class EliminarRolModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string Nombre { get; set; }
        public IActionResult OnGet(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("sessionId")))
            {
                return RedirectToPage("./Index");
            }

            var repo = new RolRepositorio();
            var rol = repo.ObtenerRolPorId(id);
            this.Id = rol.Id;
            this.Nombre = rol.Nombre;

            return Page();
        }

        public IActionResult OnPost()
        {
            //Elimiar de la bd
            var repo = new RolRepositorio();
            repo.EliminarRol(this.Id);
            return RedirectToPage("./Roles");
        }


    }
}
