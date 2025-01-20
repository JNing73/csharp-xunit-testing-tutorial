using WiredBrainCoffee.DataProcessor.Model;

namespace WiredBrainCoffee.DataProcessor.Data;
public class ConsoleCoffeeCountStoreTests
{
    [Fact]
    public void ShouldWriteOutputToConsole()
    {
        var coffeeCountItem = new CoffeeCountItem("Cappuccino", 5);
        var consoleCoffeeCountStore = new ConsoleCoffeeCountStore();

        consoleCoffeeCountStore.Save(coffeeCountItem);
    }
}
