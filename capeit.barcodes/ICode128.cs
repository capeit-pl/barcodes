using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace capeit.barcodes
{
    public interface ICode128
    {
        List<int> EncodeTypeA(string data);
        List<int> EncodeTypeB(string data);
        List<int> EncodeTypeC(string data);
    }
}
