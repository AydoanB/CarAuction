using System.Threading.Tasks;
using CarAuction.Data.Models.CarModels;
using Microsoft.AspNetCore.Http;

namespace CarAuction.Services.Data
{
    public interface IImageService
    {
        Task SaveImageToWebRootAsync(string imagePath, Image dbImage, IFormFile image, string folderPath);
        Image CreateImage(IFormFile image, string userId);
    }
}
