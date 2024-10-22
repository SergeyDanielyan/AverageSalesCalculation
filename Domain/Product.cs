namespace Domain;

public class Product
{
    public int Id { get; private set; }
    public DateTime Date { get; private set; }
    public int Sale { get; private set; }
    public int Stock { get; private set; }

    public Product(int id, DateTime date, int sale, int stock)
    {
        Id = id;
        Date = date;
        Sale = sale;
        Stock = stock;
    }
    
    public Product(int id, string stringDate, int sale, int stock)
    {
        Id = id;
        if (DateTime.TryParse(stringDate, out DateTime tempDate))
        {
            Date = tempDate;
        }
        else
        {
            throw new FormatException("Data format is wrong");
        }
        Sale = sale;
        Stock = stock;
    }
}