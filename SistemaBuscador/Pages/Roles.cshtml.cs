using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaBuscador.Models;
using SistemaBuscador.Repositories;

namespace SistemaBuscador.Pages
{
    public class RolesModel : PageModel
    {
        public List<RolListaModelo> Roles { get; set; }
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("sessionId")))
            {
                return RedirectToPage("./Index");
            }

            //Buscar en la bd los roles y cargarlo en la propiedad
            var repo = new RolRepositorio();
            this.Roles = repo.ObtenerRoles();

            return Page();
        }
    }
}
