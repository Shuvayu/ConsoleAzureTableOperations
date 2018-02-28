using ConsoleTableStorage.Models.LogicModels;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Linq;

namespace ConsoleTableStorage.Services
{
    public class TableStorageService : ITableStorageService
    {
        private string _connectionString;
        private CloudStorageAccount _storageAccount;
        private CloudTableClient _tableClient;

        public TableStorageService(string connectionString)
        {
            _connectionString = connectionString;
            _storageAccount = CloudStorageAccount.Parse(_connectionString);
            _tableClient = _storageAccount.CreateCloudTableClient();
        }

        public void CreateTableStorageTable(string tableName)
        {
            CloudTable table = _tableClient.GetTableReference(tableName);
            table.CreateIfNotExistsAsync().GetAwaiter().GetResult();
        }

        public void InsertIntoTableStorage(string tableName, CarEntityModel car)
        {
            CloudTable table = _tableClient.GetTableReference(tableName);
            TableOperation insert = TableOperation.Insert(car);
            table.ExecuteAsync(insert).GetAwaiter().GetResult();
        }

        public void RetrieveAllFromTableStorage(string tableName)
        {
            TableQuery<CarEntityModel> carQuery = new TableQuery<CarEntityModel>(); // Can add predicate here
            CloudTable table = _tableClient.GetTableReference(tableName);
            TableContinuationToken token = null;
            var result = (table.ExecuteQuerySegmentedAsync(carQuery, token).GetAwaiter().GetResult()).ToList();
            foreach (var car in result)
            {
                Console.WriteLine("{0} {1} {2} {3}", car.Year, car.Make, car.Model, car.Color);
            }
        }

        public void RetrieveFromTableStorage(string tableName, string rowId, string partitionKey)
        {
            CloudTable table = _tableClient.GetTableReference(tableName);
            TableOperation retrieve = TableOperation.Retrieve<CarEntityModel>(partitionKey, rowId);
            TableResult result = table.ExecuteAsync(retrieve).GetAwaiter().GetResult();
            if (result.Result == null)
            {
                Console.WriteLine("Not Found.");
            }
            else
            {
                var resultModel = (CarEntityModel)result.Result;
                Console.WriteLine("Found the Car of make {0} and model {1}", resultModel.Make, resultModel.Model);
            }
        }
    }
}
