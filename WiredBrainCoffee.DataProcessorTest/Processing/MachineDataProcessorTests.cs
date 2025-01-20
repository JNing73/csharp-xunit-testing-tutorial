using WiredBrainCoffee.DataProcessor.Data;
using WiredBrainCoffee.DataProcessor.Model;

namespace WiredBrainCoffee.DataProcessor.Processing;

public class MachineDataProcessorTests
{
    [Fact]
    public void ShouldSaveCountPerCoffeeType()
    {
        var coffeeCountStore = new FakeCoffeeCountStore();
        var machineDataProcessor = new MachineDataProcessor(coffeeCountStore);
        var items = new[]
        {
            new MachineDataItem("Cappuccino", new DateTime(2022,10,27,8,0,0)),
            new MachineDataItem("Cappuccino", new DateTime(2022,10,27,9,0,0)),
            new MachineDataItem("Espresso", new DateTime(2022,10,27,10,0,0))
        };

        machineDataProcessor.ProcessItems(items);

        // Assert that the correct values will be written to the console
        Assert.Equal(2, coffeeCountStore.SavedItems.Count); // Two types of coffee

        // First entry should be Cappucino with a count of 2 entries
        var item = coffeeCountStore.SavedItems[0];
        Assert.Equal("Cappuccino", item.CoffeeType);
        Assert.Equal(2, item.Count);

        // One Espresso
        item = coffeeCountStore.SavedItems[1];
        Assert.Equal("Espresso", item.CoffeeType);
        Assert.Equal(1, item.Count);
    }
}

public class FakeCoffeeCountStore : ICoffeeCountStore
{
    public List<CoffeeCountItem> SavedItems { get; } = new();

    public void Save(CoffeeCountItem item)
    {
        SavedItems.Add(item);
    }
}
