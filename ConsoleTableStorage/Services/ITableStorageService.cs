using ConsoleTableStorage.Models.LogicModels;

namespace ConsoleTableStorage.Services
{
    public interface ITableStorageService
    {
        void CreateTableStorageTable(string tableName);
        void InsertIntoTableStorage(string tableName, CarEntityModel car);
        void RetrieveFromTableStorage(string tableName, string rowId, string partitionKey);
        void RetrieveAllFromTableStorage(string tableName);
    }
}
