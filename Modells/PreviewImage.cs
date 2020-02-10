using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;

namespace image_tag_service.Modells
{
    internal class PreviewImage
    {
        private List<string> _Tags = new List<string>();

        public List<string> Tags
        {
            get => _Tags;
            set => this._Tags = value;
        }

        private BitmapImage _ImageData;
        public BitmapImage ImageData
        {
            get => _ImageData;
            set => this._ImageData = value;
        }
    }
}