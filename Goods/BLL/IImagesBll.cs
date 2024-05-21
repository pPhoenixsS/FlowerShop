using Goods.DAL.Models;

namespace Goods.BLL;

public interface IImagesBll
{
    Task<List<Images>> AddImagesAsync(int productId, List<IFormFile> images);
    Task DeleteImagesAsync(List<Images> images);
    Task<List<Images>> GetImagesByProductAsync(int productId);
}