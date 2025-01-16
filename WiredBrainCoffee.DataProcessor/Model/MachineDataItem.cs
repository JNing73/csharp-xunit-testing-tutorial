namespace WiredBrainCoffee.DataProcessor.Model
{
    public class MachineDataItem
    {
        public MachineDataItem(string coffeeType, DateTime createdAt)
        {
            CoffeeType = coffeeType;
            CreatedAt = createdAt;
        }

        public string CoffeeType { get; }

        public DateTime CreatedAt { get; }
    }
}
