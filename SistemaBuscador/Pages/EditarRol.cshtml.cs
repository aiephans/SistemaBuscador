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
    public class EditarRolModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        [Required(ErrorMessage ="El campo nombre es requerido")]
        public string Nombre { get; set; }
        [BindProperty]
        public bool Escritura { get; set; }
        [BindProperty]
        public bool Lectura { get; set; }
        public IActionResult OnGet(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("sessionId")))
            {
                return RedirectToPage("./Index");
            }

            // buscar en la bd en registro con el id recibido y cargar los valores a las propiedades
            var repo = new RolRepositorio();
            var rol = repo.ObtenerRolPorId(id);
            this.Id = rol.Id;
            this.Nombre = rol.Nombre;
            this.Lectura = rol.Lectura;
            this.Escritura = rol.Escritura;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //Actualizar el registro en la bd
                var repo = new RolRepositorio();
                repo.ActualizarRol(this.Id, this.Nombre, this.Lectura, this.Escritura);
                return RedirectToPage("./Roles");
            }

            return Page();
        }
    }
}
