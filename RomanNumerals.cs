using System;
using System.Text;

namespace Roman {
    public static class RomanNumerals {
        private const string validRomanSymbols = "IVXLCDM";
        private static readonly (string RomanNumeral, int DecimalValue)[] romanNumeralMappings = [
            ("M", 1000),
            ("CM", 900),
            ("D", 500),
            ("CD", 400),
            ("C", 100),
            ("XC", 90),
            ("L", 50),
            ("XL", 40),
            ("X", 10),
            ("IX", 9),
            ("V", 5),
            ("IV", 4),
            ("I", 1)
        ];

        public static string ToRomanNumeral(int number) {
            if(number == 0) return "N";
            if(number < 0 || number > 3999) throw new ArgumentOutOfRangeException(nameof(number), $"Integer value '{number}' is out of range");

            StringBuilder result = new();
            for(int i = 0; i < romanNumeralMappings.Length; i++) {
                while(number >= romanNumeralMappings[i].DecimalValue) {
                    number -= romanNumeralMappings[i].DecimalValue;
                    result.Append(romanNumeralMappings[i].RomanNumeral);
                }
            }
            return result.ToString();
        }

        public static int FromRomanNumeral(string romanNumeral, bool strict = false) {
            if(romanNumeral == "N") return 0;
            int result = 0;
            int lastVal = int.MaxValue;
            int cCount = 0;

            Action<string, int> throwException = (string crn, int p) => {
                crn = $"{crn[..p]}[{crn[p]}]{crn[(p + 1)..]}";
                crn = $"Invalid Roman Numeral '{crn}' at position {p + 1}";
                throw new ArgumentException(crn);
            };

            for(int i = 0; i < romanNumeral.Length;) {
                if(!validRomanSymbols.Contains(romanNumeral[i])) throwException(romanNumeral, i);
                int remainingLength = romanNumeral.Length - i;
                for(int j = 0; j < romanNumeralMappings.Length; j++) {
                    int romanNumeralLength = romanNumeralMappings[j].RomanNumeral.Length;
                    if(romanNumeralMappings[j].RomanNumeral == romanNumeral.Substring(i, Math.Min(remainingLength, romanNumeralLength))) {
                        int value = romanNumeralMappings[j].DecimalValue;
                        if(lastVal < value) throwException(romanNumeral, i);
                        if(lastVal == value) {
                            if(++cCount == 3) throwException(romanNumeral, i);
                        } else {
                            cCount = 0;
                            lastVal = value;
                        }
                        result += value;
                        i += romanNumeralLength;
                        break;
                    }
                }
            }
            if(strict && romanNumeral != ToRomanNumeral(result)) throwException(romanNumeral, 0);
            return result;
        }
    }
}