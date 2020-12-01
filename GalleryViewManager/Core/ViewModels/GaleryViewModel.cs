using GalleryViewManager.Common.Utils;
using GalleryViewManager.Common.ViewModels;
using GalleryViewManager.Core.Model;
using GalleryViewManager.Core.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GalleryViewManager.Core.ViewModels
{
    public class GaleryViewModel : AbstractViewModel
    {
        private string _searchedPhrase;
        private ObservableCollection<ImageModel> _images;
        private GoogleImageService _googleService;
        private DbImageService _dbService;
        private ICommand _loadData;
        private ICommand _addToFavourites;

        public string SearchedPhrase
        {
            get
            {
                return _searchedPhrase;
            }
            set
            {
                if (value != _searchedPhrase)
                {
                    _searchedPhrase = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ObservableCollection<ImageModel> Images
        {
            get
            {
                return _images;
            }
            set
            {
                if (value != _images)
                {
                    _images = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ICommand LoadImages
        {
            get => _loadData ??= new CommandHandler(
                (obj) => ExecuteLoadImages(),
                () => { return true; });
         }

        public ICommand SaveImageInStorage
        {
            get => _addToFavourites ??= new CommandHandler(
                (image) => 
                {
                    ExecuteSaveImageInStorageDb(image);
                    ExecuteSaveImageInStorageGui(image);
                },
                () => { return true; });
        }

        private void ExecuteSaveImageInStorageGui(object image)
        {
            var imageToSaveInGui = image as BitmapImage;
            Locator.SavedImagesViewModel.SavedImages.Add(new ImageModel { Image = imageToSaveInGui});
            
        }

        private void ExecuteSaveImageInStorageDb(object image)
        {
            var imageToSaveInDb = image as BitmapImage;
            _dbService.AddImage(new ImageModel { Image = imageToSaveInDb});
        }
        public async void ExecuteLoadImages()
        {
            Images.Clear();
            var items = _googleService.GetGoogleImages(SearchedPhrase);
            int counter = 0;
            await foreach (var item in items)
            {
                Images.Add(new ImageModel
                {
                    Image = item,
                    Id = counter++
                });
            }
        }

        public GaleryViewModel(DbImageService dbService, GoogleImageService googleService)
        {
            Images = new ObservableCollection<ImageModel>();
            _dbService = dbService;
            _googleService = googleService;
        }
    }
}
