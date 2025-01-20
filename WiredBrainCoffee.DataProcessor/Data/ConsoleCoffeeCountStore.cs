using System.Security.Cryptography;
using WiredBrainCoffee.DataProcessor.Model;

namespace WiredBrainCoffee.DataProcessor.Data
{
    public class ConsoleCoffeeCountStore : ICoffeeCountStore
    {
        private readonly TextWriter _textWriter;

        public ConsoleCoffeeCountStore(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public ConsoleCoffeeCountStore() : this(Console.Out) { }

        public void Save(CoffeeCountItem item)
        {
            var line = $"{item.CoffeeType}:{item.Count}";
            _textWriter.WriteLine(line);
        }
    }
}

