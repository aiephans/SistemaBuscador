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
    public class EditarPaisModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public string Nombre { get; set; }
        public IActionResult OnGet(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("sessionId")))
            {
                return RedirectToPage("./Index");
            }

            // buscar en la bd en registro con el id recibido y cargar los valores a las propiedades
            var repo = new PaisRepositorio();
            var rol = repo.ObtenerPaisPorId(id);
            this.Id = rol.Id;
            this.Nombre = rol.Nombre;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //Actualizar el registro en la bd
                var repo = new PaisRepositorio();
                repo.ActualizarPais(this.Id, this.Nombre);
                return RedirectToPage("./Paises");
            }

            return Page();
        }
    }
}
