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
    public class EliminarUsuarioModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string NombreUsuario { get; set; }
        public IActionResult OnGet(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("sessionId")))
            {
                return RedirectToPage("./Index");
            }

            var repo = new UsuariosRepository();
            var usuario = repo.ObtenerUsuarioPorId(id);
            this.Id = usuario.Id;
            this.NombreUsuario = usuario.NombreUsuario;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (this.Id > 0)
            {
                var repo = new UsuariosRepository();
                repo.DeleteUsuario(this.Id);
                //Eliminar el registro de la bd
                return RedirectToPage("./Usuarios");
            }
            return Page();
        }

    }
}
