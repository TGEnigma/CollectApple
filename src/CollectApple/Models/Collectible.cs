using CollectApple.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CollectApple.Models
{
    public class Collectible : IEntity
    {
        public int Id { get; set; }
        public int CollectionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public virtual Collection Collection { get; set; }

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
