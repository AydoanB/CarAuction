namespace CarAuction.Services.Data
{
    using System.Threading.Tasks;

    using CarAuction.Data.Models.CarModels;
    using Microsoft.AspNetCore.Http;

    public interface IImagesService
    {
        Task SaveImageToWebRootAsync(string imagePath, Image dbImage, IFormFile image, string folderPath);
        Image CreateImage(IFormFile image, string userId);
        void Resizer(string imagesDirectory, string folderPath, string imageName);
    }
}
