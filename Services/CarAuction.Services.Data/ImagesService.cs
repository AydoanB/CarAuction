namespace CarAuction.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CarAuction.Data.Models.CarModels;
    using Microsoft.AspNetCore.Http;

    public class ImageService : IImageService
    {
        private readonly string[] allowedExtensions = { "jpg", "jpeg", "png", "gif" };

        public async Task SaveImageToWebRootAsync(string imagePath, Image dbImage, IFormFile image,
            string folderPath)
        {
            var posterPhysicalPath = $"{imagePath}/{folderPath}/{dbImage.Id}.{dbImage.Extension}";
            using Stream fileStream = new FileStream(posterPhysicalPath, FileMode.Create);
            await image.CopyToAsync(fileStream);
        }

        public Image CreateImage(IFormFile image, string userId)
        {
            var extension = Path.GetExtension(image.FileName).TrimStart('.');

            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var carImage = new Image
            {
                AddedByUserId = userId,
                Extension = extension,
            };

            return carImage;
        }
    }
}
