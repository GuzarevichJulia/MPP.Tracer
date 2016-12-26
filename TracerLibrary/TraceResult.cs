using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace MyTracer
{
    public class TraceResult
    {
        public ConcurrentDictionary<int, ThreadInfo> threadsDictionary = new ConcurrentDictionary<int, ThreadInfo>();
    }
}
