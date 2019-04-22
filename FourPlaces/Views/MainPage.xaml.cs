using Storm.Mvvm.Forms;
using FourPlaces.ViewModels;
using FourPlaces.Models;
using FourPlaces.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using FourPlaces.Utils;
using System.Threading.Tasks;

namespace FourPlaces.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : BaseContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            Title = "Places";

            BindingContext = new MainPageViewModel();
            ListeLieux.ItemsSource = Constantes.Places;

            ListeLieux.RefreshCommand = new Command(() => {
                Constantes.Places = MainPageModel.GetPlaces();
                ListeLieux.EndRefresh();
            });

            Constantes.authModel?.AutoRefreshToken();
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if(sender is ListView l && l.SelectedItem is Place p)
            {
                l.SelectedItem = null;

                Navigation.PushAsync(new PlaceDetails(p.id));
            }
        }
    }
}
