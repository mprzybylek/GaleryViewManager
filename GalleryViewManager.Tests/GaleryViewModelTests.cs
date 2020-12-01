using GalleryViewManager.Common.Utils;
using GalleryViewManager.Core;
using GalleryViewManager.Core.Model;
using GalleryViewManager.Core.Services;
using GalleryViewManager.Core.ViewModels;
using GalleryViewManager.DataAccess;
using Moq;
using NUnit.Framework;
using System.Windows.Media.Imaging;

namespace GalleryViewManager.Tests
{
    class GaleryViewModelTests
    {
        private Mock<GoogleImageService> _mockGoogleImageService;
        private Mock<DbImageService> _mockDbService;
        private GaleryViewModel _galeryViewModel;


        [SetUp]
        public void Setup()
        {
            _mockGoogleImageService = new Mock<GoogleImageService>(new Mock<BitmapImageUtils>().Object);
            _mockDbService = new Mock<DbImageService>(new Mock<ImageContext>().Object, new Mock<BitmapImageUtils>().Object);
            _galeryViewModel = new GaleryViewModel(_mockDbService.Object,_mockGoogleImageService.Object);
        }
        [Test]
        public void saveImageInStorage_save_in_storage()
        {
            var expected = Locator.SavedImagesViewModel.SavedImages.Count + 1;
            var actual = 0;
            var image = new BitmapImage();

            _galeryViewModel.SaveImageInStorage.Execute(image);
            actual = Locator.SavedImagesViewModel.SavedImages.Count;

            Assert.AreEqual(expected,actual);
        }

        [Test]
        public void saveImageInStorage_save_in_db()
        {
            var image = new BitmapImage();

            _galeryViewModel.SaveImageInStorage.Execute(image);

            _mockDbService.Verify(x => x.AddImage(It.IsAny<ImageModel>()), Times.Once());
        }
    }
}
