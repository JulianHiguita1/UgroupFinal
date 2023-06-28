using Ugroup.Vistas;

namespace Ugroup.Controles
{
    public partial class Grupos : ContentView
    {
        private int idgrupo;

        CrearGrupo Grupo { get; set; }

        public Grupos(CrearGrupo modelo)
        {
            InitializeComponent();
            idgrupo = modelo.ID;
            Grupo = modelo;
            RenderData();
        }
        
        private void RenderData()
        {
            nomGrupo.Text = Grupo.NOMBRE_GRUPO;
            descGrupo.Text = Grupo.DESCRIPCION;
        }

        private void Label_Tapped(object sender, EventArgs e)
        {
            // Navega a la p�gina Unirsegrupo y pasa el ID del grupo como par�metro
            Navigation.PushAsync(new Unirsegrupo(idgrupo));
        }
    }
}