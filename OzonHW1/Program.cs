using System.Reflection.Metadata;
using DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace OzonHW1;

class Program
{
    static void Menu()
    {
        Console.WriteLine("Welcome to the app!\n" +
                          "You can read data from some .txt file or get information about that data.\n" +
                          "Write:\n" +
                          "\"data <path>\" to update the data;\n" +
                          "\"ads <id>\" to get average sales per day;\n" +
                          "\"prediction <id> [days]\" to get sales prediction;\n" +
                          "\"demand <id> [days]\" to get demand;\n" +
                          "\"stop\" to stop this app.");
    }
    
    
    static void Main(string[] args)
    {
        ServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IProductService, ProductService>();
        serviceCollection.AddSingleton<ConsoleHandler>();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        ConsoleHandler consoleHandler = serviceProvider.GetRequiredService<ConsoleHandler>();
        Menu();
        string query = Console.ReadLine();
        while (query != "stop")
        {
            consoleHandler.Handle(query);
            Menu();
            query = Console.ReadLine();
        }
    }
}