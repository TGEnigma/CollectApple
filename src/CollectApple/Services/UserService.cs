using CollectApple.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;

namespace CollectApple.Services
{
    public class UserService
    {
        public User Current { get; private set; }
        public AppDbContext Context => DependencyService.Get<AppDbContext>();

        public UserService()
        {
        }

        private static string ComputeHash( string input )
        {
            using ( SHA256 algo = SHA256.Create() )
                return BitConverter.ToString( algo.ComputeHash( Encoding.UTF8.GetBytes( input ) ) );
        }

        public User Create( string email, string password )
        {
            var passwordSalt = Guid.NewGuid().ToString();
            var user = new User()
            {
                Email = email.Trim().ToLower(),
                PasswordHash = ComputeHash( password + passwordSalt ),
                PasswordSalt = passwordSalt,
            };
            Context.Users.Add( user );
            Context.SaveChanges();
            return user;
        }

        public void Delete( User user )
        {
            Context.Users.Remove( user );
            Context.SaveChanges();
        }

        public void Login( string email, string password )
        {
            if ( email == null )
                throw new ArgumentNullException( nameof( email ) );

            if ( password == null )
                throw new ArgumentNullException( nameof ( password ) );

            email = email.Trim().ToLower();

            var user = Context.Users.Where( x => x.Email == email ).FirstOrDefault();
            if ( user == null )
                throw new Exception( "Unknown combination of email and password" );

            var hash = ComputeHash( password + user.PasswordSalt );
            if ( user.PasswordHash != hash )
                throw new Exception( "Unknown combination of email and password" );

            Current = user;
        }
    }
}
