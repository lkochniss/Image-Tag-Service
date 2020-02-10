using image_tag_service.Modells;
using System.Collections.Generic;

namespace image_tag_service.Services
{
    internal class SearchService
    {
       public List<PreviewImage> SearchImages(List<PreviewImage> previewImages, string searchString)
        {
            if (searchString == "")
            {
                return previewImages;
            }

            List<PreviewImage> filteredImages = new List<PreviewImage>();
            foreach (PreviewImage previewImage in previewImages)
            {
                if (HasAllTags(searchString, previewImage.Tags))
                {
                    filteredImages.Add(previewImage);
                }
                
            }

            return filteredImages;
        }

        private bool HasAllTags(string searchString, List<string> tags)
        {
            if (tags.Count == 0)
            {
                return false;
            }

            List<string> searchTags = new List<string>(searchString.Split(','));
            foreach (string searchTag in searchTags)
            {
                if (HasTag(tags, searchTag) == false)
                {
                    return false;
                }
            }
            

            return true;
        }

        private bool HasTag(List<string> tags, string searchTag)
        {
            foreach (string tag in tags)
            {
                if (tag.Trim().ToLower().Contains(searchTag.Trim().ToLower()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}