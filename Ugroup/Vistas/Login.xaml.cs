using Newtonsoft.Json;
using System.Diagnostics;


namespace Ugroup.Vistas
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        public static int userid { get; set; }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {

            string email = CorreoLogin.Text ?? "";
            string password = ContrasenaLogin.Text ?? "";

            int loginSuccessful = await VerifyLogin(email, password);

            if (loginSuccessful > 0)
            {
                userid = loginSuccessful;
                new FlyoutPageSample.AppFlyout().Show();
            }

            else
            {
                await DisplayAlert("Error", "Credenciales inválidas. Verifica tu correo electrónico y contraseña.", "Aceptar");
            }
        }


        private async Task<int> VerifyLogin(string email, string Contrasena)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string apiUrl = "http://www.Ugroup.somee.com/LogIn/Confirmacion";

                    var credentials = new LoginModel
                    {
                        Email = email,
                        Contrasena = Contrasena
                    };

                    var jsonCredentials = JsonConvert.SerializeObject(credentials);
                    var httpContent = new StringContent(jsonCredentials, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, httpContent);

                    string responseContent = await response.Content.ReadAsStringAsync();

                    int obj = JsonConvert.DeserializeObject<int>(responseContent);

                    return obj;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al verificar el inicio de sesión: {ex.Message}");
            }

            return 0;
        }

        private void btnRegistro_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registro());
        }
        private void btnOlvidarContrasena_Clicked(object sender, EventArgs e)
        {
        }

    }
}