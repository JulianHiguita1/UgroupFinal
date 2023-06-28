namespace Ugroup;
using Ugroup.Vistas;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		var page = new Login();
        NavigationPage.SetHasNavigationBar(page, false);
        MainPage = new NavigationPage(page);
	}
}
