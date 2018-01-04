using System.Collections.Generic;
using MattsWorld.Mapi.Domain;

namespace MattsWorld.Mapi.Converters
{
    public interface IMapiConverter
    {
        IEnumerable<KeyValuePair<string, string>> Convert(MapiMap map, Dictionary<string, string> data);
    }
}