using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyTracer
{
    class Program
    {
        public static Tracer tracer = Tracer.Instance();
        public static ConsoleTraceResultFormatter consoleFormatter = new ConsoleTraceResultFormatter();
        
        public static void Func1()
        {
            tracer.StartTrace();
            Thread.Sleep(10);
            Thread thread1 = new Thread(Func2);
            thread1.Start();
            thread1.Join();
            tracer.StopTrace();
        }

        public static void Func2()
        {
            tracer.StartTrace();
            Thread.Sleep(10);
            Func3();
            tracer.StopTrace();
        }

        public static void Func3()
        {
            tracer.StartTrace();
            Thread.Sleep(10);
            for (int i = 0; i <= 8; i++)
            {
                Func4();
            }
                tracer.StopTrace();
        }

        public static void Func5()
        {
            tracer.StartTrace();
            Thread.Sleep(10);
            for (int i = 0; i <= 3; i++)
            {
                Thread thread = new Thread(Func6);
                thread.Start();              
            }

            tracer.StopTrace();
        }

        public static void Func6()
        {
            tracer.StartTrace();
            Thread.Sleep(10);
            tracer.StopTrace();
        }

        public static void Func4()
        {
            tracer.StartTrace();
            Thread.Sleep(10);
            tracer.StopTrace();
        }

        static void Main(string[] args)
        {
            tracer.StartTrace();
            Thread.Sleep(10);
            Func5();
            Func2();
            Func4();
            Thread thread1 = new Thread(Func1);
            thread1.Start();
            thread1.Join();
            Thread thread2 = new Thread(Func1);
            thread2.Start();
            thread2.Join();
            tracer.StopTrace();
            consoleFormatter.Format(tracer.GetTraceResult());
            var xmlFormatter = new XmlTraceResultFormatter("document.xml");
            xmlFormatter.Format(tracer.GetTraceResult());
            var xmlSerializer = new XmlSerializaton("xmldoc.xml");
            xmlSerializer.Serialize(tracer.GetTraceResult());
            Console.Read();
        }
    }
}
