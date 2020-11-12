using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MapNotepad.Validators
{
    public class Validator
    {
        private const string RegexEmail = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        //public const string RegexEmailBeforeAt = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*";
        //public const string RegexEmailAfterAt = @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
        private const string _regexPasswordContainsNumber = @"[0-9]+";
        private const string _regexPasswordContainsUpper = @"[A-Z]+";
        private const string _regexPasswordContainsLower = @"[a-z]+";
        static Validator()
        {

        }
        public static bool MatchesRequirements(string value)
        {
            bool isMatch = false;

            var hasNumber = new Regex(_regexPasswordContainsNumber);
            var hasUpperChar = new Regex(_regexPasswordContainsUpper);
            var hasLowerChar = new Regex(_regexPasswordContainsLower);

            if (!string.IsNullOrEmpty(value))
            {
                isMatch = hasNumber.IsMatch(value) && hasUpperChar.IsMatch(value) && hasLowerChar.IsMatch(value);
            }

            return isMatch;
        }
        public static bool IsEmail(string value)
        {
            bool isMatch = false;

            if (!string.IsNullOrEmpty(value))
            {
                isMatch = Regex.IsMatch(value, RegexEmail, RegexOptions.IgnoreCase);
            }

            return isMatch;
        }

    }
}
