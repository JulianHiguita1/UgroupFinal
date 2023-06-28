namespace Ugroup.Vistas;


public partial class Home : ContentPage
{
    private List<Api.Acceso.Modelos.Publicaciones> Publicaciones = new List<Publicaciones>(); 
    public Home()
    {
        InitializeComponent();
        GetData();
    }

    private void btnCrearPublicacion_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CrearPublicacion());
    }

    private void MostrarDatos()
    {
        foreach (Api.Acceso.Modelos.Publicaciones publicacion in Publicaciones)
        {
            var control = new Controles.Publicaciones(publicacion);
            LayoutPublis.Add(control);
        }
    }

    private async void GetData()
    {
        var response = await Api.Acceso.Publicaciones.Obtener(6);
        if (response == null)
        {
            return;
        }

        Publicaciones = response;
        MostrarDatos();
    }
}