namespace TabViewMaui;

public partial class TabPage3 : ContentPage
{
	public TabPage3()
	{
		InitializeComponent();
	}


    private void OnTapped(object sender, TappedEventArgs e)
    {
        var navigation = CustomTabItem.GetNavigation();
        if (navigation != null)
        {
            navigation.PushAsync(new SettingsPage());
        }
    }
}