using Goods.DAL.Models;
using Goods.Exceptions;

namespace Goods.Services;

public static class ImageManager
{
    public static async Task<List<string>> SaveImageAsync(List<IFormFile> images)
    {
        var imagesNames = new List<string>();
        
        foreach (var image in images)
        {
            if (image == null || image.Length == 0)
                throw new BadImageException();

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = $"{Guid.NewGuid()}_{image.FileName}";
            var filePath = Path.Combine(uploadPath, fileName);
            
            imagesNames.Add(filePath);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(stream);
        }

        return imagesNames;
    }

    public static void DeleteImages(List<string> fileNames)
    {
        if (fileNames == null || fileNames.Count == 0)
        {
            throw new NotFoundException();
        }

        var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        var deletedFiles = new List<string>();

        foreach (var fileName in fileNames)
        {
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                deletedFiles.Add(fileName);
            }
        }

        if (deletedFiles.Count == 0)
        {
            throw new NotFoundException();
        }
    }

    public static async Task<List<string>> ImageToBase64(List<Images> images)
    {
        List<string> base64Images = new List<string>();
        
        foreach (var image in images)
        {
            var imageArray = await File.ReadAllBytesAsync(image.Path);
            base64Images.Add(Convert.ToBase64String(imageArray));
        }

        return base64Images;
    }
}