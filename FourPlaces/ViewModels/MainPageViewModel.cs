using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using FourPlaces.Models;
using FourPlaces.Utils;
using FourPlaces.Views;
using Storm.Mvvm;
using Xamarin.Forms;

namespace FourPlaces.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            if (Constantes.Places == null)
            {
                Constantes.Places = MainPageModel.GetPlaces();
            }

        }

        public bool IsRefreshing { get; set; } = false;
    }
}
