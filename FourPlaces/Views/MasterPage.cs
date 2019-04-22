using System;

using Xamarin.Forms;

namespace FourPlaces.Views
{
    public class MasterPage : ContentPage
    {
        public MasterPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

