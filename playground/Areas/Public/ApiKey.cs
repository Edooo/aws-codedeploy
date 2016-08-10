using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Playground.Areas.Public
{
    public class ApiKey
    {
        static int API_KEY_LENGTH = 20; // plus 3 more chars
        static public string GenerateKey(int sequentialNumber)
        {
            var key = new byte[30];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(key); // make a rather long random key
            }
            string rndPiece = Convert.ToBase64String(key); // A..Za..z0..9/+  (convert to Base64 .. plenty long)
            if (rndPiece.Length > API_KEY_LENGTH) rndPiece = rndPiece.Substring(0, API_KEY_LENGTH); // truncate down to wanted length
            rndPiece = rndPiece.Replace('/', 'b'); // remove icky non-alphanumeric
            rndPiece = rndPiece.Replace('+', 'B');
            int n = sequentialNumber % 999;
            string apiKey = "a" + n + rndPiece; // "a" <number> <random>
            return apiKey;
        }
    }
}
