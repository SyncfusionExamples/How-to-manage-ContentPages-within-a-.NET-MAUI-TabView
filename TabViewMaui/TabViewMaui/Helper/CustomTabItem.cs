using Syncfusion.Maui.TabView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabViewMaui
{
    public class CustomTabItem : SfTabItem
    {
        public static readonly BindableProperty PageProperty =
     BindableProperty.Create(nameof(Page), typeof(ContentPage), typeof(CustomTabItem), null, propertyChanged: OnPagePropertyChanged);

        public CustomTabItem()
        {
        }

        public ContentPage Page
        {
            get => (ContentPage)GetValue(PageProperty);
            set => SetValue(PageProperty, value);
        }

        private static INavigation? _pageNavigation;

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

}
