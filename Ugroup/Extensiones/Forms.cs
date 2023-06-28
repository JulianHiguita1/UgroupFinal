namespace Ugroup.Extensiones;


/// <summary>
/// Extensiones para ContextPages
/// </summary>
public static class ContextPageExtensions
{


    /// <summary>
    /// Abre una nueva pagina
    /// </summary>
    public static async void Show(this ContentPage newPage)
    {
        try
        {
            // Obtiene la pagina actual
            var actualPage = Application.Current?.MainPage;

            if (actualPage == null) return;

           
            // Muestra la nueva pagina
            await actualPage.Navigation.PushAsync(newPage, true);
        }
        catch
        {
        }
    }




    /// <summary>
    /// Abre una nueva pagina
    /// </summary>
    public static async void Show(this FlyoutPage newPage)
    {
        try
        {
            // Obtiene la pagina actual
             Application.Current.MainPage = newPage;

        }
        catch
        {
        }
    }






    /// <summary>
    /// Cierra la ventana una nueva ventana
    /// </summary>
    public static void Close(this ContentPage context)
    {
        try
        {
            context?.Navigation.RemovePage(context);
        }
        catch { }
    }


}