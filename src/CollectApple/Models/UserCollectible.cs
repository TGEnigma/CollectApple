using CollectApple.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CollectApple.Models
{
    public class UserCollectible : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UserCollectionId { get; set; }
        public int CollectibleId { get; set; }
        public CollectibleState State { get; set; }
        public decimal? Price { get; set; }

        public User User { get; set; }
        public UserCollection UserCollection { get; set; }
        public Collectible Collectible { get; set; }

        public void OnAdded( AppDbContext db, EntityEntry entry )
        {
        }

        public void OnDeleted( AppDbContext db, EntityEntry entry )
        {
        }

        public void OnModified( AppDbContext db, EntityEntry entry )
        {
        }
    }
}
