using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MattsWorld.Mapi.Data
{
    public interface IRepository
    {
        Task Save<T>(T entity) where T : IEntity;
        Task<T> GetById<T>(Guid id) where T : IEntity;
        Task<IEnumerable<T>> List<T>() where T : IEntity;
    }
}