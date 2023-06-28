using Api.Acceso;

namespace Ugroup.Vistas
{
    // Clase parcial ChatGrupos que extiende ContentPage
    public partial class ChatGrupos : ContentPage
    {
        private int IdGrupo { get; set; } // Propiedad para almacenar el ID del grupo

        Hub? Hub = null; // Objeto Hub para la comunicación en tiempo real

        // Constructor de la clase ChatGrupos que recibe el ID del grupo como parámetro
        public ChatGrupos(int idgrupo)
        {
            IdGrupo = idgrupo; // Asigna el ID del grupo a la propiedad IdGrupo

            InitializeComponent();

            StarHub(); // Llama al método StarHub para establecer la comunicación con el hub
        }

        // Método para inicializar la comunicación con el hub
        public async void StarHub()
        {
            Hub = new Hub(); // Crea una nueva instancia del objeto Hub
            var res = await Hub.Suscribe(); // Suscribe al hub

            if (!res)
            {
                await this.DisplayAlert("Error", "Hubo un error al conectar", "Ok");
                return;
            }

            Hub.JoinTo(IdGrupo.ToString()); // Se une al grupo correspondiente en el hub

            Hub.OnRecieveMessage += Hub_OnRecieveMessage; // Suscribe al evento OnRecieveMessage del hub
            Hub.OnRecieveLocation += Hub_OnRecieveLocation; // Suscribe al evento OnRecieveLocation del hub
        }

        // Evento de recepción de ubicación desde el hub
        private void Hub_OnRecieveLocation(object? sender, string e)
        {
            this.Dispatcher.DispatchAsync(() =>
            {
                recibirUbicacion(sender, e); // Método para recibir y procesar la ubicación recibida desde el hub
            });
        }

        // Evento de recepción de mensaje desde el hub
        private void Hub_OnRecieveMessage(object? sender, string e)
        {
            this.Dispatcher.DispatchAsync(() =>
            {
                RecibirMessage(e, false); // Método para recibir y procesar el mensaje recibido desde el hub
            });
        }

        // Método para procesar y mostrar un mensaje recibido desde el hub
        public void RecibirMessage(string message, bool notMine)
        {
            Controles.Chat MessageDesign = new Controles.Chat(message, notMine); // Crea una instancia del control Chat
            chatLayout.Add(MessageDesign); // Agrega el control al layout "chatLayout"
            scroll.ScrollToAsync(0, scroll.Content.Height, true); // Desplaza el scroll hacia la parte inferior del contenido
        }

        // Evento para probar la obtención de la ubicación actual
        public async void pruebaGPS(object sender, EventArgs e)
        {
            var ubicacion = await Servicios.gps.GetLocation(); // Obtiene la ubicación actual

            var mapa = new Controles.Map(ubicacion.Longitude, ubicacion.Latitude, true)
            {
                HeightRequest = 300,
                WidthRequest = 300
            };

            chatLayout.Add(mapa); // Agrega el control de mapa al layout "chatLayout"
            Hub.SendLocation($"{ubicacion.Longitude}|{ubicacion.Latitude}", IdGrupo.ToString()); // Envía la ubicación al hub
        }

        // Método para recibir y mostrar una ubicación recibida desde el hub
        public void recibirUbicacion(object receiver, string coordenadas)
        {
            double longitud = Double.Parse(coordenadas.Split('|')[0]); // Obtiene la longitud de las coordenadas
            double latitud = Double.Parse(coordenadas.Split('|')[1]); // Obtiene la latitud de las coordenadas

            var mapa = new Controles.Map(longitud, latitud, false)
            {
                HeightRequest = 300,
                WidthRequest = 300
            };

            chatLayout.Add(mapa); // Agrega el control de mapa al layout "chatLayout"
        }

        // Evento para enviar un mensaje al hub
        public void enviarMensaje(object sender, EventArgs e)
        {
            if (Hub == null || mensajeEntry.Text == null || mensajeEntry.Text.Length <= 0)
                return;

            // Envia el mensaje al hub
            Hub.SendMessage(mensajeEntry.Text, IdGrupo.ToString());
            RecibirMessage(mensajeEntry.Text, true); // Muestra el mensaje enviado en la interfaz
            mensajeEntry.Text = string.Empty; // Limpia el campo de entrada de texto
        }
    }
}