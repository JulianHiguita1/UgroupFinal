namespace Ugroup.Vistas;

public partial class pagGrupos : ContentPage
{

    private List<CrearGrupo>Grupos = new List<CrearGrupo>();
	public pagGrupos()
	{
		InitializeComponent();
        GetData();
	}

    private void MostrarDatos() 
    {
        foreach (CrearGrupo Grupo in Grupos) {
            var control = new Controles.Grupos(Grupo);
            LayoutGrupos.Add(control);
        }
    }

    private async void GetData()
    {
        var response = await Api.Acceso.Grupos.MostrarOtrosGrupos(Login.userid);
        if (response == null) {
            return;
        }

        Grupos = response;
        MostrarDatos();
    }
}