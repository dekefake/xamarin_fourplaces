using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FourPlaces.Models;
using FourPlaces.Views;
using Storm.Mvvm;

namespace FourPlaces.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public List<Place> Places { get; set; }

        public MainPageViewModel()
        {
            Places = MainPageModel.GetPlaces();
        }
    }
}
