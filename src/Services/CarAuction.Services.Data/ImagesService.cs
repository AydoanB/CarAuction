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
    using SixLabors.ImageSharp.Formats.Png;
    using SixLabors.ImageSharp.PixelFormats;
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

        public void Resizer(string imagesDirectory, string folderPath, string imageName)
        {
            var outDir = $"{imagesDirectory}/resized";
                //var curDir = Directory.GetDirectoryRoot($"{imagesDirectory}/{folderPath}");
            var imagePath = $"{imagesDirectory}/{folderPath}/{imageName}";

            // foreach (FileInfo file in files.Where(x=> x.EndsWith(".png")).Select(x=> new FileInfo(x)))
            // {
                using (SixLabors.ImageSharp.Image<Rgba32> image = (SixLabors.ImageSharp.Image<Rgba32>)SixLabors.ImageSharp.Image.Load(imagePath))
                {
                    image.Mutate(x => x.Resize(256, 256));

                    using Stream fileStream = new FileStream(outDir, FileMode.Create);

                    image.Save(fileStream, new PngEncoder());
                }

            // }
        }
    }
}
