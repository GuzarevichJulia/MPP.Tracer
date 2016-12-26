using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyTracer
{
    public class XmlTraceResultFormatter : ITraceResultFormatter
    {
        private readonly string fileName;

        public XmlTraceResultFormatter(string fileName) 
        {
            this.fileName = fileName;
        }

        public void Format(TraceResult traceResult)
        {
            XDocument document = new XDocument();
            XElement root = new XElement("root");
            document.Add(root);
            int i = 0;

            foreach (ThreadInfo item in traceResult.threadsDictionary.Values)
            {
                XElement thread = new XElement("thread");
                XAttribute idAttribute = new XAttribute("id", traceResult.threadsDictionary.Keys.ElementAt(i++));
                XAttribute timeAttribute = new XAttribute("time", Math.Round(item.GetThreadTime(),3)+" ms");
                thread.Add(idAttribute, timeAttribute);
                root.Add(thread);
                Print(item.methodList, thread);
            }
            document.Save(fileName);
        }

        public void Print(List<MethodDescriptor> methods, XElement element)
        {
            foreach (MethodDescriptor method in methods)
            {
                XElement methodElement = new XElement("method");
 
                XAttribute nameAttribute = new XAttribute("name", method.methodName);
                XAttribute classAttribute = new XAttribute("class", method.className);
                XAttribute paramsAttribute = new XAttribute("params", method.paramCount.ToString());
                XAttribute timeAttribute = new XAttribute("time",(Math.Round(method.elapsedTime, 3)) + " ms");
                methodElement.Add(nameAttribute, classAttribute, paramsAttribute, timeAttribute);
                element.Add(methodElement);
                Print(method.insertedMethod, methodElement);
            }
        }
    }
}
