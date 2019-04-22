using System;
using System.Collections.Generic;
using System.Linq;
using FourPlaces.Models;
using FourPlaces.Utils;
using FourPlaces.ViewModels;
using FourPlaces.Views;
using Storm.Mvvm.Forms;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace FourPlaces.Views
{
    public partial class PlaceDetails : BaseContentPage
    {
        private int placeID = 1;

        public PlaceDetails(int id = 1)
        {
            InitializeComponent();

            placeID = id;
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

            Constantes.authModel?.AutoRefreshToken();
        }


        async void PostCommentClick(object sender, EventArgs args)
        {
            if (Constantes.authModel!=null && Constantes.authModel.Connected)
            {
                bool succeed = await CommentsModel.PostComment(comment.Text, placeID);
                if (succeed)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Thanks for your comment !");

                    Place p = CommentsModel.GetPlaceWithComments(placeID);
                    BindingContext = new PlaceDetailsViewModel { BindedPlace = p };
                    ListComments.ItemsSource = p.comments;
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert("An error occured while posting your comment. Please try again later.");
                }
                
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("You must be logged to comment.");
            }
            comment.Text = "";
        }
    }
}
