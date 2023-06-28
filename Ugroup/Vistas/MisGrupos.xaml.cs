namespace Ugroup.Vistas
{
    public partial class MisGrupos : ContentPage
    {
        private List<CrearGrupo> Grupos = new List<CrearGrupo>(); // Lista de objetos CrearGrupo

        public MisGrupos()
        {
            InitializeComponent(); // Inicializa los componentes de la p�gina
            GetData(); // Obtiene los datos de los grupos
        }

        private void btnCrearGrupo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CrearGrupos()); // Navega a la p�gina CrearGrupos al hacer clic en el bot�n "CrearGrupo"
            this.Close(); // Cierra la p�gina actual
        }

        private void MostrarDatos()
        {
            foreach (CrearGrupo grupo in Grupos) // Itera sobre la lista de grupos
            {
                var control = new Controles.ChatMG(grupo); // Crea una instancia del control ChatMG pasando el grupo como argumento
                Layout.Add(control); // Agrega el control al layout de la p�gina
            }
        }

        private async void GetData()
        {
            var response = await Api.Acceso.Grupos.Obtener(Login.userid); // Obtiene los grupos de la API
            if (response == null) // Si la respuesta es nula, no se pudo obtener los grupos
            {
                return; // Sale del m�todo
            }

            Grupos = response; // Asigna los grupos obtenidos a la lista Grupos
            MostrarDatos(); // Muestra los datos en la p�gina
        }
    }
}