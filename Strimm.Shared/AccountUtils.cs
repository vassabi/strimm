using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Strimm.Shared
{
    public static class AccountUtils
    {
        public static PasswordScore GetPasswordStrengthScore(string password)
        {
            var score = PasswordScore.VeryWeak;

            Regex regexForDigits        = new Regex(@"[/\d+/]");
            Regex regexForLowChars      = new Regex(@"[a-z]");
            Regex regexForUpperChars    = new Regex(@"[A-Z]");
            Regex regexForSymbol        = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (password.Length < 1)
            {
                score = PasswordScore.Blank;
            }

            if (password.Length < 4)
            {
                score = PasswordScore.VeryWeak;
            }

            if (password.Length >= 6)
            {
                score++;
            }

            if (password.Length >= 7)
            {
                score++;
            }

            if (password.Length >= 8)
            {
                score++;
            }

            if (regexForDigits.IsMatch(password))
            {
                score++;
            }

            if (regexForLowChars.IsMatch(password))
            {
                score++;
            }

            if (regexForUpperChars.IsMatch(password))
            {
                score++;
            }

            if (regexForSymbol.IsMatch(password))
            {
                score++;
            }

            return (PasswordScore)score;
        }

        public static string GenerateAccountNumber()
        {
            var builder = new StringBuilder();

            builder.Append(RandomUtils.RandomNumber(10000, 99999));
            builder.Append(RandomUtils.RandomString(3, false));
            
            return builder.ToString();
        }

       
    }
}
