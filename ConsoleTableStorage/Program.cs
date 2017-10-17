using ConsoleTableStorage.Models;
using ConsoleTableStorage.Models.LogicModels;
using ConsoleTableStorage.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ConsoleTableStorage
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            var azureSettings = new AzureConfig();
            Configuration.GetSection("Azure").Bind(azureSettings);

            var TestResourceSettings = new TestResourceConfig();
            Configuration.GetSection("TestResources").Bind(TestResourceSettings);

            ITableStorageService tableService = new TableStorageService(azureSettings.StorageAccountConnectionString);

            tableService.CreateTableStorageTable(TestResourceSettings.TableName);

            Console.WriteLine("Table Created !!!");

            var id = (int)DateTime.Now.Ticks; // some random id
            var random = new Random();

            var newCar = new CarEntityModel(id, 2016, "Audi", "X5", "White" );
            var newCar1 = new CarEntityModel(random.Next(1,1000), 2005, "Honda", "Civic", "Blue" );
            var newCar2 = new CarEntityModel(random.Next(1, 1000), 2013, "BMW", "X1", "Silver" );
            var newCar3 = new CarEntityModel(random.Next(1, 1000), 2000, "Tesla", "S3", "White" );
            var newCar4 = new CarEntityModel(random.Next(1, 1000), 2002, "Mazda", "RX7", "Red" );

            tableService.InsertIntoTableStorage(TestResourceSettings.TableName, newCar);
            tableService.InsertIntoTableStorage(TestResourceSettings.TableName, newCar1);
            tableService.InsertIntoTableStorage(TestResourceSettings.TableName, newCar2);
            tableService.InsertIntoTableStorage(TestResourceSettings.TableName, newCar3);
            tableService.InsertIntoTableStorage(TestResourceSettings.TableName, newCar4);

            Console.WriteLine("New cars inserted !!!");
            tableService.RetrieveFromTableStorage(TestResourceSettings.TableName, id.ToString(), newCar.PartitionKey);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Get All...");
            tableService.RetrieveAllFromTableStorage(TestResourceSettings.TableName);
            Console.ReadKey();
        }
    }
}
