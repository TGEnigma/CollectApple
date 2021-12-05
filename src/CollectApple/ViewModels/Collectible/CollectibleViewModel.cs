namespace CollectApple.ViewModels
{
    public class CollectibleViewModel : BaseViewModel
    {
        private int id;
        private string name;
        private string imageUrl;

        public int Id
        {
            get => id;
            set => SetProperty( ref id, value );
        }

        public string Name
        {
            get => name;
            set => SetProperty( ref name, value );
        }

        public string ImageUrl
        {
            get => imageUrl;
            set => SetProperty( ref imageUrl, value );
        }
    }
}
