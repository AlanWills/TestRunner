using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestRunner.Extensions
{
    public static class Extensions
    {
        public static void SkipWhiteSpace(this XmlReader reader)
        {
            while (reader.NodeType == XmlNodeType.Whitespace)
            {
                reader.Read();
            }
        }
    }
}
