using CollectApple.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectApple.Services
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Collectible> Collectibles { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<UserCollectible> UserCollectibles { get; set; }
        public DbSet<UserCollection> UserCollections { get; set; }

        public AppDbContext()
        {
            ChangeTracker.StateChanged += OnStateChanged;
        }

        /// <summary>
        /// Validate entity when a state change is detected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnStateChanged( object sender, EntityStateChangedEventArgs e )
        {
            var entity = (IEntity)e.Entry.Entity;

            try
            {
                switch ( e.OldState )
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        entity.OnDeleted( this, e.Entry );
                        break;
                    case EntityState.Modified:
                        entity.OnModified( this, e.Entry );
                        break;
                    case EntityState.Added:
                        entity.OnAdded( this, e.Entry );
                        break;
                    default:
                        break;
                }
            }
            catch ( Exception ex )
            {
                // TODO
            }
        }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            optionsBuilder
                .UseInMemoryDatabase( "AppDb" )
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .ConfigureWarnings( x => x.Log( InMemoryEventId.TransactionIgnoredWarning ) );
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
        }
    }
}
