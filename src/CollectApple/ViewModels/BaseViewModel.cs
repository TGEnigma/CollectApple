using CollectApple.Localization;
using CollectApple.Models;
using CollectApple.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace CollectApple.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected AppDbContext DbContext => DependencyService.Get<AppDbContext>();
        protected CollectibleService CollectibleService => DependencyService.Get<CollectibleService>();
        protected CollectionService CollectionService => DependencyService.Get<CollectionService>();
        protected UserService UserService => DependencyService.Get<UserService>();


        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty( ref isBusy, value ); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty( ref title, value ); }
        }

        protected bool SetProperty<T>( ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null )
        {
            if ( EqualityComparer<T>.Default.Equals( backingStore, value ) )
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged( propertyName );
            return true;
        }

        protected void HandleException( Exception ex )
        {
            Shell.Current.DisplayAlert( Strings.Error, ex.Message, Strings.OK );
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged( [CallerMemberName] string propertyName = "" )
        {
            var changed = PropertyChanged;
            if ( changed == null )
                return;

            changed.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }
        #endregion
    }

    public class _BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty( ref isBusy, value ); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty( ref title, value ); }
        }

        protected bool SetProperty<T>( ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null )
        {
            if ( EqualityComparer<T>.Default.Equals( backingStore, value ) )
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged( propertyName );
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged( [CallerMemberName] string propertyName = "" )
        {
            var changed = PropertyChanged;
            if ( changed == null )
                return;

            changed.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }
        #endregion
    }
}
