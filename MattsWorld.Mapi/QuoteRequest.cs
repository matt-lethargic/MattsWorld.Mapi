using System.Collections.Generic;

namespace MattsWorld.Mapi
{
    public class QuoteRequest
    {
        public QuoteRequest()
        {
            Data = new Dictionary<string, string>();
        }

        public Dictionary<string, string> Data { get; set; }
    }
}
