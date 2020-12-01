using GalleryViewManager.Common.Utils;
using GalleryViewManager.Core.Model;
using GalleryViewManager.Core.Services;
using GalleryViewManager.DataAccess;
using GalleryViewManager.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Media.Imaging;

namespace GalleryViewManager.Tests
{
    public class DbImageServiceTests
    {
        private DbImageService _dbImageService;
        private Mock<ImageContext> _mockContext;
        private Mock<ImageModel> _mockImageModel;
        private Mock<BitmapImageUtils> _mockBitmapUtils;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<ImageContext>();
            var mockList = new List<ImageEntity>
            {
                new ImageEntity(),
                new ImageEntity()
            }.AsQueryable();
            var mockSet = new Mock<DbSet<ImageEntity>>();
            mockSet.As<IAsyncEnumerable<ImageEntity>>()
                .Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestDbAsyncEnumerator<ImageEntity>(mockList.GetEnumerator()));

            mockSet.As<IQueryable<ImageEntity>>()
             .Setup(m => m.Provider)
             .Returns(new TestDbAsyncQueryProvider<ImageEntity>(mockList.Provider));

            mockSet.As<IQueryable<ImageEntity>>().Setup(m => m.Expression).Returns(mockList.Expression);
            mockSet.As<IQueryable<ImageEntity>>().Setup(m => m.ElementType).Returns(mockList.ElementType);
            mockSet.As<IQueryable<ImageEntity>>().Setup(m => m.GetEnumerator()).Returns(mockList.GetEnumerator());

            _mockContext.Setup(x => x.Images).Returns(mockSet.Object);

            _mockImageModel = new Mock<ImageModel>();

            _mockBitmapUtils = new Mock<BitmapImageUtils>();
            _mockBitmapUtils.Setup(x => x.ImageToByteArray(It.IsAny<BitmapImage>())).Returns(new byte[0]);

            _dbImageService = new DbImageService(_mockContext.Object,_mockBitmapUtils.Object);
            
        }

        [Test]
        public void addImage_update_db_context()
        {
            _dbImageService.AddImage(_mockImageModel.Object);

            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
        [Test]
        public void getImages_return_collection_from_context()
        {
            var images = _dbImageService.GetImages();

            Assert.AreEqual(2, images.Result.Count);
        }

    }
}
