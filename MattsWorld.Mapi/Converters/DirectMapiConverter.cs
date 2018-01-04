using System.Collections.Generic;
using MattsWorld.Mapi.Domain;

namespace MattsWorld.Mapi.Converters
{
    public class DirectMapiConverter : IMapiConverter
    {
        public IEnumerable<KeyValuePair<string, string>> Convert(MapiMap map, Dictionary<string, string> data)
        {
            if (data.ContainsKey(map.From))
                yield return new KeyValuePair<string, string>(map.To, data[map.From]);
        }
    }
}