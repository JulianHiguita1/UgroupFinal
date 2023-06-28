using Api.Acceso.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Api.Acceso
{
    public class Publicaciones
    {
        public static async Task<bool> Crear(Modelos.Publicaciones sender)
        {
            try
            {
                using (HttpClient httpClient = new())
                {
                    // Serializa los datos en formato JSON
                    var json = JsonConvert.SerializeObject(sender);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Realiza la solicitud POST para agregar el registro a la base de datos
                    HttpResponseMessage response = await httpClient.PostAsync("http://www.Ugroup.somee.com/Publicacion/Create", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Se publicó exitosamente a la base de datos

                        var result = await response.Content.ReadAsStringAsync();
                        return (result == "OK");
                    }
                    
                }
            }
            catch
            {

            }
            return false;
        }

        public static async Task<List<Modelos.Publicaciones>> Obtener(int id) {
            try
            {
                using (HttpClient httpClient = new())
                {

                    // Realiza la solicitud POST para agregar el registro a la base de datos
                    HttpResponseMessage response = await httpClient.GetAsync($"http://www.ugroup.somee.com/Publicacion/List?IdGrupos={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        // Se publicó exitosamente a la base de datos

                        var body = await response.Content.ReadAsStringAsync();
                        var publicaciones = JsonConvert.DeserializeObject<List<Modelos.Publicaciones>>(body);
                        return publicaciones;
                    }

                }
            }
            catch
            {

            }
            return new List<Modelos.Publicaciones> { };
        }
    }
}
