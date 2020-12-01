using GalleryViewManager.Common.Utils;
using GalleryViewManager.Core.Services;
using GalleryViewManager.Core.ViewModels;
using GalleryViewManager.DataAccess;
using GalleryViewManager.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GalleryViewManager.Core
{
    public class Locator
    {

        private static ServiceProvider _serviceProvider;

        static Locator()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
            _serviceProvider.GetService<ImageContext>().Database.Migrate();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<ImageContext>();
            services.AddSingleton<DbImageService>();
            services.AddScoped<GaleryViewModel>();
            services.AddScoped<SavedImagesViewModel>();
            services.AddScoped<EditImageWindowViewModel>();
            services.AddSingleton<GoogleImageService>();
            services.AddTransient<EditImageWindow>();
            services.AddSingleton<BitmapImageUtils>();
        }
        //Views
        public static EditImageWindow EditImageView { get { return _serviceProvider.GetService<EditImageWindow>(); } }
        //ViewModels
        public static GaleryViewModel GaleryViewModel { get { return _serviceProvider.GetService<GaleryViewModel>(); } }
        public static SavedImagesViewModel SavedImagesViewModel { get { return _serviceProvider.GetService<SavedImagesViewModel>(); } }
        public static EditImageWindowViewModel EditImageViewModel { get { return _serviceProvider.GetService<EditImageWindowViewModel>(); } }

    }
}
