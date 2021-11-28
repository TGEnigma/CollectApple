using System;
using System.Collections.Generic;
using System.Text;

namespace CollectApple.Models
{
    public class LibraryCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<LibraryCollectible> Items { get; set; }
    }

    public class LibraryCollectible
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }

    public class Collectible
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public CollectibleState State { get; set; }
        public decimal Price { get; set; }
        public User Owner { get; set; }
    }

    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<Collectible> Items { get; set; } = new List<Collectible>();
        public User Owner { get; set; }
    }

    public enum CollectibleState
    {
        DisplayOnly,
        ForSale,
        ForTrade,
        Sold,
        Traded
    }

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Collection> Collections { get; set; }
    }
}
