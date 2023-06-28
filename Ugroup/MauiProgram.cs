global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using Api.Acceso.Modelos;
global using Ugroup.Extensiones;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Platform;

namespace Ugroup;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Poppins-Black.ttf", "PBLACK");
                fonts.AddFont("Poppins-Bold.ttf", "PB");
                fonts.AddFont("Poppins-Light.ttf", "PL");
                fonts.AddFont("Poppins-Regular.ttf", "PR");
            });
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
        {
            if (view is Entry)
            {
		#if ANDROID

                handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());

		#endif
            }
        });

        return builder.Build();
	}
}
