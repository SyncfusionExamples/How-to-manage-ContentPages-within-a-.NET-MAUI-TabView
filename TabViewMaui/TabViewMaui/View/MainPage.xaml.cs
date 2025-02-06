using Syncfusion.Maui.TabView;

namespace TabViewMaui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            UpdateToolbar();
        }

        private void OnSelectionChanged(object sender, TabSelectionChangedEventArgs e)
        {
            UpdateToolbar();
        }

        private void UpdateToolbar()
        {
            // Clear existing toolbar items
            ToolbarItems.Clear();

            if (tabView is not null && tabView?.Items is not null && tabView.SelectedIndex >= 0 &&
                tabView.Items[(int)tabView.SelectedIndex] is CustomTabItem currentTab &&
                currentTab.Page is ContentPage page)
            {
                // Update title
                Title = page.Title;

                // Add toolbar items if they exist
                if (page.ToolbarItems?.Any() == true)
                {
                    foreach (var item in page.ToolbarItems)
                    {
                        // Detach any existing event handlers to prevent duplicate bindings
                        item.Clicked -= OnItemClicked;

                        // Attach the MainPage event handler
                        item.Clicked += OnItemClicked;

                        // Add the toolbar item to MainPage
                        ToolbarItems.Add(item);
                    }
                }
            }
        }

        private void OnItemClicked(object? sender, EventArgs e)
        {
            if (sender is ToolbarItem toolbarItem)
            {
                // Handle the click event for the toolbar item
                DisplayAlert("Toolbar Clicked", $"Toolbar item '{toolbarItem.Text}' clicked in MainPage", "OK");

                // You can add additional navigation or logic here
                if (toolbarItem.Text == "Help")
                {
                    Navigation.PushAsync(new HelpPage());
                }
            }
        }
    }

}
