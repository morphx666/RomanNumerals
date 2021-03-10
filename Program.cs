using System;

// Bash test script:
// for i in {1..100}; do echo "$i - $(./Roman $i)"; done

namespace Roman {
    public class Program {
        private static void Main(string[] args) {
            try {
                if(args.Length != 1) throw new ArgumentException("An Integer or Roman Numeral is required as an argument");

                if(int.TryParse(args[0], out int n)) {
                    Console.WriteLine(RomanNumerals.ToRomanNumeral(n));
                } else {
                    Console.WriteLine(RomanNumerals.FromRomanNumeral(args[0].ToUpper()));
                }
            } catch(Exception ex) {
                Console.WriteLine($"{ex.GetType().ToString().Split('.')[1]}: {ex.Message}");
            }
        }
    }
}