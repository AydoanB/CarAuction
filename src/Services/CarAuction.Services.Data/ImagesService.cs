using SixLabors.ImageSharp;
using static CarAuction.Common.GlobalConstants;

namespace CarAuction.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CarAuction.Data.Models.CarModels;
    using Microsoft.AspNetCore.Http;
    using SixLabors.ImageSharp.Processing;

    public class ImagesService : IImagesService
    {
        private readonly string[] allowedExtensions = { "jpg", "jpeg", "png", "gif" };

        public async Task SaveImageToWebRootAsync(string imagePath, Image dbImage, IFormFile image, string folderPath)
        {
            var imagePhysicalPath = $"{imagePath}/{folderPath}/{dbImage.Id}.{dbImage.Extension}";
            using Stream fileStream = new FileStream(imagePhysicalPath, FileMode.Create);
            await image.CopyToAsync(fileStream);

            using SixLabors.ImageSharp.Image img = await SixLabors.ImageSharp.Image.LoadAsync(imagePhysicalPath);
            var size = new Size(650, 600);
            img.Mutate(x => x.Resize(new ResizeOptions()
            {
                Size = size,
                Mode = ResizeMode.Crop,
                Position = AnchorPositionMode.Center,
            }));
            var imageResizedPath = $"{imagePath}/{CarsResizedImagesFolder}/{dbImage.Id}.{dbImage.Extension}";
            using Stream file2Stream = new FileStream(imageResizedPath, FileMode.Create);
            await img.SaveAsPngAsync(file2Stream);
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
