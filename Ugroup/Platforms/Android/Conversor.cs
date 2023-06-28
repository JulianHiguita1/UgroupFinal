using Android.Graphics;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ugroup.Platforms
{
    class Conversor
    {
        /// <summary>
        /// Convierte a base64
        /// </summary>
        public static async Task<string> ToBase64(Microsoft.Maui.Controls.Image image)
        {

            // Control convertido en Android 
            ImageView control = (ImageView)(image?.Handler?.PlatformView ?? new());

            // Convierte la imagen
            Bitmap? bitmap = (control.Drawable as Android.Graphics.Drawables.BitmapDrawable)?.Bitmap;

            // Si es Null
            if (bitmap == null)
                return "";


            // Compresion
            MemoryStream stream = new();
            await bitmap.CompressAsync(Bitmap.CompressFormat.Png, 30, stream);
            byte[] result = stream.ToArray();

            // Retorna
            return Convert.ToBase64String(result) ?? "";
        }
    }
}
