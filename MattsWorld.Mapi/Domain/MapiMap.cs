using System;

namespace MattsWorld.Mapi.Domain
{
    public class MapiMap
    {
        public string From { get; }
        public string Type { get; }
        public string To { get; }
        public string Meta { get; }

        public MapiMap(string from, string type, string to, string meta)
        {
            if (string.IsNullOrEmpty(from))
            {
                throw new ArgumentException("From is null", nameof(from));
            }

            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("Type is null", nameof(type));
            }

            if (string.IsNullOrEmpty(to))
            {
                throw new ArgumentException("To is null", nameof(to));
            }

            From = @from;
            Type = type;
            To = to;
            Meta = meta;
        }
    }
}