using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Acceso.Modelos
{
    public class Usuario
    {
        public string NombreUsuario { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime FNacimiento { get; set; }
    }
    public class LoginModel
    {
        public string Email { get; set; }
        public string Contrasena { get; set; }
    }

    public class VerifyCorreo
    {
        public string Email { get; set; }
    }
}
