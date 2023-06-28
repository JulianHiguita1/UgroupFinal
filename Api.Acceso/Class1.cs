using Microsoft.AspNetCore.SignalR.Client;

namespace Api.Acceso;


public class Hub
{

    /// <summary>
    /// Recibir mensaje
    /// </summary>
    public event EventHandler<string>? OnRecieveMessage;

    /// <summary>
    /// Recibir Location
    /// </summary>
    public event EventHandler<string>? OnRecieveLocation;

    /// <summary>
    /// Conexion del Hub
    /// </summary>
    private HubConnection? HubConnection { get; set; }





    public async Task<bool> Suscribe()
    {
        try
        {
            // Crea la conexion al HUB
            HubConnection = new HubConnectionBuilder()
                 .WithUrl("http://ugroup.somee.com/chat")
                 .WithAutomaticReconnect()
                 .Build();

            // Evento ccuando se reciba una tarea
            HubConnection.On<string>("canalMensajes", SendTaskEvent);


            HubConnection.On<string>("canalLocation", Ubication);

            // Inicia la conexion
            await HubConnection.StartAsync();
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error HUB Account: " + ex);
        }

        return false;
    }


    public async void JoinTo(string group)
    {

        if (HubConnection == null)
           return;
        

        // Suscribe al grupo
        await HubConnection?.InvokeAsync("JoinGroup", group);

    }


    public async void SendMessage(string message, string groupName)
    {

        if (HubConnection == null)
            return;


        // Suscribe al grupo
        await HubConnection?.InvokeAsync("SendMessage", message, groupName);

    }

    public async void SendLocation(string location, string groupName)
    {

        if (HubConnection == null)
            return;


        // Suscribe al grupo
        await HubConnection?.InvokeAsync("SendLocation", location, groupName);

    }



    private void SendTaskEvent(string message)
    {
        OnRecieveMessage?.Invoke(null, message);
    }

    private void Ubication(string message)
    {
        OnRecieveLocation?.Invoke(null, message);
    }


}