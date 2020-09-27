using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace capeit.barcodes
{
    public static class Ean13
    {
        #region Metadata
        private static List<int> START_STOP = new List<int>() { 1, 0, 1 };
        private static List<int> MIDDLE = new List<int>() { 0, 1, 0, 1, 0 };

        private static Dictionary<int, List<int>> AType = new Dictionary<int, List<int>>()
        {
            {0, new List<int> { 0, 0, 0, 1, 1, 0, 1 } },
            {1, new List<int> { 0, 0, 1, 1, 0, 0, 1 } },
            {2, new List<int> { 0, 0, 1, 0, 0, 1, 1 } },
            {3, new List<int> { 0, 1, 1, 1, 1, 0, 1 } },
            {4, new List<int> { 0, 1, 0, 0, 0, 1, 1 } },
            {5, new List<int> { 0, 1, 1, 0, 0, 0, 1 } },
            {6, new List<int> { 0, 1, 0, 1, 1, 1, 1 } },
            {7, new List<int> { 0, 1, 1, 1, 0, 1, 1 } },
            {8, new List<int> { 0, 1, 1, 0, 1, 1, 1 } },
            {9, new List<int> { 0, 0, 0, 1, 0, 1, 1 } },
        };

        private static Dictionary<int, List<int>> BType = new Dictionary<int, List<int>>()
        {
            {0, new List<int> { 0, 1, 0, 0, 1, 1, 1 } },
            {1, new List<int> { 0, 1, 1, 0, 0, 1, 1 } },
            {2, new List<int> { 0, 0, 1, 1, 0, 1, 1 } },
            {3, new List<int> { 0, 1, 0, 0, 0, 0, 1 } },
            {4, new List<int> { 0, 0, 1, 1, 1, 0, 1 } },
            {5, new List<int> { 0, 1, 1, 1, 0, 0, 1 } },
            {6, new List<int> { 0, 0, 0, 0, 1, 0, 1 } },
            {7, new List<int> { 0, 0, 1, 0, 0, 0, 1 } },
            {8, new List<int> { 0, 0, 0, 1, 0, 0, 1 } },
            {9, new List<int> { 0, 0, 1, 0, 1, 1, 1 } },
        };

        private static Dictionary<int, List<int>> CType = new Dictionary<int, List<int>>()
        {
            {0, new List<int> { 1, 1, 1, 0, 0, 1, 0 } },
            {1, new List<int> { 1, 1, 0, 0, 1, 1, 0 } },
            {2, new List<int> { 1, 1, 0, 1, 1, 0, 0 } },
            {3, new List<int> { 1, 0, 0, 0, 0, 1, 0 } },
            {4, new List<int> { 1, 0, 1, 1, 1, 0, 0 } },
            {5, new List<int> { 1, 0, 0, 1, 1, 1, 0 } },
            {6, new List<int> { 1, 0, 1, 0, 0, 0, 0 } },
            {7, new List<int> { 1, 0, 0, 0, 1, 0, 0 } },
            {8, new List<int> { 1, 0, 0, 1, 0, 0, 0 } },
            {9, new List<int> { 1, 1, 1, 0, 1, 0, 0 } },
        };

        private static Dictionary<int, List<Dictionary<int, List<int>>>> CodeSet = new Dictionary<int, List<Dictionary<int, List<int>>>>()
        {
            {0,  new List<Dictionary<int, List<int>>> { AType, AType, AType, AType, AType, AType } },
            {1,  new List<Dictionary<int, List<int>>> { AType, AType, BType, AType, BType, BType } },
            {2,  new List<Dictionary<int, List<int>>> { AType, AType, BType, BType, AType, BType } },
            {3,  new List<Dictionary<int, List<int>>> { AType, AType, BType, BType, BType, AType } },
            {4,  new List<Dictionary<int, List<int>>> { AType, BType, AType, AType, BType, BType } },
            {5,  new List<Dictionary<int, List<int>>> { AType, BType, BType, AType, AType, BType } },
            {6,  new List<Dictionary<int, List<int>>> { AType, BType, BType, AType, AType, AType } },
            {7,  new List<Dictionary<int, List<int>>> { AType, BType, AType, BType, AType, BType } },
            {8,  new List<Dictionary<int, List<int>>> { AType, BType, AType, BType, BType, AType } },
            {9,  new List<Dictionary<int, List<int>>> { AType, BType, BType, AType, BType, AType } },
        };
        #endregion

        public static string EncodedData { get; private set; }
        public static List<int> Ean13Encode(this string data)
        {
            if (data.Count() != 12 && data.Count() != 13)
                throw new ArgumentException("String must contain 12 or 13 signs");

            var dNum = data.Select(d => d - 48).ToList();

            if (data.Count() == 12)
                AddCheckSum(dNum);

            EncodedData = string.Join("", dNum);

            var result = new List<int>();
            result.AddRange(START_STOP);

            var codepage = CodeSet[dNum[0]];
            for (int i = 1; i < 7; i++)
            {
                var ABType = codepage[i - 1];
                var vale = dNum[i];
                result.AddRange(ABType[vale]);
            }

            result.AddRange(MIDDLE);

            for (int i = 7; i < dNum.Count; i++)
            {
                var vale = dNum[i];
                result.AddRange(CType[vale]);
            }

            result.AddRange(START_STOP);

            return result;
        }

        private static void AddCheckSum(List<int> dNum)
        {
            var sum = 0;
            for (int i = 0; i < dNum.Count; i++)
            {
                sum += i % 2 == 0 ? 3 * dNum[i] : dNum[i];
            }

            dNum.Add(10 - sum % 10);
        }
    }

}
