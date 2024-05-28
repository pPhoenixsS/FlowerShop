namespace Goods.DAL.Models;

public class CartModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
}