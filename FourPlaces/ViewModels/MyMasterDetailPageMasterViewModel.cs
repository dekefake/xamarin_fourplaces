using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FourPlaces.Views;
using FourPlaces.Models;
using System.Linq;

namespace FourPlaces.ViewModels
{
    public class MyMasterDetailPageMasterViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MyMasterDetailPageMenuItem> MenuItems { get; set; }

        public MyMasterDetailPageMasterViewModel()
        {
            MenuItems = new ObservableCollection<MyMasterDetailPageMenuItem>(new[]
            {
                    new MyMasterDetailPageMenuItem { Id = 0, Title = "Places", TargetType=typeof(MainPage) },
                    new MyMasterDetailPageMenuItem { Id = 1, Title = "Mon compte", TargetType=typeof(RegisterPage) },
            });
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
