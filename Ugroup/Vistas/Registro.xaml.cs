using Newtonsoft.Json;
using System.Text.RegularExpressions;



namespace Ugroup.Vistas
{

    public partial class Registro : ContentPage
    {
        public Registro()
        {
            InitializeComponent();
        }
        
        private async void btnRegistrarse_Clicked(object sender, EventArgs e)
        {
            // Captura los datos del front
            string nombre = nomRegistro.Text;
            string apellido = apellidoRegistro.Text;
            string contrasena = contraseña.Text;
            string rcontrasena = Rcontraseña.Text;
            string Correo = email.Text;
            DateTime fechaNacimiento = Fnacimiento.Date;

            //Condicional para verificar que los datos sean validos en el registro

            try
            {
                bool registroValido =  ValidarRegistro(nombre, apellido, Correo, contrasena, rcontrasena);
                
                if (!registroValido)
                {
                    return;
                }

                bool edadValida = ValidarEdadMinima(fechaNacimiento);

                if (!edadValida)
                {
                    DisplayAlert("Error", "Debes tener al menos 7 años de edad", "Aceptar");
                    return;
                }

            }
            catch (Exception ex)
            {
                // Manejar cualquier error de solicitud o respuesta aquí
                await DisplayAlert("Error", "Debes ingresar un correo", "Aceptar");
                return;
            }

            // Crea un objeto para representar los datos a enviar
            var datos = new { NombreUsuario = nombre, Apellido = apellido, Contrasena = contrasena, Email = Correo, Fnacimiento = fechaNacimiento.ToString("yyyy-MM-dd") };

            try
            {
                using (HttpClient httpClient = new())
                {
                    // Serializa los datos en formato JSON
                    var json = JsonConvert.SerializeObject(datos);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Realiza la solicitud POST para agregar el registro a la base de datos
                    HttpResponseMessage response = await httpClient.PostAsync("http://www.Ugroup.somee.com/User/Create", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // El registro se agregó exitosamente a la base de datos
                        
                        var result = await response.Content.ReadAsStringAsync();
                        if (result == "OK")
                        {
                            await DisplayAlert("Éxito", "El registro se agregó correctamente a la base de datos.", "Aceptar");
                            new Login().Show();
                        }
                        else if (result == "Existente") {
                            await DisplayAlert("Error", "El correo ingresado ya existe!", "Aceptar");
                        }
                    }
                    else
                    {
                        // Hubo un error al agregar el registro a la base de datos
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var errorMessage = responseContent ?? "Error desconocido";
                        await DisplayAlert("Error", $"Hubo un error al agregar el registro a la base de datos.\n\n{errorMessage}", "Aceptar");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de solicitud o respuesta aquí
                await DisplayAlert("Error", "Ocurrió un error al comunicarse con la API.", "Aceptar");
            }
        }
        private bool IsValidEmail(string email)
        {
            // Expresión regular para validar el formato del correo electrónico
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            return Regex.IsMatch(email, pattern);
        }

        private bool ValidarRegistro(string nombre, string apellido, string Correo, string contrasena, string rcontrasena)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                 DisplayAlert("Error", "Debes ingresar un nombre", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(apellido))
            {
                 DisplayAlert("Error", "Debes ingresar un apellido", "Aceptar");
                return false;
            }

            if (!IsValidEmail(Correo))
            {
                 DisplayAlert("Error", "Debes ingresar un correo electrónico válido", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(contrasena))
            {
                 DisplayAlert("Error", "Debes ingresar una contraseña", "Aceptar");
                return false;
            }

            if (contrasena != rcontrasena)
            {
                 DisplayAlert("Error", "Las contraseñas no coinciden", "Aceptar");
                return false;
            }

            return true;
        }

        private bool ValidarEdadMinima(DateTime fechaNacimiento)
        {
            DateTime fechaActual = DateTime.Now;
            TimeSpan edad = fechaActual - fechaNacimiento;

            int edadMinima = 7;

            if (edad.TotalDays < (365 * edadMinima))
            {
                return false;
            }

            return true;
        }

        private void btnVolverlogin_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

    }
}