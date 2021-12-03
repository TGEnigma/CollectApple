using CollectApple.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace CollectApple.Models
{
    public class UserCollection : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CollectionId { get; set; }

        public User User { get; set; }
        public Collection Collection { get; set; }
        public virtual ICollection<UserCollectible> Collectibles { get; set; }

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
