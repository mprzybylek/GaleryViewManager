using GalleryViewManager.Common.Utils;
using GalleryViewManager.Core.Model;
using GalleryViewManager.Core.Services;
using System.Collections.ObjectModel;

namespace GalleryViewManager.Core.ViewModels
{
    public class SavedImagesViewModel : AbstractViewModel
    {
        private DbImageService _dbService;
        private BitmapImageUtils _bitmapUtils;
        private ObservableCollection<ImageModel> _savedImages;
        public ObservableCollection<ImageModel> SavedImages
        {
            get
            {
                return _savedImages;
            }
            set
            {
                if (value != _savedImages)
                {
                    _savedImages = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public SavedImagesViewModel(DbImageService dbService,BitmapImageUtils bitmapUtils)
        {
            SavedImages = new ObservableCollection<ImageModel>();
            _dbService = dbService;
            _bitmapUtils = bitmapUtils;
        }


        public async void LoadImages()
        {
            foreach(var image in await _dbService.GetImages())
            {
                SavedImages.Add(new ImageModel
                {
                    Image = _bitmapUtils.ByteArrayToImage(image.Image)
                });
            }
        }

    }
}
