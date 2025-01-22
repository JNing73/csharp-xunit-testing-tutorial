using WiredBrainCoffee.DataProcessor.Data;
using WiredBrainCoffee.DataProcessor.Model;

namespace WiredBrainCoffee.DataProcessor.Processing;

public class MachineDataProcessorTests : IDisposable
{
    private readonly FakeCoffeeCountStore _coffeeCountStore;
    private readonly MachineDataProcessor _machineDataProcessor;

    public MachineDataProcessorTests()
    {
        _coffeeCountStore = new FakeCoffeeCountStore();
        _machineDataProcessor = new MachineDataProcessor(_coffeeCountStore);
    }

    [Fact]
    public void ShouldSaveCountPerCoffeeType()
    {
        var items = new[]
        {
            new MachineDataItem("Cappuccino", new DateTime(2022,10,27,8,0,0)),
            new MachineDataItem("Cappuccino", new DateTime(2022,10,27,9,0,0)),
            new MachineDataItem("Espresso", new DateTime(2022,10,27,10,0,0))
        };

        _machineDataProcessor.ProcessItems(items);

        // Assert that the correct values will be written to the console
        Assert.Equal(2, _coffeeCountStore.SavedItems.Count); // Two types of coffee

        // First entry should be Cappucino with a count of 2 entries
        var item = _coffeeCountStore.SavedItems[0];
        Assert.Equal("Cappuccino", item.CoffeeType);
        Assert.Equal(2, item.Count);

        // One Espresso
        item = _coffeeCountStore.SavedItems[1];
        Assert.Equal("Espresso", item.CoffeeType);
        Assert.Equal(1, item.Count);
    }

    [Fact]
    public void ShouldIgnoreItemsThatAreNotNewer()
    {
        var items = new[]
        {
            new MachineDataItem("Cappuccino", new DateTime(2022,10,27,8,0,0)), // Valid Entry 1
            new MachineDataItem("Cappuccino", new DateTime(2022,10,27,7,0,0)), // Older than valid entry 1
            new MachineDataItem("Cappuccino", new DateTime(2022,10,27,7,10,0)),// Older than valid entry 1
            new MachineDataItem("Cappuccino", new DateTime(2022,10,27,9,0,0)), // Valid Entry 2
            new MachineDataItem("Espresso", new DateTime(2022,10,27,10,0,0)), // Valid Entry 3
            new MachineDataItem("Espresso", new DateTime(2022,10,27,10,0,0)) // Not newer than valid entry 3
        };

        _machineDataProcessor.ProcessItems(items);

        // Assert that the correct values will be written to the console
        Assert.Equal(2, _coffeeCountStore.SavedItems.Count); // Two types of coffee

        // First entry should be Cappucino with a count of 2 entries
        var item = _coffeeCountStore.SavedItems[0];
        Assert.Equal("Cappuccino", item.CoffeeType);
        Assert.Equal(2, item.Count);

        // One Espresso
        item = _coffeeCountStore.SavedItems[1];
        Assert.Equal("Espresso", item.CoffeeType);
        Assert.Equal(1, item.Count);
    }

    [Fact]
    public void ShouldClearPreviousCoffeeCount()
    {
        var items = new[]
        {
            new MachineDataItem("Cappuccino", new DateTime(2022,10,27,8,0,0)),
        };

        // After running twice two Type:Count's should be saved
        // And they should both be Cappuccino : 1 if the dictionary correctly cleared between runs
        _machineDataProcessor.ProcessItems(items);
        _machineDataProcessor.ProcessItems(items);

        Assert.Equal(2, _coffeeCountStore.SavedItems.Count);
        foreach (var item in _coffeeCountStore.SavedItems)
        {
            Assert.Equal("Cappuccino", item.CoffeeType);
            Assert.Equal(1, item.Count);
        }
    }

    public void Dispose()
    {
        // Code to run after every test
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
