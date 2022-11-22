using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CouponFollow.TestTask.PageObjects.DomainObjects
{
    public class Deal
    {
        private const string REGEX_USD = @"\$[0-9]*\.{0,1}[0-9]{0,2}";
        private const string REGEX_PERCENTAGE = @"[0-9]*\.{0,1}[0-9]{0,2}%";
        private static readonly Regex ValidityRegex = new($"{REGEX_USD}|{REGEX_PERCENTAGE}", RegexOptions.Compiled);

        private readonly string _text;

        internal Deal(string text)
        {
            _text = text;
        }

        public bool IsValid
        {
            get
            {
                return ValidityRegex.IsMatch(_text) || string.IsNullOrWhiteSpace(_text);
            }
        }

        public override int GetHashCode()
        {
            return _text.GetHashCode();
        }

        public override string ToString()
        {
            return _text.ToString();
        }

        public static implicit operator Deal(string s) => new(s);
    }
}
