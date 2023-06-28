using Api.Acceso;

namespace Ugroup.Vistas
{
    public partial class Chat : ContentPage
    {

        Hub? Hub = null;

        public Chat()
        {
            InitializeComponent();

            StarHub();
        }

        public async void StarHub()
        {
            Hub = new();
            var res = await Hub.Suscribe();

            if (!res)
            {
                await this.DisplayAlert("Error", "Hubo un error al conectar", "Ok");
                return;
            }

            Hub.JoinTo("A1");

            Hub.OnRecieveMessage += Hub_OnRecieveMessage;
            Hub.OnRecieveLocation += Hub_OnRecieveLocation;

        }

        private void Hub_OnRecieveLocation(object? sender, string e)
        {
            this.Dispatcher.DispatchAsync(() =>
            {
                recibirUbicacion(sender, e);
            });
        }

        private void Hub_OnRecieveMessage(object? sender, string e)
        {
            this.Dispatcher.DispatchAsync(() =>
            {
                RecibirMessage(e, false);
            });
        }

        public void RecibirMessage(string message, bool notMine)
        {
            Controles.Chat MessageDesign = new(message, notMine);
            chatLayout.Add(MessageDesign);
            scroll.ScrollToAsync(0, scroll.Content.Height, true);
        }

        public async void pruebaGPS(object sender, EventArgs e)
        {

            var ubicacion = await Servicios.gps.GetLocation();


            var mapa = new Controles.Map(ubicacion.Longitude, ubicacion.Latitude, true)
            {
                HeightRequest = 300,
                WidthRequest = 300
            };

            chatLayout.Add(mapa);
            Hub.SendLocation($"{ubicacion.Longitude}|{ubicacion.Latitude}", "A1");
        }

        public void recibirUbicacion(object receiver, string coordenadas)
        {
            double longitud = Double.Parse(coordenadas.Split('|')[0]);
            double latitud = Double.Parse(coordenadas.Split('|')[1]);

            var mapa = new Controles.Map(longitud, latitud, false)
            {
                HeightRequest = 300,
                WidthRequest = 300
            };

            chatLayout.Add(mapa);
        }


        public void enviarMensaje(object sender, EventArgs e)
        {

            if (Hub == null || mensajeEntry.Text == null || mensajeEntry.Text.Length <= 0)
                return;

            // Envia el mensaje
            Hub.SendMessage(mensajeEntry.Text, "A1");
            RecibirMessage(mensajeEntry.Text, true);
            mensajeEntry.Text = string.Empty;

        }
    }
}