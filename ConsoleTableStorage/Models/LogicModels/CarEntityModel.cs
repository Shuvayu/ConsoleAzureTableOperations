using Microsoft.WindowsAzure.Storage.Table;

namespace ConsoleTableStorage.Models.LogicModels
{
    public class CarEntityModel : TableEntity
    {
        public CarEntityModel()
        {

        }

        public CarEntityModel(int id, int year, string make, string model, string color)
        {
            this.UniqueID = id;
            this.Year = year;
            this.Make = make;
            this.Model = model;
            this.Color = color;
            this.PartitionKey = Car;
            this.RowKey = id.ToString();
        }

        public int UniqueID { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        private readonly string Car = "Car";
    }
}
