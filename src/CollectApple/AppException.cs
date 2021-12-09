using System;

namespace CollectApple
{
    public class AppException : Exception
    {
        public AppException(string message)
            : base(message)
        {
        }
    }

    public class AppUnknownUserException : AppException
    {
        public string Email { get; }

        public AppUnknownUserException(string email)
            : base(FormatMessage(email))
        {
            Email = email;
        }

        private static string FormatMessage(string email)
        {
            return $"No known user with the given email";
        }
    }
}
