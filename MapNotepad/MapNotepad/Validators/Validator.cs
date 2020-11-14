using System.Text.RegularExpressions;

namespace MapNotepad.Validators
{
    public class Validator
    {
        private const string _RegexEmail = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        private const string _RegexPasswordContainsNumber = @"[0-9]+";
        private const string _RegexPasswordContainsUpper = @"[A-Z]+";
        private const string _RegexPasswordContainsLower = @"[a-z]+";
        static Validator()
        {

        }
        public static bool MatchesRequirements(string value)
        { 
            var hasNumber = new Regex(_RegexPasswordContainsNumber);
            var hasUpperChar = new Regex(_RegexPasswordContainsUpper);
            var hasLowerChar = new Regex(_RegexPasswordContainsLower);

            return !string.IsNullOrEmpty(value)
                && hasNumber.IsMatch(value)
                && hasUpperChar.IsMatch(value)
                && hasLowerChar.IsMatch(value);
        }
        public static bool IsEmail(string value)
        {
            return !string.IsNullOrEmpty(value)
                && Regex.IsMatch(value, _RegexEmail, RegexOptions.IgnoreCase);
        }

    }
}
