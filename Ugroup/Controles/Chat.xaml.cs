namespace Ugroup.Controles;


public partial class Chat : ContentView
{
	public Chat(string message, bool itsMine)
	{
		InitializeComponent();
		MessageDesign.Text = message;

		MensajePropio.HorizontalOptions = (itsMine) ? LayoutOptions.End : LayoutOptions.Start;
		Caja.BackgroundColor = (!itsMine) ? Color.FromRgba("#ABB2B9") : Caja.BackgroundColor;
		//MessageDesign.TextColor = (!itsMine) ? Color.FromRgba("#000000"): MessageDesign.TextColor;

    }
}