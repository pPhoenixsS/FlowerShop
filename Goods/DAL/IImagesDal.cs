using Goods.DAL.Models;

namespace Goods.DAL;

public interface IImagesDal
{
    Task<List<Images>> AddImagesAsync(List<Images> images);
    Task DeleteImagesAsync(List<Images> images);
    Task<List<Images>> GetImagesByProductAsync(int productId);
}