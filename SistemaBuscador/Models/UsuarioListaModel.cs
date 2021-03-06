using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Models
{
    public class UsuarioListaModel
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NombreUsuario { get; set; }
        public int RolId { get; set; }
        public string Rol { get; set; }
        public int PaisId { get; set; }
        public string Pais { get; set; }
        public bool Lectura { get; set; }
        public bool Escritura { get; set; }

    }
}
