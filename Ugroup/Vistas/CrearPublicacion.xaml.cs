using Ugroup.Platforms;

namespace Ugroup.Vistas;

public partial class CrearPublicacion : ContentPage
{
    public CrearPublicacion()
    {
        InitializeComponent();
    }

    private async void Foto_Clicked(object sender, EventArgs e)
    {

        var result = await MediaPicker.PickPhotoAsync();
        if (result != null)
        {
            imagenPubli.Source = ImageSource.FromStream(() => result.OpenReadAsync().Result);
        }
    }

    private async void btnPublicar_Clicked(object sender, EventArgs e)
    {

        string descripcion = descripcionPubli.Text;
        string titulo = tituloPubli.Text;
        var imagen = await imagenPubli.ToBase64();

        var datos = new Api.Acceso.Modelos.Publicaciones { Descripcion = descripcion, TituloPublicacion = titulo, ImagenPublicacion = imagen };
        var response = await Api.Acceso.Publicaciones.Crear(datos);

        if (response)
        {
            await DisplayAlert("Éxito", "Se ha publicado correctamente", "Aceptar");
            await Navigation.PushAsync(new Home());
            this.Close();
        }
        else
        {
            await DisplayAlert("Error", $"No se ha podido publicar correctamente.", "Aceptar");
        }

    }


}