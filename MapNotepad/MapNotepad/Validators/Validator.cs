using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using MapNotepad.Enums;

namespace MapNotepad.Validators
{
    public static class Validator
    {
        private const string RegexEmail = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        private const string _RegexPasswordContainsNumber = @"[0-9]+";
        private const string _RegexPasswordContainsUpper = @"[A-Z]+";
        private const string _RegexPasswordContainsLower = @"[a-z]+";

        public static bool PasswordMatchesRequirements(string value)
        {
            var hasNumber = new Regex(_RegexPasswordContainsNumber);
            var hasUpperChar = new Regex(_RegexPasswordContainsUpper);
            var hasLowerChar = new Regex(_RegexPasswordContainsLower);

            return !string.IsNullOrEmpty(value)
                && hasNumber.IsMatch(value)
                && hasUpperChar.IsMatch(value)
                && hasLowerChar.IsMatch(value);
        }

        public static bool IsMatch(string regex, string value, ValidationType validationType = ValidationType.Custom)
        {
            var isMatch = validationType switch
            {
                ValidationType.Custom when !string.IsNullOrEmpty(regex) => Regex.IsMatch(value, regex),
                ValidationType.Email => Regex.IsMatch(value, RegexEmail, RegexOptions.IgnoreCase),
                ValidationType.Password => PasswordMatchesRequirements(value),
                _ => false,
            };

            return isMatch;
        }
    }
}
