using Domain;

namespace DataAccess;

public class ProductRepository : IProductRepository
{
    private Dictionary<int, List<Product> > _products;

    private bool IsHeaderCorrect(string header)
    {
        return header == "id, date, sales, stock";
    }

    private Product ParseProduct(string tableLine)
    {
        string[] strings = tableLine.Split(", ");
        if (strings.Length != 4)
        {
            throw new FormatException("Data from file is wrong");
        }

        if (!int.TryParse(strings[0], out int id))
        {
            throw new FormatException("Data format is wrong");
        }
        if (!DateTime.TryParse(strings[1], out DateTime tempDate)) 
        
        {
            throw new FormatException("Data format is wrong");
        }
        if (!int.TryParse(strings[2], out int sale))
        {
            throw new FormatException("Data format is wrong");
        }
        if (!int.TryParse(strings[3], out int stock))
        {
            throw new FormatException("Data format is wrong");
        }
        return new Product(id, tempDate, sale, stock);
    }

    public void ReadData(string fileName)
    {
        _products = new();
        using (StreamReader streamReader = new StreamReader(fileName))
        {
            string? header = streamReader.ReadLine();
            if (header == null || !IsHeaderCorrect(header))
            { 
                throw new FormatException("Data from file is wrong");
            }
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                Product product = ParseProduct(line); 
                if (!_products.ContainsKey(product.Id)) 
                { 
                    _products[product.Id] = new List<Product>(); 
                } 
                _products[product.Id].Add(product);
            }
        }
    }

    public List<Product> GetProductsListById(int id)
    {
        if (_products.ContainsKey(id))
        {
            return _products[id];
        }

        return new List<Product>();
    }
}