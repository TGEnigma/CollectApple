using CollectApple.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace CollectApple.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public virtual ICollection<UserCollection> Collections { get; set; }
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
