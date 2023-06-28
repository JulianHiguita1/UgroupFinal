using Api.Acceso.Modelos;
using Newtonsoft.Json;


namespace Api.Acceso;

public class Grupos
{
    public int Id { get; set; }
    public static async Task<bool> Crear(CrearGrupo grupo)
    {
        try
        {
            // Envio a la URL
            HttpClient httpClient = new();

            string apiUrl = "http://www.Ugroup.somee.com/Grupo/Create";

            // Convertir el objeto Grupo a JSON
            string jsonGrupo = JsonConvert.SerializeObject(grupo);

            // Crear el contenido de la solicitud HTTP
            var content = new StringContent(jsonGrupo, System.Text.Encoding.UTF8, "application/json");

            // Enviar la solicitud POST a la URL 
            HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

            return response.IsSuccessStatusCode;
        }

        catch { }
        return false;

    }

    public static async Task<List<Modelos.CrearGrupo>?> Obtener(int id)
    {
        try
        {
            // Envio a la URL
            HttpClient httpClient = new();

            string apiUrl = $"http://www.Ugroup.somee.com/Grupo/List?id={id}";      

            // Enviar la solicitud POST a la URL 
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            var data = await response.Content.ReadAsStringAsync();
            var grupos = JsonConvert.DeserializeObject<List<CrearGrupo>>(data);

            return grupos;
        }

        catch { }
        return null;

    }

    public static async Task<List<Modelos.CrearGrupo>?> MostrarOtrosGrupos(int id)
    {
        try
        {
            // Envio a la URL
            HttpClient httpClient = new();

            string apiUrl = $"http://www.Ugroup.somee.com/Grupo/ListTodo?id={id}";

            // Enviar la solicitud POST a la URL 
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            var data = await response.Content.ReadAsStringAsync();
            var grupos = JsonConvert.DeserializeObject<List<CrearGrupo>>(data);

            return grupos;
        }

        catch { }
        return null;

    }
}