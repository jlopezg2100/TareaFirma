using System;
using System.Collections.Generic;
using System.Text;

namespace Firma
{
    public interface IStorage
    {
        string SaveImage(byte[] bytedata);

    }
}
