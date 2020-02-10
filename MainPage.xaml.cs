using image_tag_service.Modells;
using image_tag_service.Services;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace image_tag_service
{
    public sealed partial class MainPage : Page
    {
        
        private List<PreviewImage> previewImages = new List<PreviewImage>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void SyncFolder_Clicked(object sender, RoutedEventArgs e)
        {
            SyncDirectoryService syncDirectoryService = new SyncDirectoryService();
            StorageFolder folder = await syncDirectoryService.OpenPicker();

            if (folder != null)
            {
                StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);

                ImageService imageService = new ImageService();
                previewImages = await imageService.ShowImages(folder);

                ImageGallery.ItemsSource = previewImages.ToArray();
            }
        }

        private void TagSearchInput_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            SearchService service = new SearchService();
            ImageGallery.ItemsSource = service.SearchImages(this.previewImages, TagSearchInput.Text);
        }
    }
}
