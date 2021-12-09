using CollectApple.Services;
using CollectApple.Views;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CollectApple
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            var dbContext = new AppDbContext();
            DependencyService.Register<MockDataStore>();
            DependencyService.RegisterSingleton( dbContext );
            DependencyService.Register<UserService>();
            DependencyService.Register<CollectionService>();
            DependencyService.Register<CollectibleService>();

            AppDbContextInitializer.Initialize( dbContext );

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
