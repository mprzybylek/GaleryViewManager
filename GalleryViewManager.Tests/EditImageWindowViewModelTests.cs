using GalleryViewManager.Core.ViewModels;
using Moq;
using NUnit.Framework;

namespace GalleryViewManager.Tests
{

    class EditImageWindowViewModelTests
    {
        private EditImageWindowViewModel _stubEditImageWindowViewModel;

        [SetUp]
        public void Setup()
        {
            _stubEditImageWindowViewModel = new EditImageWindowViewModel();
        }

        [Test]
        public void scale_raise_PropertyChangedEvent()
        {
            var notified = false;
            var expected = true;

            _stubEditImageWindowViewModel.PropertyChanged += (s, e) =>
            {
                    notified = true;
            };
            _stubEditImageWindowViewModel.Scale += 1;

            Assert.AreEqual(expected, notified);
        }

        [Test]
        public void image_update_raise_PropertyChangedEvent()
        {
            var notified = false;
            var expected = true;

            _stubEditImageWindowViewModel.PropertyChanged += (s, e) =>
            {
                notified = true;
            };
            _stubEditImageWindowViewModel.Image = new Core.Model.ImageModel { };

            Assert.AreEqual(expected, notified);
        }
    }
}
