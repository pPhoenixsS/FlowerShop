namespace Goods.DAL.Models;

public class ProductResponceModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Kind { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Count { get; set; }
    public List<string> Images;
}