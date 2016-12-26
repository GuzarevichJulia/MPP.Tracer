using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTracer
{
    public class ConsoleTraceResultFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            int i = 0;
            ICollection<int> keys = traceResult.threadsDictionary.Keys;
            foreach (ThreadInfo item in traceResult.threadsDictionary.Values)
            {
                Console.WriteLine("ThreadId: {0}  time = {1}:", keys.ElementAt(i++), Math.Round(item.GetThreadTime(), 3) + " ms");
                Print(item.methodList, string.Empty);
            }
        }

        public void Print(List<MethodDescriptor> methodList, string space)
        {
            space = space + "   ";
            foreach (MethodDescriptor method in methodList)
            {
                Console.WriteLine("{0}Method:{1}", space, method.methodName);
                Console.WriteLine("{0}Class:{1}", space, method.className);
                Console.WriteLine("{0}Count of param:{1}", space, method.paramCount);
                Console.WriteLine("{0}Elapsed time:{1}", space, Math.Round(method.elapsedTime, 3) + " ms");
                Console.WriteLine();
                Print(method.insertedMethod, space);
            }
        }
    }
}
