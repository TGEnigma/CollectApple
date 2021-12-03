using CollectApple.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollectApple.Models
{
    public interface IEntity
    {
        int Id { get; set; }
        void OnDeleted( AppDbContext db, EntityEntry entry );
        void OnModified( AppDbContext db, EntityEntry entry );
        void OnAdded( AppDbContext db, EntityEntry entry );
    }
}
