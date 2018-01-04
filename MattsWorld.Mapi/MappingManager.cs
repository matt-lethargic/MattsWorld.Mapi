using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MattsWorld.Mapi.Converters;
using MattsWorld.Mapi.Data;
using MattsWorld.Mapi.Domain;

namespace MattsWorld.Mapi
{
    public class MappingManager
    {
        private readonly IRepository _repository;


        public MappingManager(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        public async Task Save(MapiMapping mapiMapping)
        {
            await _repository.Save(mapiMapping);
        }

        public async Task<QuoteRequest> Process(QuoteRequest request)
        {
            var outRequest = new QuoteRequest();

            var mapping = await _repository.GetById<MapiMapping>(new Guid("d26922a0-a437-4e2b-8c52-b429e828e844"));

            foreach (MapiMap map in mapping.Maps)
            {
                IMapiConverter converter = ConverterFactory.GetConverter(map);
                var outData = converter.Convert(map, request.Data);
                foreach (KeyValuePair<string, string> keyValuePair in outData)
                {
                    outRequest.Data.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }

            return outRequest;
        }
    }
}
