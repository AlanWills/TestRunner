using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExtensions
{
    public static class Attributes
    {
        /// <summary>
        /// An attribute we can mark our unit test with to use when constructing an object in the TestInitialize function.
        /// By checking to see if the test has this attribute we can see whether we should construct the object or not.
        /// </summary>
        public class DontAutoConstructAttribute : Attribute { }
    }
}
