using System.Collections.Generic;
using System.Linq;
using MattsWorld.Mapi.Converters;
using MattsWorld.Mapi.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MattsWorld.Mapi.Tests
{
    [TestClass]
    public class SplitMapiConverterTests
    {
        [TestMethod]
        public void JustSomeRandomTest()
        {
            var converter = new SplitMapiConverter();
            
            MapiMap map = new MapiMap("a","split", "b|c", @"([\d|A-Z]*) (([A-Z|a-z| ]*) ([A-Z|a-z]*))");
            Dictionary<string, string> from = new Dictionary<string, string>
            {
                {"trash", "more trash" },
                {"a", "123 Main Street"},
                {"blah", "blahdeblah" }
            };


            IEnumerable<KeyValuePair<string, string>> result = converter.Convert(map, from);

            Assert.AreEqual(1, result.Count());
        }
    }
}
