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

            //ListeLieux.ItemsSource = new List<Place>();
            //BindingContext = null;
            //Task.Run(InitBinding);
            BindingContext = new MainPageViewModel();
            ListeLieux.ItemsSource = ((MainPageViewModel)base.BindingContext).Places;

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

        async Task<bool> InitBinding()
        {
            base.BindingContext = new MainPageViewModel();

            ListeLieux.ItemsSource = ((MainPageViewModel)base.BindingContext).Places;
            return true;
            
        }
    }
}
