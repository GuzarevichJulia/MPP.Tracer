using MyTracer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyTracer
{
    public class XmlSerializaton
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(MethodDescriptor));
        System.IO.StreamWriter file;

        public XmlSerializaton(string fileName)
        {            
            this.file = new System.IO.StreamWriter(fileName);
        }

        public void Serialize(TraceResult traceResult)
        {
            foreach (ThreadInfo item in traceResult.threadsDictionary.Values)
            {
                SerializeList(item.methodList);
            }
            file.Close();
        }

        private void SerializeList(List<MethodDescriptor> methodList)
        {
            foreach (MethodDescriptor method in methodList)
            {
                xmlSerializer.Serialize(file, method);
                SerializeList(method.insertedMethod);
            }
        }
    }
}
