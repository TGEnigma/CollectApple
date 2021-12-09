using System;
using System.Collections.Generic;
using System.Text;

namespace CollectApple.Utilities
{
    public static class CharacterCodeGenerator
    {
        static char[] characterCodeCharset = new[]
        {
            'A', 'B',  'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L',
            'M', 'N',  'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
            'Y', 'Z',  '2', '3', '4', '5', '6', '7', '8', '9'
        };

        public static string Generate( int charCount )
        {
            var rnd = new Random();
            var result = new char[ charCount ];
            for ( int i = 0; i < charCount; i++ )
                result[ i ] = characterCodeCharset[ rnd.Next( 0, characterCodeCharset.Length ) ];
            return new string( result );
        }
    }
}
