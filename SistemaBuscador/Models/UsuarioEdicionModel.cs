using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Models
{
    public class UsuarioEdicionModel
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NombreUsuario { get; set; }
        public int RolId { get; set; }
        public int PaisId { get; set; }
        public string Password { get; set; }
    }
}
