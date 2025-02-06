using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabViewMaui
{
    public class Model : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private string? name;

        public string? Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        private List<string>? list;

        public List<string>? ItemList
        {
            get { return list; }
            set
            {
                list = value;
                OnPropertyChanged("ItemList");
            }
        }
         private ContentPage? tabItem;

        public ContentPage? TabItem
        {
            get { return tabItem; }
            set
            {
                tabItem = value;
                OnPropertyChanged("TabItem");
            }
        }
        private string? icon;

        public string? FontIcon
        {
            get { return icon; }
            set
            {
                icon = value;
                OnPropertyChanged("FontIcon");
            }
        }
    }
}
