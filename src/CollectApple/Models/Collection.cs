using CollectApple.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollectApple.Models
{
    public class Collection : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<Collectible> Collectibles { get; set; }

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
