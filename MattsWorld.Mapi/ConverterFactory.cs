using MattsWorld.Mapi.Converters;
using MattsWorld.Mapi.Domain;

namespace MattsWorld.Mapi
{
    public static class ConverterFactory
    {
        public static IMapiConverter GetConverter(MapiMap map)
        {
            switch (map.Type.ToLower())
            {
                case "direct":
                    return new DirectMapiConverter();
                case "split":
                    return new SplitMapiConverter();
                default:
                        return null;
            }
        }
    }
}
