using GalleryViewManager.Common.Utils;
using GalleryViewManager.Core.Model;
using GalleryViewManager.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalleryViewManager.Core.Services
{
    public class DbImageService
    {
        private readonly ImageContext _dbContext;
        private readonly BitmapImageUtils _bitmapUtils;

        public DbImageService(ImageContext dbContext, BitmapImageUtils bitmapUtils)
        {
            _dbContext = dbContext;
            _bitmapUtils = bitmapUtils;
        }
        public virtual async void AddImage(ImageModel image)
        {
            var imageEntity = new ImageEntity
            {
                Image = _bitmapUtils.ImageToByteArray(image.Image)
            };

            await _dbContext.AddAsync<ImageEntity>(imageEntity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<List<ImageEntity>> GetImages()
        {
            return await _dbContext.Images.ToListAsync();
        }
    }
}
