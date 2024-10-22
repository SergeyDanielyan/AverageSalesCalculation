namespace Domain;

public interface IProductRepository
{
    void ReadData(string fileName);
    List<Product> GetProductsListById(int id);
}