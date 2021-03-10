using System;
using System.Text;

namespace Roman {
    public static class RomanNumerals {
        private const string validRomNums = "IVXLCDM";
        private static readonly (string Numeral, int Value)[] rn = {
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
        };

        public static string ToRomanNumeral(int intArg) {
            if(intArg == 0) return "N";
            if(intArg < 0 || intArg > 3999) throw new ArgumentOutOfRangeException(nameof(intArg), $"Integer value '{intArg}' is out of range");

            StringBuilder result = new();
            for(int i = 0; i < rn.Length; i++) {
                while(intArg >= rn[i].Value) {
                    intArg -= rn[i].Value;
                    result.Append(rn[i].Numeral);
                }
            }
            return result.ToString();
        }

        public static int FromRomanNumeral(string romNum, bool strict = false) {
            if(romNum == "N") return 0;
            int result = 0;
            int lastVal = int.MaxValue;
            int cCount = 0;

            Action<string, int> throwException = (string crn, int p) => {
                crn = $"{crn[..p]}[{crn[p]}]{crn[(p + 1)..]}";
                crn = $"Invalid Roman Numeral '{crn}' at position {p + 1}";
                throw new ArgumentException(crn);
            };

            for(int i = 0; i < romNum.Length;) {
                if(!validRomNums.Contains(romNum[i])) throwException(romNum, i);
                int nlen = romNum.Length - i;
                for(int j = 0; j < rn.Length; j++) {
                    int rnlen = rn[j].Numeral.Length;
                    if(rn[j].Numeral == romNum.Substring(i, Math.Min(nlen, rnlen))) {
                        int value = rn[j].Value;
                        if(lastVal < value) throwException(romNum, i);
                        if(lastVal == value) {
                            if(++cCount == 3) throwException(romNum, i);
                        } else {
                            cCount = 0;
                            lastVal = value;
                        }
                        result += value;
                        i += rnlen;
                        break;
                    }
                }
            }
            if(strict && romNum != ToRomanNumeral(result)) throwException(romNum, 0);
            return result;
        }
    }
}