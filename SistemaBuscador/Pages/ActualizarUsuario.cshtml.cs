using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SistemaBuscador.Repositories;

namespace SistemaBuscador.Pages
{
    public class ActualizarUsuarioModel : PageModel
    {
        public bool Escritura { get; set; }
        public bool Lectura { get; set; }

        private readonly ILogger<IndexModel> _logger;

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
        [Display(Name = "País")]
        [BindProperty]
        [Required(ErrorMessage = "Debe seleccionar una opción")]
        public int? PaisId { get; set; }
        public SelectList Roles { get; set; }
        public SelectList Paises { get; set; }

        public ActualizarUsuarioModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
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
            var idUsuario = (int)HttpContext.Session.GetInt32("usuarioId");
            var repo = new UsuariosRepository();
            var permisos = repo.ObtenerPermisosPorId(idUsuario);
            this.Escritura = permisos.Escritura;
            this.Lectura = permisos.Lectura;

            
            var usuario = repo.ObtenerUsuarioPorId(id);
            this.Id = usuario.Id;
            this.NombreUsuario = usuario.NombreUsuario;
            this.Nombres = usuario.Nombres;
            this.Apellidos = usuario.Apellidos;
            this.RolId = usuario.RolId;
            this.PaisId = usuario.PaisId;
            var rolRepo = new RolRepositorio();
            var list = rolRepo.ObtenerRoles();
            Roles = new SelectList(list, "Id", "Nombre");
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
                repo.UpdateUsuario(this.Id, this.Nombres, this.Apellidos, (int)this.RolId,(int)this.PaisId);
                _logger.LogInformation("Usuario modificado : " + this.NombreUsuario);
                return RedirectToPage("./Usuarios");
            }
            return Page();
        }

    }
}
