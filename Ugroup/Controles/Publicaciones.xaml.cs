using Ugroup.Platforms;

namespace Ugroup.Controles;

public partial class Publicaciones : ContentView
{
	public Api.Acceso.Modelos.Publicaciones Modelo { get; set; }

	public Publicaciones(Api.Acceso.Modelos.Publicaciones modelo)
	{
		InitializeComponent();
		this.Modelo = modelo;
		Renderizar();
	}

	public void Renderizar() {
		TituloPubli.Text = Modelo.TituloPublicacion;
		DescPubli.Text = Modelo.Descripcion;
		try { ImgPubli.Source = Abstraccion.Decode(Modelo.ImagenPublicacion); }
		catch { 
			
		}
	}
}