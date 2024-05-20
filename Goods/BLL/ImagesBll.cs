using Goods.DAL;
using Goods.DAL.Models;
using Goods.Exceptions;
using Goods.Services;

namespace Goods.BLL;

public class ImagesBll(IImagesDal imagesDal) : IImagesBll
{
    public async Task<List<Images>> AddImagesAsync(int productId, List<IFormFile> images)
    {
        var paths = await ImageManager.SaveImageAsync(images);

        var imageModels = new List<Images>();

        foreach (var path in paths)
        {
            imageModels.Add(new Images()
            {
                Path = path,
                ProductId = productId
            });
        }

        await imagesDal.AddImagesAsync(imageModels);

        return imageModels;
    }

    public async Task DeleteImagesAsync(List<Images> images)
    {
        var fileNames = images.Select(i => i.Path).ToList();
        var deleteTask = Task.Run(() => ImageManager.DeleteImages(fileNames));

        await imagesDal.DeleteImagesAsync(images);
    }

    public async Task<List<Images>> GetImagesByProductAsync(int productId)
    {
        var imagesFromDb = await imagesDal.GetImagesByProductAsync(productId);
        if (imagesFromDb == null || imagesFromDb.Count == 0)
            throw new NotFoundException();

        return imagesFromDb;
    }
}