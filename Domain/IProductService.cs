namespace DataAccess;

public interface IProductService
{
    void ReadData(string path);
    double Ads(int id);
    double SalesPrediction(int id, int days);
    double Demand(int id, int days);
}