using System.Globalization;
using System.Linq;
using System.Text;

namespace Workforce.FixedWidthExtractor
{
    public static class StringExtensions
    {
        public static string RemoveNonAlphanumericCharacters(this string s)
        {
            var alphanumericCategories = new UnicodeCategory[]
            {
                UnicodeCategory.LowercaseLetter,
                UnicodeCategory.UppercaseLetter,
                UnicodeCategory.DecimalDigitNumber,
            };

            return new string(s.Where(c => alphanumericCategories.Contains(CharUnicodeInfo.GetUnicodeCategory(c))).ToArray());
        }

        public static string RemoveDiacritics(this string s)
        {
            var stringBuilder = new StringBuilder();

            foreach (var c in s.Normalize(NormalizationForm.FormD))
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string ToPascalCase(this string s)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLowerInvariant());
        }
    }
}
