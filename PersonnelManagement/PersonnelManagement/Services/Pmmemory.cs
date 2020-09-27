using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;

namespace PersonnelManagement.Services
{
    public class Pmmemory
    {
        public static ConcurrentDictionary<int, byte[]> tempFiles = new ConcurrentDictionary<int, byte[]>();
    }
}
