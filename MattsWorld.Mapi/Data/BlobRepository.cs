using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MattsWorld.Mapi.Data.DataConverters;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace MattsWorld.Mapi.Data
{
    public class BlobRepository : IRepository
    {
        private string _connectionString = "UseDevelopmentStorage=true;";
        private string _containerName = "mapi";
        private readonly CloudBlobContainer _container;
        private readonly JsonSerializerSettings _jsonSettings;

        public BlobRepository()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            _container = blobClient.GetContainerReference(_containerName);
            _container.CreateIfNotExistsAsync().Wait();

            _jsonSettings =
                new JsonSerializerSettings {Converters = new List<JsonConverter> {new MapiMappingDataConverter()}};
        }


        public async Task Save<T>(T entity) where T : IEntity
        {
            string typeName = entity.GetType().Name;
            string content = JsonConvert.SerializeObject(entity);

            CloudBlockBlob blockBlob = _container.GetBlockBlobReference($"{typeName}/{entity.Id}");

            await blockBlob.UploadTextAsync(content);
        }

        public async Task<T> GetById<T>(Guid id) where T : IEntity
        {
            string typeName = typeof(T).Name;

            CloudBlockBlob blockBlob = _container.GetBlockBlobReference($"{typeName}/{id}");

            string content = await blockBlob.DownloadTextAsync();

            T entity = JsonConvert.DeserializeObject<T>(content, _jsonSettings);
            return entity;
        }

        public async Task<IEnumerable<T>> List<T>() where T : IEntity
        {
            List<T> entities = new List<T>();

            var blobs = await _container.ListBlobsSegmentedAsync(typeof(T).Name, null);
            
            foreach (IListBlobItem item in blobs.Results)
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = item as CloudBlockBlob;
                    string content = await blob.DownloadTextAsync();
                    T entity = JsonConvert.DeserializeObject<T>(content, _jsonSettings);
                    entities.Add(entity);
                }
            }

            return entities;
        }
    }
}