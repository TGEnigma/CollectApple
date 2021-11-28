using CollectApple.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CollectApple.Views
{
    public partial class CollectibleDetailPage : ContentPage
    {
        public CollectibleDetailPage()
        {
            InitializeComponent();
            BindingContext = new CollectibleDetailViewModel();
        }
    }
}