using System.Collections.Generic;
using System.Text.RegularExpressions;
using MattsWorld.Mapi.Domain;

namespace MattsWorld.Mapi.Converters
{
    public class SplitMapiConverter : IMapiConverter
    {
        public IEnumerable<KeyValuePair<string, string>> Convert(MapiMap map, Dictionary<string, string> data)
        {
            var values = new List<KeyValuePair<string, string>>();

            if (!data.ContainsKey(map.From) || map.Type.ToLower() != "split")
                return values;

            var toSplits = map.To.Split('|');


            Regex regex = new Regex(map.Meta);
            Match match = regex.Match(data[map.From]);

            if (match.Success)
            {
                for (int i = 0; i < toSplits.Length; i++)
                {
                    values.Add(new KeyValuePair<string, string>(toSplits[i], match.Groups[i+1].Value));
                }
            }

            return values;
        }
    }
}