using CollectApple.Services;
using System;
using System.Collections.Generic;
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

        public async void LoadId( int id )
        {
            try
            {
                var item = CollectAppleDbContext.Instance.Collections.Where( x => x.Id == id ).FirstOrDefault();
                this.id = id;
                Name = item.Name;
                Description = item.Description;
            }
            catch ( Exception )
            {
                Debug.WriteLine( "Failed to Load Item" );
            }
        }
    }
}
