using System;
using FourPlaces.Models;
using Storm.Mvvm;

namespace FourPlaces.ViewModels
{
    public class PlaceDetailsViewModel : ViewModelBase
    {
        public Place BindedPlace { get; set; } = null;
    }
}
