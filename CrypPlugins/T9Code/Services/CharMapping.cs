﻿using System.Collections.Generic;
using System.Text;

namespace CrypTool.T9Code.Services
{
    public static class CharMapping
    {
        private static readonly Dictionary<string, string[]> DigitToCharMapping = new Dictionary<string, string[]>
        {
            { "2", new[] { "a", "ä", "b", "c" } },
            { "3", new[] { "d", "e", "f" } },
            { "4", new[] { "g", "h", "i" } },
            { "5", new[] { "j", "k", "l" } },
            { "6", new[] { "m", "n", "o", "ö" } },
            { "7", new[] { "p", "q", "r", "s", "ß" } },
            { "8", new[] { "t", "u", "ü", "v" } },
            { "9", new[] { "w", "x", "y", "z" } },
            { "0", new[] { " " } }
        };

        private static Dictionary<string, string> _charToDigitMapping;

        private static readonly StringBuilder StringBuilder = new StringBuilder();

        private static Dictionary<string, string> CharToDigitMapping =>
            _charToDigitMapping ?? (_charToDigitMapping = GenerateCharToDigitMapping());

        private static Dictionary<string, string> GenerateCharToDigitMapping()
        {
            var res = new Dictionary<string, string>();
            foreach (var keyValuePair in DigitToCharMapping)
            {
                foreach (var value in keyValuePair.Value)
                {
                    res[value] = keyValuePair.Key;
                }
            }

            return res;
        }

        public static string StringToDigit(string letters)
        {
            StringBuilder.Clear();
            var lowerLetters = letters.ToLower();
            foreach (var c in lowerLetters)
            {
                if (CharToDigitMapping.TryGetValue(c.ToString(), out var value))
                {
                    StringBuilder.Append(value);
                }
            }

            return StringBuilder.ToString();
        }
    }
}