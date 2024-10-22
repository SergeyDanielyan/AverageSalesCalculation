using DataAccess;
using Services;

namespace OzonHW1;

public class ConsoleHandler
{
    private readonly IProductService _productService;

    public ConsoleHandler(IProductService productService)
    {
        _productService = productService;
    }

    public void Handle(string stringQuery)
    {
        string[] query = stringQuery.Split();
        try
        {
            if (query.Length == 2)
            {
                if (query[0] == "data")
                {
                    _productService.ReadData(query[1]);
                }
                else if (query[0] == "ads" && int.TryParse(query[1], out int id))
                {
                    Console.WriteLine($"ADS = {_productService.Ads(id)}");
                }
                else
                {
                    throw new FormatException("Wrong query format");
                }
            } 
            else if (query.Length == 3)
            {
                if (query[0] == "prediction" && int.TryParse(query[1], out int id)
                                             && int.TryParse(query[2], out int days))
                {
                    Console.WriteLine($"Prediction = {_productService.SalesPrediction(id, days)}");
                } 
                else if (query[0] == "demand" && int.TryParse(query[1], out id)
                                                && int.TryParse(query[2], out days))
                {
                    Console.WriteLine($"Demand = {_productService.Demand(id, days)}");
                }
                else
                {
                    throw new FormatException("Wrong query format");
                }
            }
            else 
            {
                throw new FormatException("Wrong query format");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}