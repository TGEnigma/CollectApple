using CollectApple.Models;
using CollectApple.Services;
using CollectApple.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CollectApple.ViewModels
{
    [QueryProperty( nameof( UserId ), nameof( UserId ) )]
    public class CollectionsViewModel : BaseViewModel
    {
        private CollectionViewModel _selectedItem;

        public ObservableCollection<CollectionViewModel> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<CollectionViewModel> ItemTapped { get; }
        public int? UserId { get; set; }

        public CollectionsViewModel()
        {
            Title = "My collections";
            Items = new ObservableCollection<CollectionViewModel>();
            LoadItemsCommand = new Command( async () => await ExecuteLoadItemsCommand() );

            ItemTapped = new Command<CollectionViewModel>( OnItemSelected );

            AddItemCommand = new Command( OnAddItem );
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = CollectionService.GetUserCollections( UserId.GetValueOrDefault( 1 ) );
                foreach ( var item in items )
                {
                    Items.Add( new CollectionViewModel()
                    {
                        Description = item.Collection.Description,
                        Id = item.Id,
                        Name = item.Collection.Name,
                        ImageUrl = item.Collection.ImageUrl,
                    } );
                }
            }
            catch ( Exception ex )
            {
                Debug.WriteLine( ex );
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public CollectionViewModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty( ref _selectedItem, value );
                OnItemSelected( value );
            }
        }

        private async void OnAddItem( object obj )
        {
            await Shell.Current.GoToAsync( nameof( NewItemPage ) );
        }

        async void OnItemSelected( CollectionViewModel item )
        {
            if ( item == null )
                return;

            // This will push the CollectionDetailPage onto the navigation stack
            await Shell.Current.GoToAsync( $"{nameof( CollectionDetailPage )}?{nameof( CollectionDetailPage.Id )}={item.Id}" );
        }
    }
}
