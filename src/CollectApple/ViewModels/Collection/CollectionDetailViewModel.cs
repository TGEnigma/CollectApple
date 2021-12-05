using CollectApple.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CollectApple.ViewModels
{
    [QueryProperty( nameof( Id ), nameof( Id ) )]
    public class CollectionDetailViewModel : BaseViewModel
    {
        private int id;
        private string name;
        private string description;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                LoadId( value );
            }
        }

        public string Name
        {
            get => name;
            set => SetProperty( ref name, value );
        }

        public string Description
        {
            get => description;
            set => SetProperty( ref description, value );
        }

        public ObservableCollection<CollectibleViewModel> Collectibles { get; }

        public CollectionDetailViewModel()
        {
            Collectibles = new ObservableCollection<CollectibleViewModel>();
        }

        public async void LoadId( int id )
        {
            try
            {
                var collection = CollectionService.GetCollectionById( id )
                    .Include( x => x.Collectibles )
                    .FirstOrDefault();

                this.id = id;
                Name = collection.Name;
                Description = collection.Description;
                foreach ( var item in collection.Collectibles )
                {
                    Collectibles.Add( new CollectibleViewModel()
                    {
                        Id = item.Id,
                        ImageUrl = item.ImageUrl,
                        Name = item.Name,
                    });
                }
            }
            catch ( Exception ex )
            {
                Debug.WriteLine( "Failed to Load Item" );
            }
        }
    }
}
