using image_tag_service.Modells;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;
using Windows.UI.Xaml.Media.Imaging;

namespace image_tag_service.Services
{
    internal class ImageService
    {
        public async Task<List<PreviewImage>> ShowImages(StorageFolder imageFolder)
        {
            CommonFileQuery query = CommonFileQuery.DefaultQuery;
            var queryOptions = new QueryOptions(query, new[] { ".png", ".jpg" })
            {
                FolderDepth = FolderDepth.Deep
            };
            _ = imageFolder.CreateFileQueryWithOptions(queryOptions);


            return await GenerateImages(await imageFolder.GetFilesAsync());
        }

        private async Task<List<PreviewImage>> GenerateImages(IReadOnlyList<StorageFile> images)
        {
            List<PreviewImage> previewImages = new List<PreviewImage>();
            foreach (StorageFile image in images)
            {
                using (var stream = await image.OpenAsync(FileAccessMode.Read))
                {
                    var previewImage = new BitmapImage();
                    await previewImage.SetSourceAsync(stream);
                    var tags = await GetTags(image);

                    previewImages.Add(new PreviewImage { ImageData = previewImage, Tags = tags });
                }
            }

            return previewImages;
        }

        private async Task<List<string>> GetTags(StorageFile image)
        {
            ImageProperties props = await image.Properties.GetImagePropertiesAsync();

            List<string> tags = new List<string>();

            foreach (string keyword in props.Keywords)
            {
                tags.Add(keyword);
            }

            return tags;
        }
    }
}