using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Acceso.Modelos
{
    public class Publicaciones
    {
        public int Id { get; set; }

        public int IdAmigos { get; set; }

        public int IdUsuario { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public string ImagenPublicacion { get; set; } = string.Empty;

        public string TituloPublicacion { get; set; } = string.Empty;
    }
}
