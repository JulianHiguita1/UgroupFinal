using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ugroup.Vistas
{
    public partial class Unirsegrupo : ContentPage
    {
        private int IdGrupo { get; set; }


        public Unirsegrupo(int idGrupoSeleccionado)
        {
            IdGrupo = idGrupoSeleccionado;
            InitializeComponent();
        }

        private async void Si_Clicked(object sender, EventArgs e)
        {
            try
            {

                using (HttpClient httpClient = new ())
                {
                    // Crea un objeto para enviar los datos de unirse al grupo
                    var unirseGrupo = new Unirgrupo
                    {
                        IdUsuario = Vistas.Login.userid,
                        IdGrupos = IdGrupo
                    };

                    // Serializa los datos en formato JSON
                    var json = JsonConvert.SerializeObject(unirseGrupo);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Realiza la solicitud POST para unirse al grupo en la base de datos
                    HttpResponseMessage response = await httpClient.PostAsync("http://www.Ugroup.somee.com/userG/Create", content);

                    if (response.IsSuccessStatusCode)
                    {

                        await DisplayAlert("Éxito", "Te has unido al grupo correctamente", "Aceptar");
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo unir al grupo. Inténtalo nuevamente.", "Aceptar");
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes mostrar un mensaje de error o realizar otras acciones necesarias
                await DisplayAlert("Error", "Ocurrió un error al unirse al grupo. Detalles: " + ex.Message, "Aceptar");
            }
        }

        private void No_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Cancelado", "No te has unido al grupo", "Aceptar");
        }
    }
}