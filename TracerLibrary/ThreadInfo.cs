using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTracer
{
    public class ThreadInfo
    {
        public Stack<MethodDescriptor> methodStack = new Stack<MethodDescriptor>();
        public List<MethodDescriptor> methodList= new List<MethodDescriptor>();
        public int time;

        public void PushMethod(MethodDescriptor item)
        {
            if (methodStack.Count > 0)
            {
                MethodDescriptor previousMethod = methodStack.Peek();
                methodStack.Push(item);
                previousMethod.insertedMethod.Add(methodStack.Peek());
            }
            else
            {
                methodStack.Push(item);
                methodList.Add(methodStack.Peek());
            }
        }

        public void PopMethod(DateTime endTime)
        {
            methodStack.Peek().elapsedTime = endTime.Subtract(methodStack.Peek().startTime).TotalMilliseconds;
            methodStack.Pop();
        }

        public double GetThreadTime()
        {
            double result = 0;
            foreach (MethodDescriptor method in this.methodList)
            {
                result = result + method.elapsedTime;
            }
            return result;
        }
    }
}
