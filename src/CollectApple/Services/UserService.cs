using CollectApple.Models;
using CollectApple.Utilities;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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

        /// <summary>
        /// Compute cryptographic hash.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string ComputeHash( string input )
        {
            using ( SHA256 algo = SHA256.Create() )
                return BitConverter.ToString( algo.ComputeHash( Encoding.UTF8.GetBytes( input ) ) );
        }

        /// <summary>
        /// Create a new user account.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User CreateUser( string email, string password )
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

        /// <summary>
        /// Deletes a user account.
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser( User user )
        {
            Context.Users.Remove( user );
            Context.SaveChanges();
        }

        /// <summary>
        /// Performs a user signin using the given credentials.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public void SignIn( string email, string password )
        {
            var user = GetUserByEmail( email );
            if ( user == null )
            {
                // User not found
                throw new InvalidCredentialsException( email );
            }

            if ( user.PasswordExpired )
            {
                // Password needs to be changed
                throw new ExpiredPasswordException();
            }

            var hash = ComputeHash( password + user.PasswordSalt );
            if ( user.PasswordHash != hash )
            {
                // Invalid password
                throw new InvalidCredentialsException( email );
            }

            Current = user;
        }

        public User GetUserByEmail( string email )
        {
            if ( string.IsNullOrWhiteSpace( email ) )
                throw new AppException( $"'{nameof( email )}' cannot be null or whitespace." );

            email = email.Trim().ToLower();
            return Context.Users.Where( x => x.Email == email ).FirstOrDefault();
        }

        public async Task<bool> ResetPasswordAsync( string email, string code )
        {
            var user = GetUserByEmail( email );
            if ( user == null )
                throw new AppException( "No user exists with the given email exists." );

            if ( DateTime.Now > user.PasswordResetCodeExpiry )
            {
                await SendResetPasswordEmailAsync( email );
                throw new AppException( "Password reset code has expired. A new reset code has been sent." );
            }

            if ( code != user.PasswordResetCode )
            {

            }

            user.PasswordHash = null;
            user.PasswordSalt = null;
            user.PasswordExpired = true;
            Context.Update( user );
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<object> SendResetPasswordEmailAsync( string email )
        {
            var user = GetUserByEmail( email );
            if ( user == null )
                return Task.CompletedTask;

            var passwordResetCode = CharacterCodeGenerator.Generate( 6 );

            using ( var smtpClient = new SmtpClient() )
            {
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential( "collectapple@gmail.com", "DHVBq_79L.mzvfa" );
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Host = "smtp.gmail.com";

                var message = new MailMessage()
                {
                    From = new MailAddress( "collectapple@gmail.com" ),
                    Subject = "Your password reset request",
                    Body = $@"
<h5>You have requested to reset the password of your CollectApp account.</h5>
<h6>You can reset your password using the following code:</h6>
<p>{passwordResetCode}<p>
<br />
<br />
If you didn't request to reset your password, please ignore this email.",
                    IsBodyHtml = true,
                };
                message.To.Add( new MailAddress( email ) );
                await smtpClient.SendMailAsync( message );

                // If mail was sent successfully, save the password reset code to the user
                // and set it to valid for 24 hours
                user.PasswordResetCode = passwordResetCode;
                user.PasswordResetCodeExpiry = DateTime.Now.AddDays( 1 );
                Context.Update( user );
                await Context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }
    }
}
