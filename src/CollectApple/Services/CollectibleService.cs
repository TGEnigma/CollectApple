using CollectApple.Models;
using System;
using System.Linq;
using Xamarin.Forms;

namespace CollectApple.Services
{
    public class CollectibleService
    {
        public AppDbContext Context => DependencyService.Get<AppDbContext>();

        public CollectibleService()
        {
        }

        public IQueryable<Collectible> GetAllCollectibles()
        {
            return Context.Collectibles;
        }

        public IQueryable<UserCollectible> GetAllUserCollectibles()
        {
            return Context.UserCollectibles;
        }

        public IQueryable<UserCollectible> GetUserCollectibles( User user )
        {
            return Context.UserCollectibles.Where( x => x.Id == user.Id );
        }

        public Collectible CreateCollectible( string name, string description, string imageUrl )
        {
            var collectible = new Collectible()
            {
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
            };
            Context.Collectibles.Add( collectible );
            Context.SaveChanges();
            return collectible;
        }

        public UserCollectible CreateUserCollectible( UserCollection userCollection, Collectible collectible )
        {
            var userCollectible = new UserCollectible()
            {
                UserCollectionId = userCollection.Id,
                CollectibleId = collectible.Id,
                Price = null,
                State = CollectibleState.New,
                UserId = userCollection.UserId,
            };

            Context.UserCollectibles.Add( userCollectible );
            Context.SaveChanges();
            return userCollectible;
        }

        public UserCollectible CreateUserCollectible( UserCollection userCollection, string name, string description, string imageUrl )
        {
            using ( var transaction = Context.Database.BeginTransaction() )
            {
                var collectible = CreateCollectible( name, description, imageUrl );
                return CreateUserCollectible( userCollection, collectible );
            }
        }

        public void DeleteUserCollectible( UserCollectible collectible )
        {
            Context.UserCollectibles.Remove( collectible );
            Context.SaveChanges();
        }

        public void SetCollectibleDisplayOnly( UserCollectible collectible )
        {
            collectible.State = CollectibleState.DisplayOnly;
            Context.SaveChanges();
        }

        public void BeginSellingCollectible( UserCollectible collectible, decimal price )
        {
            collectible.State = CollectibleState.ForSale;
            collectible.Price = price;
            Context.SaveChanges();
        }

        public void BeginTradingCollectible( UserCollectible collectible )
        {
            collectible.State = CollectibleState.ForTrade;
            Context.SaveChanges();
        }

        public UserCollection GetUserCollection( User user, Collection collection )
        {
            return Context.UserCollections.Where( x => x.UserId == user.Id && x.CollectionId == collection.Id ).FirstOrDefault();
        }

        public UserCollectible BuyCollectible( User user, UserCollectible userCollectible )
        {
            using ( var transaction = Context.Database.BeginTransaction() )
            {
                if ( user.Id == userCollectible.Id )
                    throw new Exception( "You can't buy your own collectible" );

                userCollectible.State = CollectibleState.Sold;

                var collectible = userCollectible.Collectible;
                var collection = collectible.Collection;
                var newUserCollection = GetUserCollection( user, collection );
                if ( newUserCollection == null )
                    newUserCollection = CreateUserCollection( user, collection );

                return CreateUserCollectible( newUserCollection, collectible );
            }
        }

        private UserCollection CreateUserCollection( User user, Collection collection )
        {
            var newUserCollection = new UserCollection()
            {
                CollectionId = collection.Id,
                UserId = user.Id,
            };
            Context.UserCollections.Add( newUserCollection );
            Context.SaveChanges();

            return newUserCollection;
        }
    }
}
