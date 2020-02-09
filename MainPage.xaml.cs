using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace image_tag_service
{
    public sealed partial class MainPage : Page
    {
        readonly FolderPicker folderPicker = new FolderPicker();

        public MainPage()
        {
            this.InitializeComponent();
            folderPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add(".png");
            folderPicker.FileTypeFilter.Add(".jpeg");
            folderPicker.FileTypeFilter.Add(".jpg");
        }

        private async void SyncFolderClick(object sender, RoutedEventArgs e)
        {


            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                initial.Text = folder.Path;
                ShowImages(folder);

            }
            else
            {
                initial.Text = "Error";
            }
        }

        private async void ShowImages(StorageFolder imageFolder)
        {
            CommonFileQuery query = CommonFileQuery.DefaultQuery;
            var queryOptions = new QueryOptions(query, new[] { ".png", ".jpg" });
            queryOptions.FolderDepth = FolderDepth.Deep;
            var queryResult = imageFolder.CreateFileQueryWithOptions(queryOptions);

            var previewImages = await GenerateImages(await imageFolder.GetFilesAsync());

            this.ImageGallery.ItemsSource = previewImages.ToArray();
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

                    previewImages.Add(new PreviewImage { ImageData = previewImage });
                }
            }

            return previewImages;
        }
    }

    public class PreviewImage
    {
        private string _Tags;
        public string Tags
        {
            get { return this._Tags; }
            set { this._Tags = value; }
        }

        private BitmapImage _ImageData;
        public BitmapImage ImageData
        {
            get { return this._ImageData; }
            set { this._ImageData = value; }
        }

    }
}
