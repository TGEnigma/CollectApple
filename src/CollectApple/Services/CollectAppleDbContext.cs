using CollectApple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectApple.Services
{
    public class CollectAppleDbContext
    {
        public static CollectAppleDbContext Instance { get; } = new CollectAppleDbContext();

        public List<User> Users { get; private set; }
        public List<Collection> Collections { get; private set; }
        public List<Collectible> Collectibles { get; private set; }
        public List<LibraryCollectible> LibraryCollectibles { get; private set; }
        public List<LibraryCollection> LibraryCollections { get; private set; }

        public CollectAppleDbContext()
        {
            // Initialize users
            Users = new List<User>();
            var testUser = new User()
            {
                Id = 1,
                Email = "test@mail.com",
                Password = "password1234",
                Collections = new List<Collection>()
            };
            Users.Add( testUser );

            // Initialize library collections
            LibraryCollections = new List<LibraryCollection>();
            var yugiohVol1LibraryCollection = new LibraryCollection()
            {
                Id = 1,
                Name = "Yugioh Vol.1",
                Description = "Vol.1, short for Volume 1, is a Booster Pack that was released on February 4, 1999[1] as the first set in the Yu-Gi-Oh! Official Card Game. Like all Series 1 sets, Vol.1 cards used the Series 1 card layout, distinguished from later layouts by the lack of a Card Number beneath the card's artwork and the Eye of Anubis Hologram in its lower-right corner.",
                ImageUrl = "https://static.wikia.nocookie.net/yugioh/images/0/00/V1-BoosterJP.jpg/revision/latest/scale-to-width-down/202?cb=20060802095032",
                Items = new List<LibraryCollectible>()
                {
                    new LibraryCollectible()
                    {
                        Id = 1,
                        Description = "A one-eyed behemoth with thick, powerful arms made for delivering punishing blows.",
                        ImageUrl = "https://static.wikia.nocookie.net/yugioh/images/e/e5/HitotsuMeGiant-DPKB-EN-C-1E.png/revision/latest/scale-to-width-down/300?cb=20100425061014",
                        Name = "Hitotsu-Me Giant",
                    },
                    new LibraryCollectible()
                    {
                        Id = 2,
                        Description = "Ultra Rare. The ultimate wizard in terms of attack and defense.",
                        ImageUrl = "https://static.wikia.nocookie.net/yugioh/images/b/b6/DarkMagician-DUPO-EN-UR-LE.png/revision/latest/scale-to-width-down/300?cb=20200826173728",
                        Name = "Dark Magician",
                    },
                    new LibraryCollectible()
                    {
                        Id = 3,
                        Description = "A knight whose horse travels faster than the wind. His battle-charge is a force to be reckoned with.",
                        ImageUrl = "https://static.wikia.nocookie.net/yugioh/images/7/75/GaiaTheFierceKnight-MIL1-EN-R-1E.png/revision/latest/scale-to-width-down/300?cb=20200414204303",
                        Name = "Gaia The Fierce Knight",
                    },
                }
            };
            LibraryCollections.Add( yugiohVol1LibraryCollection );

            // Initialize library collectibles by adding all collectibles inside collections
            LibraryCollectibles = new List<LibraryCollectible>();
            foreach ( var item in LibraryCollections.SelectMany( x => x.Items ) )
                LibraryCollectibles.Add( item );

            // Initialize collections
            Collections = new List<Collection>();
            var yugiohVol1Collection = new Collection()
            {
                Id = 1,
                Name = "Yugioh Vol.1",
                Description = "Vol.1, short for Volume 1, is a Booster Pack that was released on February 4, 1999[1] as the first set in the Yu-Gi-Oh! Official Card Game. Like all Series 1 sets, Vol.1 cards used the Series 1 card layout, distinguished from later layouts by the lack of a Card Number beneath the card's artwork and the Eye of Anubis Hologram in its lower-right corner.",
                ImageUrl = "https://static.wikia.nocookie.net/yugioh/images/0/00/V1-BoosterJP.jpg/revision/latest/scale-to-width-down/202?cb=20060802095032",
                Owner = testUser,
                Items = new List<Collectible>()
                {
                    new Collectible()
                    {
                        Id = 1,
                        Description = "A one-eyed behemoth with thick, powerful arms made for delivering punishing blows.",
                        ImageUrl = "https://static.wikia.nocookie.net/yugioh/images/e/e5/HitotsuMeGiant-DPKB-EN-C-1E.png/revision/latest/scale-to-width-down/300?cb=20100425061014",
                        Name = "Hitotsu-Me Giant",
                        Owner = testUser,
                        Price = 0.99m,
                        State = CollectibleState.ForSale,
                    },
                    new Collectible()
                    {
                        Id = 2,
                        Description = "Ultra Rare. The ultimate wizard in terms of attack and defense.",
                        ImageUrl = "https://static.wikia.nocookie.net/yugioh/images/b/b6/DarkMagician-DUPO-EN-UR-LE.png/revision/latest/scale-to-width-down/300?cb=20200826173728",
                        Name = "Dark Magician",
                        Owner = testUser,
                        State = CollectibleState.DisplayOnly,
                    },
                }
            };
            Collections.Add( yugiohVol1Collection );
            Collections.Add( yugiohVol1Collection );

            // Link collections to users
            testUser.Collections.Add( yugiohVol1Collection );

            // Initialize collectibles by adding all collectibles inside collections
            Collectibles = new List<Collectible>();
            foreach ( var item in Collections.SelectMany( x => x.Items ) )
                Collectibles.Add( item );
        }
    }
}
