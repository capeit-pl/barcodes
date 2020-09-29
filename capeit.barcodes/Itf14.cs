using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace capeit.barcodes
{
    public static class Itf14
    {
        #region Metadata
        private static List<int> start = new List<int>() { 1, 0, 1, 0 };
        private static List<int> stop = new List<int>() { 1, 1, 0, 1 };

        private class LineDef
        {
            public List<int> Line { get; set; }
        }

        private static LineDef oddN = new LineDef() { Line = new List<int>() { 1 } };
        private static LineDef oddW = new LineDef() { Line = new List<int>() { 1, 1 } };

        private static Dictionary<int, List<LineDef>> oddValues = new Dictionary<int, List<LineDef>>()
        {
            {0, new List<LineDef>() { oddN, oddN, oddW, oddW, oddN } },
            {1, new List<LineDef>() { oddW, oddN, oddN, oddN, oddW } },
            {2, new List<LineDef>() { oddN, oddW, oddN, oddN, oddW } },
            {3, new List<LineDef>() { oddW, oddW, oddN, oddN, oddN } },
            {4, new List<LineDef>() { oddN, oddN, oddW, oddN, oddW } },
            {5, new List<LineDef>() { oddW, oddN, oddW, oddN, oddN } },
            {6, new List<LineDef>() { oddN, oddW, oddW, oddN, oddN } },
            {7, new List<LineDef>() { oddN, oddN, oddN, oddW, oddW } },
            {8, new List<LineDef>() { oddW, oddN, oddN, oddW, oddN } },
            {9, new List<LineDef>() { oddN, oddW, oddN, oddW, oddN } },

        };

        private static LineDef evenN = new LineDef() { Line = new List<int>() { 0 } };
        private static LineDef evenW = new LineDef() { Line = new List<int>() { 0, 0 } };

        private static Dictionary<int, List<LineDef>> evenValues = new Dictionary<int, List<LineDef>>()
        {
            {0, new List<LineDef>() { evenN, evenN, evenW, evenW, evenN } },
            {1, new List<LineDef>() { evenW, evenN, evenN, evenN, evenW } },
            {2, new List<LineDef>() { evenN, evenW, evenN, evenN, evenW } },
            {3, new List<LineDef>() { evenW, evenW, evenN, evenN, evenN } },
            {4, new List<LineDef>() { evenN, evenN, evenW, evenN, evenW } },
            {5, new List<LineDef>() { evenW, evenN, evenW, evenN, evenN } },
            {6, new List<LineDef>() { evenN, evenW, evenW, evenN, evenN } },
            {7, new List<LineDef>() { evenN, evenN, evenN, evenW, evenW } },
            {8, new List<LineDef>() { evenW, evenN, evenN, evenW, evenN } },
            {9, new List<LineDef>() { evenN, evenW, evenN, evenW, evenN } },
        };
        #endregion

        private static List<int> GetChecksum(List<int> data)
        {
            List<int> result = new List<int>(data);
            if (result.Count % 2 == 0)
                result.Insert(0, 0);

            var oddSum = 0;
            var evenSum = 0;

            for (int i = 0; i < result.Count; i++)
            {
                if (i % 2 == 0) //even
                {
                    evenSum += result[i];
                }
                else
                    oddSum += result[i];
            }

            var sum = oddSum + evenSum * 3;
            var mod = sum % 10;
            var checkSum = (10 - mod) % 10;

            result.Add(checkSum);

            return result;

        }

        public static List<int> Itf14Encode(this string data) => data.Itf14Encode(true);

        public static List<int> Itf14Encode(this string data, bool addCheckSum)
        {
            var dNum = data.Select(d => d - 48).ToList();

            var result = new List<int>();

            var forEncoding = addCheckSum ? GetChecksum(dNum) : dNum;

            result.AddRange(start);
            for (int i = 0; i < forEncoding.Count; i += 2)
            {
                for (var col = 0; col < 5; col++)
                {
                    var oddDigit = forEncoding[i];
                    var evenDigit = forEncoding[i + 1];
                    result.AddRange(oddValues[oddDigit][col].Line); //odd digit
                    result.AddRange(evenValues[evenDigit][col].Line); //even digit

                }
            }

            result.AddRange(stop);

            return result;
        }
    }
}
