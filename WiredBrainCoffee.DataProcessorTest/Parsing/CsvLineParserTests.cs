namespace WiredBrainCoffee.DataProcessor.Parsing;
public class CsvLineParserTests
{
    [Fact]
    public void ShouldParseValidLine()
    {
        string[] csvLines = new[] { "Cappuccino;10/27/2022 8:06:04 AM" };

        var machineDataItems = CsvLineParser.Parse(csvLines);

        Assert.NotNull(machineDataItems);
        Assert.Single(machineDataItems);
        Assert.Equal("Cappuccino", machineDataItems[0].CoffeeType);
        Assert.Equal(new DateTime(2022,10,27,8,6,4), machineDataItems[0].CreatedAt);
    }

    [Fact]
    public void ShouldSkipEmptyLines()
    {
        string[] csvLines = new[] { "", " " };

        var machineDataItems = CsvLineParser.Parse(csvLines);

        Assert.NotNull(machineDataItems);
        Assert.Empty(machineDataItems);
    }

    [Fact]
    public void ShouldThrowExceptionForInvalidLine()
    {
        var csvLine = "Cappucino";
        string[] csvLines = new[] { csvLine };

        var exception = Assert.Throws<Exception>(() => CsvLineParser.Parse(csvLines));
        Assert.Equal($"Invalid csv line: {csvLine}", exception.Message);
    }

    [Fact]
    public void ShouldThrowExceptionForInvalidLine2()
    {
        var csvLine = "Cappucino;InvalidDateTime";
        string[] csvLines = new[] { csvLine };

        var exception = Assert.Throws<Exception>(() => CsvLineParser.Parse(csvLines));
        Assert.Equal($"Invalid csv line: {csvLine}", exception.Message);
    }
}
