using System;
using System.Collections.Generic;
using System.Linq;
using FourPlaces.Models;
using FourPlaces.ViewModels;
using FourPlaces.Views;
using Storm.Mvvm.Forms;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace FourPlaces.Views
{
    public partial class PlaceDetails : BaseContentPage
    {
        public PlaceDetails(int id = 1)
        {
            InitializeComponent();

            Place p = CommentsModel.GetPlaceWithComments(id);

            BindingContext = new PlaceDetailsViewModel { BindedPlace = p };
            Title = ((PlaceDetailsViewModel)BindingContext)?.BindedPlace.title;

            ListComments.ItemsSource = p.comments;

            // Setting map properties to make it show the current place
            Position pos = new Position(p.latitude, p.longitude);
            map.InitialCameraUpdate = CameraUpdateFactory.NewCameraPosition(
                new CameraPosition(
                    pos,
                    13,
                    0,
                    0
                )
            );

            Pin pin = new Pin();
            pin.Position = pos;
            pin.Label = p.title;
            map.Pins.Add(pin);
        }
    }
}
