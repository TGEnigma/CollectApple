using CollectApple.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CollectApple.Views
{
    public partial class CollectionDetailPage : ContentPage
    {
        public CollectionDetailPage()
        {
            InitializeComponent();
            BindingContext = new CollectionDetailViewModel();
        }
    }
}