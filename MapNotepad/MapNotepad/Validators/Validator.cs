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

        public static bool IsMatch(string regex, string value, ValidationType regexType = ValidationType.Custom)
        {
            bool isMatch;

            switch (regexType)
            {
                case ValidationType.Custom when !string.IsNullOrEmpty(regex):
                    isMatch = Regex.IsMatch(value, regex);
                    break;
                case ValidationType.Email:
                    isMatch = Regex.IsMatch(value, RegexEmail);
                    break;
                case ValidationType.Password:
                    isMatch = PasswordMatchesRequirements(value);
                    break;
                default:
                    isMatch = false;
                    break;
            }
            return isMatch;
        }
    }
}
