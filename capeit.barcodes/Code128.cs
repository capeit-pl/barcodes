using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capeit.barcodes
{
    public static class Code128
    {
        private const int FUNC1 = 102;
        private const int START_A = 103;
        private const int START_B = 104;
        private const int START_C = 105;
        private const int STOP = 106;

        private const int DENOMINATOR = 103;

        private static Dictionary<int, List<int>> valuePattern = new Dictionary<int, List<int>>()
        {
            #region valuePattern
            { 0, new List<int> { 1, 1, 0, 1, 1, 0, 0, 1, 1, 0, 0 } },
            { 1, new List<int> { 1, 1, 0, 0, 1, 1, 0, 1, 1, 0, 0 } },
            { 2, new List<int> { 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0 } },
            { 3, new List<int> { 1, 0, 0, 1, 0, 0, 1, 1, 0, 0, 0 } },
            { 4, new List<int> { 1, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0 } },
            { 5, new List<int> { 1, 0, 0, 0, 1, 0, 0, 1, 1, 0, 0 } },
            { 6, new List<int> { 1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0 } },
            { 7, new List<int> { 1, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0 } },
            { 8, new List<int> { 1, 0, 0, 0, 1, 1, 0, 0, 1, 0, 0 } },
            { 9, new List<int> { 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0 } },
            { 10, new List<int> { 1, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0 } },
            { 11, new List<int> { 1, 1, 0, 0, 0, 1, 0, 0, 1, 0, 0 } },
            { 12, new List<int> { 1, 0, 1, 1, 0, 0, 1, 1, 1, 0, 0 } },
            { 13, new List<int> { 1, 0, 0, 1, 1, 0, 1, 1, 1, 0, 0 } },
            { 14, new List<int> { 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0 } },
            { 15, new List<int> { 1, 0, 1, 1, 1, 0, 0, 1, 1, 0, 0 } },
            { 16, new List<int> { 1, 0, 0, 1, 1, 1, 0, 1, 1, 0, 0 } },
            { 17, new List<int> { 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 0 } },
            { 18, new List<int> { 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 0 } },
            { 19, new List<int> { 1, 1, 0, 0, 1, 0, 1, 1, 1, 0, 0 } },
            { 20, new List<int> { 1, 1, 0, 0, 1, 0, 0, 1, 1, 1, 0 } },
            { 21, new List<int> { 1, 1, 0, 1, 1, 1, 0, 0, 1, 0, 0 } },
            { 22, new List<int> { 1, 1, 0, 0, 1, 1, 1, 0, 1, 0, 0 } },
            { 23, new List<int> { 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0 } },
            { 24, new List<int> { 1, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0 } },
            { 25, new List<int> { 1, 1, 1, 0, 0, 1, 0, 1, 1, 0, 0 } },
            { 26, new List<int> { 1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 0 } },
            { 27, new List<int> { 1, 1, 1, 0, 1, 1, 0, 0, 1, 0, 0 } },
            { 28, new List<int> { 1, 1, 1, 0, 0, 1, 1, 0, 1, 0, 0 } },
            { 29, new List<int> { 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 0 } },
            { 30, new List<int> { 1, 1, 0, 1, 1, 0, 1, 1, 0, 0, 0 } },
            { 31, new List<int> { 1, 1, 0, 1, 1, 0, 0, 0, 1, 1, 0 } },
            { 32, new List<int> { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 0 } },
            { 33, new List<int> { 1, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0 } },
            { 34, new List<int> { 1, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0 } },
            { 35, new List<int> { 1, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0 } },
            { 36, new List<int> { 1, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0 } },
            { 37, new List<int> { 1, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0 } },
            { 38, new List<int> { 1, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0 } },
            { 39, new List<int> { 1, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0 } },
            { 40, new List<int> { 1, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0 } },
            { 41, new List<int> { 1, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0 } },
            { 42, new List<int> { 1, 0, 1, 1, 0, 1, 1, 1, 0, 0, 0 } },
            { 43, new List<int> { 1, 0, 1, 1, 0, 0, 0, 1, 1, 1, 0 } },
            { 44, new List<int> { 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 0 } },
            { 45, new List<int> { 1, 0, 1, 1, 1, 0, 1, 1, 0, 0, 0 } },
            { 46, new List<int> { 1, 0, 1, 1, 1, 0, 0, 0, 1, 1, 0 } },
            { 47, new List<int> { 1, 0, 0, 0, 1, 1, 1, 0, 1, 1, 0 } },
            { 48, new List<int> { 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0 } },
            { 49, new List<int> { 1, 1, 0, 1, 0, 0, 0, 1, 1, 1, 0 } },
            { 50, new List<int> { 1, 1, 0, 0, 0, 1, 0, 1, 1, 1, 0 } },
            { 51, new List<int> { 1, 1, 0, 1, 1, 1, 0, 1, 0, 0, 0 } },
            { 52, new List<int> { 1, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0 } },
            { 53, new List<int> { 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0 } },
            { 54, new List<int> { 1, 1, 1, 0, 1, 0, 1, 1, 0, 0, 0 } },
            { 55, new List<int> { 1, 1, 1, 0, 1, 0, 0, 0, 1, 1, 0 } },
            { 56, new List<int> { 1, 1, 1, 0, 0, 0, 1, 0, 1, 1, 0 } },
            { 57, new List<int> { 1, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0 } },
            { 58, new List<int> { 1, 1, 1, 0, 1, 1, 0, 0, 0, 1, 0 } },
            { 59, new List<int> { 1, 1, 1, 0, 0, 0, 1, 1, 0, 1, 0 } },
            { 60, new List<int> { 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0 } },
            { 61, new List<int> { 1, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0 } },
            { 62, new List<int> { 1, 1, 1, 1, 0, 0, 0, 1, 0, 1, 0 } },
            { 63, new List<int> { 1, 0, 1, 0, 0, 1, 1, 0, 0, 0, 0 } },
            { 64, new List<int> { 1, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0 } },
            { 65, new List<int> { 1, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0 } },
            { 66, new List<int> { 1, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0 } },
            { 67, new List<int> { 1, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0 } },
            { 68, new List<int> { 1, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0 } },
            { 69, new List<int> { 1, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0 } },
            { 70, new List<int> { 1, 0, 1, 1, 0, 0, 0, 0, 1, 0, 0 } },
            { 71, new List<int> { 1, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0 } },
            { 72, new List<int> { 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0 } },
            { 73, new List<int> { 1, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0 } },
            { 74, new List<int> { 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0 } },
            { 75, new List<int> { 1, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0 } },
            { 76, new List<int> { 1, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0 } },
            { 77, new List<int> { 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0 } },
            { 78, new List<int> { 1, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0 } },
            { 79, new List<int> { 1, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0 } },
            { 80, new List<int> { 1, 0, 1, 0, 0, 1, 1, 1, 1, 0, 0 } },
            { 81, new List<int> { 1, 0, 0, 1, 0, 1, 1, 1, 1, 0, 0 } },
            { 82, new List<int> { 1, 0, 0, 1, 0, 0, 1, 1, 1, 1, 0 } },
            { 83, new List<int> { 1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 0 } },
            { 84, new List<int> { 1, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0 } },
            { 85, new List<int> { 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 0 } },
            { 86, new List<int> { 1, 1, 1, 1, 0, 1, 0, 0, 1, 0, 0 } },
            { 87, new List<int> { 1, 1, 1, 1, 0, 0, 1, 0, 1, 0, 0 } },
            { 88, new List<int> { 1, 1, 1, 1, 0, 0, 1, 0, 0, 1, 0 } },
            { 89, new List<int> { 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 0 } },
            { 90, new List<int> { 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0 } },
            { 91, new List<int> { 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 0 } },
            { 92, new List<int> { 1, 0, 1, 0, 1, 1, 1, 1, 0, 0, 0 } },
            { 93, new List<int> { 1, 0, 1, 0, 0, 0, 1, 1, 1, 1, 0 } },
            { 94, new List<int> { 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 0 } },
            { 95, new List<int> { 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0 } },
            { 96, new List<int> { 1, 0, 1, 1, 1, 1, 0, 0, 0, 1, 0 } },
            { 97, new List<int> { 1, 1, 1, 1, 0, 1, 0, 1, 0, 0, 0 } },
            { 98, new List<int> { 1, 1, 1, 1, 0, 1, 0, 0, 0, 1, 0 } },
            { 99, new List<int> { 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0 } },
            { 100, new List<int> { 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 0 } },
            { 101, new List<int> { 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 0 } },
            { 102, new List<int> { 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0 } },
            { 103, new List<int> { 1, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0 } },
            { 104, new List<int> { 1, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0 } },
            { 105, new List<int> { 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 0 } },
            { 106, new List<int> { 1, 1, 0, 0, 0, 1, 1, 1, 0, 1, 0, 1, 1 } }
            #endregion
        };

        private static Dictionary<char, int> typeA = new Dictionary<char, int>()
        {
            { ' ', 0 },
            { '!', 1 },
            { '"', 2 },
            { '#', 3 },
            { '$', 4 },
            { '%', 5 },
            { '&', 6 },
            { '\'', 7 },
            { '(', 8 },
            { ')', 9 },
            { '*', 10 },
            { '+', 11 },
            { ',', 12 },
            { '-', 13 },
            { '.', 14 },
            { '/', 15 },
            { '0', 16 },
            { '1', 17 },
            { '2', 18 },
            { '3', 19 },
            { '4', 20 },
            { '5', 21 },
            { '6', 22 },
            { '7', 23 },
            { '8', 24 },
            { '9', 25 },
            { ':', 26 },
            { ';', 27 },
            { '<', 28 },
            { '=', 29 },
            { '>', 30 },
            { '?', 31 },
            { '@', 32 },
            { 'A', 33 },
            { 'B', 34 },
            { 'C', 35 },
            { 'D', 36 },
            { 'E', 37 },
            { 'F', 38 },
            { 'G', 39 },
            { 'H', 40 },
            { 'I', 41 },
            { 'J', 42 },
            { 'K', 43 },
            { 'L', 44 },
            { 'M', 45 },
            { 'N', 46 },
            { 'O', 47 },
            { 'P', 48 },
            { 'Q', 49 },
            { 'R', 50 },
            { 'S', 51 },
            { 'T', 52 },
            { 'U', 53 },
            { 'V', 54 },
            { 'W', 55 },
            { 'X', 56 },
            { 'Y', 57 },
            { 'Z', 58 },
            { '[', 59 },
            { '\\', 60 },
            { ']', 61 },
            { '^', 62 },
            { '_', 63 },
        };

        private static Dictionary<char, int> typeB = new Dictionary<char, int>()
        {
            { ' ', 0 },
            { '!', 1 },
            { '"', 2 },
            { '#', 3 },
            { '$', 4 },
            { '%', 5 },
            { '&', 6 },
            { '\'', 7 },
            { '(', 8 },
            { ')', 9 },
            { '*', 10 },
            { '+', 11 },
            { ',', 12 },
            { '-', 13 },
            { '.', 14 },
            { '/', 15 },
            { '0', 16 },
            { '1', 17 },
            { '2', 18 },
            { '3', 19 },
            { '4', 20 },
            { '5', 21 },
            { '6', 22 },
            { '7', 23 },
            { '8', 24 },
            { '9', 25 },
            { ':', 26 },
            { ';', 27 },
            { '<', 28 },
            { '=', 29 },
            { '>', 30 },
            { '?', 31 },
            { '@', 32 },
            { 'A', 33 },
            { 'B', 34 },
            { 'C', 35 },
            { 'D', 36 },
            { 'E', 37 },
            { 'F', 38 },
            { 'G', 39 },
            { 'H', 40 },
            { 'I', 41 },
            { 'J', 42 },
            { 'K', 43 },
            { 'L', 44 },
            { 'M', 45 },
            { 'N', 46 },
            { 'O', 47 },
            { 'P', 48 },
            { 'Q', 49 },
            { 'R', 50 },
            { 'S', 51 },
            { 'T', 52 },
            { 'U', 53 },
            { 'V', 54 },
            { 'W', 55 },
            { 'X', 56 },
            { 'Y', 57 },
            { 'Z', 58 },
            { '[', 59 },
            { '\\', 60 },
            { ']', 61 },
            { '^', 62 },
            { '_', 63 },
            { '`', 64 },
            { 'a', 65 },
            { 'b', 66 },
            { 'c', 67 },
            { 'd', 68 },
            { 'e', 69 },
            { 'f', 70 },
            { 'g', 71 },
            { 'h', 72 },
            { 'i', 73 },
            { 'j', 74 },
            { 'k', 75 },
            { 'l', 76 },
            { 'm', 77 },
            { 'n', 78 },
            { 'o', 79 },
            { 'p', 80 },
            { 'q', 81 },
            { 'r', 82 },
            { 's', 83 },
            { 't', 84 },
            { 'u', 85 },
            { 'v', 86 },
            { 'w', 87 },
            { 'x', 88 },
            { 'y', 89 },
            { 'z', 90 },
            { '{', 91 },
            { '|', 92 },
            { '}', 93 },
            { '~', 94 },
        };

        public static List<int> EncodeTypeA(this string data) => Encode(data.ToUpper(), START_A, typeA);

        public static List<int> EncodeTypeB(this string data) => Encode(data, START_B, typeB);

        public static List<int> EncodeTypeC(this string data)
        {
            if (data.Length % 2 != 0)
                data = data.PadLeft(data.Length + 1, '0');

            List<int> result = new List<int>();
            result.AddRange(valuePattern[START_C]);

            var sum = START_C;
            var index = 1;
            for (int i = 0; i < data.Length; i += 2)
            {
                var value = Int32.Parse(data.Substring(i, 2));
                sum += index * value;

                result.AddRange(valuePattern[value]);
                index++;
            }

            var checkSum = sum % DENOMINATOR;

            result.AddRange(valuePattern[checkSum]);

            result.AddRange(valuePattern[STOP]);

            return result;
        }

        private static List<int> Encode(string data, int startValue, Dictionary<char, int> charValue)
        {
            List<int> result = new List<int>();
            result.AddRange(valuePattern[startValue]);

            var sum = startValue;
            for (int i = 0;i < data.Length;i++)
            {
                var c = data[i];
                var value = charValue[c];
                sum += (i + 1) * value;

                result.AddRange(valuePattern[value]);
            }

            var checkSum = sum % DENOMINATOR;

            result.AddRange(valuePattern[checkSum]);

            result.AddRange(valuePattern[STOP]);

            return result;
        }
    }
}
