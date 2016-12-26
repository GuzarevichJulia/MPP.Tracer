using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyTracer
{
    public class MethodDescriptor
    {
        public List<MethodDescriptor> insertedMethod = new List<MethodDescriptor>();

        [XmlAttribute]
        public int threadId { get; set; }
        [XmlAttribute]
        public string methodName{get; set;}
        [XmlAttribute]
        public string className { get; set; }
        [XmlAttribute]
        public DateTime startTime { get; set; }
        [XmlAttribute]
        public double elapsedTime { get; set; }
        [XmlAttribute]
        public int paramCount { get; set; }
    }
}
