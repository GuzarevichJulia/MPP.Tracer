using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Collections.Concurrent;

namespace MyTracer
{
    public class Tracer : ITracer
    {
        private static volatile Tracer instance;
        private static readonly Object syncRoot = new Object();
        
        private ConcurrentDictionary<int, ThreadInfo> threadsDictionary = new ConcurrentDictionary<int,ThreadInfo>();

        public static Tracer Instance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new Tracer();
                    }
                }
            }
            return instance;
        }

        public void StartTrace()
        {
            DateTime startTime = DateTime.UtcNow;
            int i = 1;
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(i);
            MethodInfo methodInfo = (MethodInfo)stackFrame.GetMethod();
            MethodDescriptor methodDescriptor = new MethodDescriptor
            {
                className = methodInfo.DeclaringType.Name,
                methodName = methodInfo.Name,
                paramCount = methodInfo.GetParameters().Length,
                threadId = Thread.CurrentThread.ManagedThreadId,
                startTime = startTime
            };
            ThreadInfo threadInfo = new ThreadInfo();
            threadsDictionary.GetOrAdd(methodDescriptor.threadId, threadInfo);
            threadInfo = threadsDictionary[methodDescriptor.threadId];
            threadInfo.PushMethod(methodDescriptor);
       }

        public void StopTrace()
        {
            DateTime endTime = DateTime.UtcNow;
            threadsDictionary[Thread.CurrentThread.ManagedThreadId].PopMethod(endTime);
        }

        public TraceResult GetTraceResult()
        {
            TraceResult traceResult = new TraceResult();
            traceResult.threadsDictionary = threadsDictionary;
            return traceResult;
        }        
    }
}
