using DataAccess;
using Domain;

namespace Services;

public class ProductService : IProductService
{
    private IProductRepository _productRepository = new ProductRepository();
    
    public void ReadData(string path)
    {
        _productRepository.ReadData(path);
    }

    private double GetLastStock(int id)
    {
        var products = _productRepository.GetProductsListById(id);
        if (products.Count == 0)
        {
            throw new ArgumentNullException("There are no products");
        }
        Product lastProduct = products[0];
        foreach (var product in products)
        {
            if (product.Date > lastProduct.Date)
            {
                lastProduct = product;
            }
        }
        return lastProduct.Stock;
    }

    public double Ads(int id)
    {
        var products = _productRepository.GetProductsListById(id);
        return products.Average(product => (product.Sale != 0 || product.Stock != 0) ? product.Sale : 0);
    }

    public double SalesPrediction(int id, int days)
    {
        return Ads(id) * days;
    }

    public double Demand(int id, int days)
    {
        return Math.Max(SalesPrediction(id, days) - GetLastStock(id), 0);
    }
}