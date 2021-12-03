using CollectApple.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CollectApple.Services
{
    /// <summary>
    /// Service that provides CRUD operations on Collection domain objects.
    /// </summary>
    public class CollectionService
    {
        public AppDbContext Context => DependencyService.Get<AppDbContext>();

        public CollectionService()
        {
        }

        public IQueryable<Collection> GetAllCollections()
        {
            return Context.Collections;
        }

        public IQueryable<UserCollection> GetAllUserCollections()
        {
            return Context.UserCollections;
        }

        public IQueryable<UserCollection> GetUserCollections(int userId)
        {
            return Context.UserCollections.Where( x => x.UserId == userId );
        }

        public Collection GetCollectionByName(string name)
        {
            return Context.Collections.FirstOrDefault( x => x.Name == name );
        }

        public IQueryable<UserCollection> GetUserCollectionByName(User user, string name)
        {
            return Context.UserCollections.Where( x => x.UserId == user.Id );
        }

        public UserCollection CreateUserCollection( User user, string name, string description, string imageUrl )
        {
            using ( var transaction = Context.Database.BeginTransaction() )
            {
                var detail = new Collection()
                {
                    Name = name,
                    Description = description,
                    ImageUrl = imageUrl,
                };
                Context.Collections.Add( detail );
                Context.SaveChanges();

                var collection = new UserCollection()
                {
                    CollectionId = detail.Id,
                    UserId = user.Id
                };
                Context.UserCollections.Add( collection );
                transaction.Commit();
                return collection;
            }
        }

        public void DeleteUserCollection( UserCollection collection )
        {
            Context.UserCollections.Remove( collection );
            Context.SaveChanges();
        }
    }
}
