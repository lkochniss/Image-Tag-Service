using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace image_tag_service.Services
{
    internal class SyncDirectoryService
    {
        readonly FolderPicker folderPicker = new FolderPicker();

        public SyncDirectoryService()
        {
            folderPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add(".png");
            folderPicker.FileTypeFilter.Add(".jpeg");
            folderPicker.FileTypeFilter.Add(".jpg");
        }

        public async Task<StorageFolder> OpenPicker()
        {
            return await folderPicker.PickSingleFolderAsync();
        }
    }
}