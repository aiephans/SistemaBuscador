using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SistemaBuscador.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Pages
{
    public class IndexModel : PageModel
    {
        [Required(ErrorMessage ="El campo Usuario es requerido")]
        [BindProperty]
        public string Usuario { get; set; }
        [Display(Name="Contraseña")]
        [Required(ErrorMessage ="El campo contraseña es requerido")]
        [BindProperty]
        public string Password { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
        }

        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var usuario = this.Usuario;
            var pass = this.Password;

            var repo = new LoginRepository();

            if (repo.UserExist(usuario, pass))
            {
                Guid sessionId = Guid.NewGuid();
                HttpContext.Session.SetString("sessionId", sessionId.ToString());
                Response.Cookies.Append("sessionId", sessionId.ToString());
                _logger.LogInformation("Usuario logeado correctamente: "+ this.Usuario);
                return RedirectToPage("./Home");
            }
            else
            {
                _logger.LogWarning("fallo al iniciar session usuario:"+this.Usuario);
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos");
                return Page();
            }

            //Validar si existe el usuario en la bd
        }
    }
}
