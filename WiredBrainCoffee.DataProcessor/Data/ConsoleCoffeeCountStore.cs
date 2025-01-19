using WiredBrainCoffee.DataProcessor.Model;

namespace WiredBrainCoffee.DataProcessor.Data
{
    public class ConsoleCoffeeCountStore : ICoffeeCountStore
    {
        public void Save(CoffeeCountItem item)
        {
            var line = $"{item.CoffeeType}:{item.Count}";
            Console.WriteLine(line);
        }
    }
}

