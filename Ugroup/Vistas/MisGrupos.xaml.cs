namespace Ugroup.Vistas
{
    public partial class MisGrupos : ContentPage
    {
        private List<CrearGrupo> Grupos = new List<CrearGrupo>(); // Lista de objetos CrearGrupo

        public MisGrupos()
        {
            InitializeComponent(); // Inicializa los componentes de la página
            GetData(); // Obtiene los datos de los grupos
        }

        private void btnCrearGrupo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CrearGrupos()); // Navega a la página CrearGrupos al hacer clic en el botón "CrearGrupo"
            this.Close(); // Cierra la página actual
        }

        private void MostrarDatos()
        {
            foreach (CrearGrupo grupo in Grupos) // Itera sobre la lista de grupos
            {
                var control = new Controles.ChatMG(grupo); // Crea una instancia del control ChatMG pasando el grupo como argumento
                Layout.Add(control); // Agrega el control al layout de la página
            }
        }

        private async void GetData()
        {
            var response = await Api.Acceso.Grupos.Obtener(Login.userid); // Obtiene los grupos de la API
            if (response == null) // Si la respuesta es nula, no se pudo obtener los grupos
            {
                return; // Sale del método
            }

            Grupos = response; // Asigna los grupos obtenidos a la lista Grupos
            MostrarDatos(); // Muestra los datos en la página
        }
    }
}