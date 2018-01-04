using System;
using System.Collections.Generic;
using MattsWorld.Mapi.Data;

namespace MattsWorld.Mapi.Domain
{
    public class MapiMapping : IEntity
    {
        public Guid Id { get; private set; }
        
        public List<MapiMap> Maps { get; private set; }

        public MapiMapping()
        {
            Id = Guid.NewGuid();
        }

        public MapiMapping(Guid id)
        {
            Id = id;
        }
        
        public MapiMapping(List<MapiMap> maps)
        {
            Id = Guid.NewGuid();
            Maps = maps;
        }

        public MapiMapping(Guid id, List<MapiMap> maps)
        {
            Id = id;
            Maps = maps;
        }

        public void AddMap(MapiMap map)
        {
            if(Maps == null) Maps = new List<MapiMap>();

            Maps.Add(map);
        }
    }
}
