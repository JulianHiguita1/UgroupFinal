using Ugroup.Vistas;

namespace Ugroup.Controles
{
    // Clase parcial ChatMG que extiende ContentView
    public partial class ChatMG : ContentView
    {
        private int idgrupo; // Variable para almacenar el ID del grupo

        CrearGrupo Grupo { get; set; } // Propiedad para almacenar un objeto CrearGrupo

        // Constructor de la clase ChatMG que recibe un objeto CrearGrupo como parámetro
        public ChatMG(CrearGrupo modelo)
        {
            InitializeComponent();

            idgrupo = modelo.ID; // Asigna el ID del grupo a la variable idgrupo
            Grupo = modelo; // Asigna el objeto CrearGrupo al atributo Grupo
            RenderData(); // Llama al método RenderData para renderizar los datos en la interfaz
        }

        // Método para renderizar los datos del grupo en la interfaz
        private void RenderData()
        {
            nomGrupo.Text = Grupo.NOMBRE_GRUPO; // Asigna el nombre del grupo al elemento "nomGrupo"
            descGrupo.Text = Grupo.DESCRIPCION; // Asigna la descripción del grupo al elemento "descGrupo"
        }

        // Evento de tap en el elemento Label
        private void Label_Tap(object sender, EventArgs e)
        {
            // Navega a la página ChatGrupos y pasa el ID del grupo como parámetro al constructor
            Navigation.PushAsync(new ChatGrupos(idgrupo));
        }
    }
}