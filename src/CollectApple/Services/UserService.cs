using CollectApple.Models;
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

        private static string ComputeHash( string input )
        {
            using ( SHA256 algo = SHA256.Create() )
                return BitConverter.ToString( algo.ComputeHash( Encoding.UTF8.GetBytes( input ) ) );
        }

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

        public void DeleteUser( User user )
        {
            Context.Users.Remove( user );
            Context.SaveChanges();
        }

        public void Login( string email, string password )
        {
            var user = GetUserByEmail( email );
            if ( user == null )
                throw new AppException( "Unknown combination of email and password" );

            var hash = ComputeHash( password + user.PasswordSalt );
            if ( user.PasswordHash != hash )
                throw new AppException( "Unknown combination of email and password" );

            Current = user;
        }

        private string GenerateSixCharacterCode()
        {
            var charset = new[] { 'A', 'B',  'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 
                                  'M', 'N',  'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 
                                  'Y', 'Z',  '2', '3', '4', '5', '6', '7', '8', '9' };
            var rnd = new Random();
            var result = new char[6];
            for ( int i = 0; i < 6; i++ )
                result[i] = charset[rnd.Next(0, charset.Length)];
            return new string(result);
        }

        public User GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException($"'{nameof(email)}' cannot be null or whitespace.", nameof(email));

            email = email.Trim().ToLower();
            return Context.Users.Where(x => x.Email == email).FirstOrDefault();
        }

        public async Task<bool> ResetPasswordAsync(string email, string code)
        {
            var user = GetUserByEmail(email);
            if (user == null)
                throw new AppException("No user exists with the given email exists.");

            if (code != user.PasswordResetCode || DateTime.Now > user.PasswordResetCodeExpiry)
            {
                await SendResetPasswordEmailAsync(email);
                throw new AppException("Invalid password reset code. A new reset code has been sent.");
            }

            user.PasswordHash = null;
            user.PasswordSalt = null;
            Context.Update(user);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<object> SendResetPasswordEmailAsync(string email)
        {
            var user = GetUserByEmail(email);
            if (user == null)
                return Task.CompletedTask;

            var passwordResetCode = GenerateSixCharacterCode();

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential("collectapple@gmail.com", "DHVBq_79L.mzvfa");
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Host = "smtp.gmail.com";

                var message = new MailMessage()
                {
                    From = new MailAddress("collectapple@gmail.com"),
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
                message.To.Add(new MailAddress(email));
                await smtpClient.SendMailAsync(message);

                // If mail was sent successfully, save the password reset code to the user
                // and set it to valid for 24 hours
                user.PasswordResetCode = passwordResetCode;
                user.PasswordResetCodeExpiry = DateTime.Now.AddDays(1);
                Context.Update(user);
                await Context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }
    }
}
