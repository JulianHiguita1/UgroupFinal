using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ugroup.Platforms
{
    static class Abstraccion
    {
        /// <summary>
        /// Retroa una imagen
        /// </summary>
        public static ImageSource Decode(string inputString)
        {
            byte[] NewBytes = Convert.FromBase64String(inputString);

            MemoryStream ms1 = new(NewBytes);
            ImageSource NewImage = ImageSource.FromStream(() => ms1);

            return NewImage;
        }



        /// <summary>
        /// Convierte una imagen a Base64
        /// </summary>
        /// <param name="image">Imagen a convertir</param>
        public static async Task<string> ToBase64(this Image image)
        {
            return await Conversor.ToBase64(image);

        }
    }
}
