using System;

namespace CollectApple
{
    public class AppException : ApplicationException
    {
        public AppException(string message)
            : base(message)
        {
        }
    }

    public class InvalidCredentialsException : AppException
    {
        public string Email { get; }

        public InvalidCredentialsException(string email)
            : base( $"No known user with the given email" )
        {
            Email = email;
        }

        private static string FormatMessage(string email)
        {
            return $"No known user with the given email";
        }
    }

    public class ExpiredPasswordException : AppException
    {
        public ExpiredPasswordException() 
            : base( "The password for this user has expired." )
        {
        }
    }
}
