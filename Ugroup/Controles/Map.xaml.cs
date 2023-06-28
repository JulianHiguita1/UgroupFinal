namespace Ugroup.Controles;

public partial class Map : ContentView
{

    private static readonly string _key = "pk.YwGku7JIJ6PfrNze87PQcQQcSrPtko0SLUUr";

    private static string UrlBase
    {
        get
        {
            return @"http://linmaps.somee.com/mapa/light/";
        }

    }

    public Map(double lon, double lat, bool itsMine)
    {
        InitializeComponent();
        var url = @$"{UrlBase}{lon.ToString().Replace(',', '.')}/{lat.ToString().Replace(',', '.')}/{_key}";
        vista.Source = url;

        Mapa.HorizontalOptions = (itsMine) ? LayoutOptions.End : LayoutOptions.Start;
        CajaMapa.BackgroundColor = (!itsMine) ? Color.FromRgba("#ABB2B9") : CajaMapa.BackgroundColor;
    }

    public void SetCords(double lon, double lat)
    {
        var url = @$"{UrlBase}{lon.ToString().Replace(',', '.')}/{lat.ToString().Replace(',', '.')}/{_key}";
        vista.Source = url;
    }




    public void NavigateToHome()
    {
        vista.EvaluateJavaScriptAsync("window.blazorInterop.goToHome()");
    }


    public void CenterMap()
    {
        vista.EvaluateJavaScriptAsync("window.blazorInterop.resetNort()");
    }



    bool l = false;
    private void vista_Loaded(object sender, EventArgs e)
    {

        if (!l)
        {
            vista.HeightRequest += vista.Height + 90;
            l = true;
        }

    }
}