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
    public class UsuariosModel : PageModel
    {
        public bool Escritura { get; set; }
        public bool Lectura { get; set; }
        public List<UsuarioListaModel> Usuarios { get; set; }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("sessionId")))
            {
                return RedirectToPage("./Index");
            }
            var idUsuario = (int)HttpContext.Session.GetInt32("usuarioId");
            var repo = new UsuariosRepository();
            var permisos = repo.ObtenerPermisosPorId(idUsuario);
            this.Escritura = permisos.Escritura;
            this.Lectura = permisos.Lectura;
            this.Usuarios = repo.ObtenerUsuarios();

            return Page();
        }
    }
}
