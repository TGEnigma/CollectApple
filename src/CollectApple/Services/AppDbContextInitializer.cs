namespace CollectApple.Services
{
    public class AppDbContextInitializer
    {
        public static void Initialize( AppDbContext context )
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var userService = new UserService();
            var collectibleService = new CollectibleService();
            var collectionService = new CollectionService();

            var user = userService.CreateUser( "test@mail.com", "password1234" );

            // Initialize library collections
            var userCollection = collectionService.CreateUserCollection(
                user,
                "Yugioh Vol.1",
                "Vol.1, short for Volume 1, is a Booster Pack that was released on February 4, 1999[1] as the first set in the Yu-Gi-Oh! Official Card Game. Like all Series 1 sets, Vol.1 cards used the Series 1 card layout, distinguished from later layouts by the lack of a Card Number beneath the card's artwork and the Eye of Anubis Hologram in its lower-right corner.",
                "https://static.wikia.nocookie.net/yugioh/images/0/00/V1-BoosterJP.jpg/revision/latest/scale-to-width-down/202?cb=20060802095032" );
            var collection = userCollection.Collection;

            var userCollectible1 = collectibleService.CreateUserCollectible( userCollection,
                "Hitotsu-Me Giant",
                "A one-eyed behemoth with thick, powerful arms made for delivering punishing blows.",
                "https://static.wikia.nocookie.net/yugioh/images/e/e5/HitotsuMeGiant-DPKB-EN-C-1E.png/revision/latest/scale-to-width-down/300?cb=20100425061014" );
            var userCollectible2 = collectibleService.CreateUserCollectible( userCollection,
                "Dark Magician",
                 "Ultra Rare. The ultimate wizard in terms of attack and defense.",
                 "https://static.wikia.nocookie.net/yugioh/images/b/b6/DarkMagician-DUPO-EN-UR-LE.png/revision/latest/scale-to-width-down/300?cb=20200826173728" );
            var userColectible3 = collectibleService.CreateUserCollectible( userCollection,
                 "Gaia The Fierce Knight",
                 "A knight whose horse travels faster than the wind. His battle-charge is a force to be reckoned with.",
                 "https://static.wikia.nocookie.net/yugioh/images/7/75/GaiaTheFierceKnight-MIL1-EN-R-1E.png/revision/latest/scale-to-width-down/300?cb=20200414204303" );
        }
    }
}
