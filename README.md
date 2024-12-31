When using the [.NET MAUI TabView](https://www.syncfusion.com/maui-controls/maui-tab-view) to display `ContentPage` content within each tab, you may want to preserve the navigation context and manage toolbar items dynamically. This article provides a solution to enable smooth navigation between pages and proper toolbar item visibility when switching between tabs.

To enable navigation from ContentPage inside a .NET MAUI TabView, implement a custom solution that retains the navigation context.

```
public class CustomTabItem : SfTabItem
{
    public static readonly BindableProperty PageProperty =
        BindableProperty.Create(nameof(Page), typeof(ContentPage), typeof(CustomTabItem), null, propertyChanged: OnPagePropertyChanged);

    private static INavigation? _pageNavigation;

    public ContentPage Page
    {
        get => (ContentPage)GetValue(PageProperty);
        set => SetValue(PageProperty, value);
    }

    public CustomTabItem()
    {
    }

    private static void OnPagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var tabItem = bindable as CustomTabItem;
        if (tabItem != null)
        {
            var contentPage = newValue as ContentPage;
            tabItem.Content = contentPage?.Content;

            // Store the navigation context
            _pageNavigation = Application.Current?.MainPage?.Navigation;
            if (tabItem.Content != null && contentPage?.BindingContext != null)
            {
                tabItem.Content.BindingContext = contentPage.BindingContext;
            }
        }
    }

    public static INavigation? GetNavigation()
    {
        return _pageNavigation;
    }
}
```

In the above code, a static field `_pageNavigation` stores the navigation context from the MainPage. The `GetNavigation` method provides access to this context, enabling navigation actions like `PushAsync` to function correctly.

**XAML**

Create different content pages that you need to display as a Tab Item. For example like below,

```
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="TabViewMaui.TabPage1"
             Title="TabPage1">
    <ContentPage.ToolbarItems>
        <ToolbarItem Text="Click"/>
    </ContentPage.ToolbarItems>
    
     <ContentPage.Content>
         <!-- Implement your content here -->
     </ContentPage.Content>
</ContentPage>
```

Initialize the .NET MAUI TabView control:

```
<tabView:SfTabView x:Name="tabView" SelectionChanged="OnSelectionChanged" >

    <tabView:SfTabView.Items>
        <local:CustomTabItem x:Name="call" Header="Call" >
            <local:CustomTabItem.Page>
                <local:TabPage1/>
            </local:CustomTabItem.Page>
        </local:CustomTabItem>

        <!-- Add more tab items as needed -->

    </tabView:SfTabView.Items>
</tabView:SfTabView>
```

**C#**

```
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
            
            if (toolbarItem.Text == "Click")
            {
                // You can add additional navigation or logic here
            }
        }
    }
}
```

**Toolbar Management**

`OnSelectionChanged:` This method triggers the UpdateToolbar method when the selected tab changes, ensuring that the toolbar items are updated based on the ContentPage of the selected tab.

`UpdateToolbar:` It clears any existing toolbar items in the MainPage, retrieves the toolbar items from the selected tab’s ContentPage, and updates the MainPage toolbar accordingly. Event handlers are detached from previous items to prevent duplicate events, and new items are added.

`OnItemClicked:` Handles toolbar item click events, allowing for additional actions such as navigation or other logic when a toolbar item is clicked.

**Output**

![TabViewMaui.gif](https://support.syncfusion.com/kb/agent/attachment/article/18735/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjM0NTEwIiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5zeW5jZnVzaW9uLmNvbSJ9.nvB6lrnqBhL61d2fUDq_-38Lb2xAiOivrJjePat8Ekg)

**Requirements to run the demo**
 
To run the demo, refer to [System Requirements for .NET MAUI](https://help.syncfusion.com/maui/system-requirements)
 
**Troubleshooting:**

**Path too long exception** 

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.