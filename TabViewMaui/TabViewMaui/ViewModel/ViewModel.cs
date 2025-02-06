using Syncfusion.Maui.TabView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabViewMaui
{
    public class ViewModel : INotifyPropertyChanged
    {
        
        private ObservableCollection<string> collections = new();

        public ObservableCollection<string> Colllections
        {
            get { return collections; }
            set { collections = value; OnPropertyChanged(nameof(Colllections)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ViewModel()
        {
            this.LoadData();
        }

        private async void LoadData()
        {
            Colllections.Add("James");
            Colllections.Add("Richard");
            Colllections.Add("Michael");
            Colllections.Add("Alex");
            Colllections.Add("Clara");
        }
    }

}
