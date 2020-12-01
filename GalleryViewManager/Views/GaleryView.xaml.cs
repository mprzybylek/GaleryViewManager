using GalleryViewManager.Core;
using GalleryViewManager.Core.Model;
using GalleryViewManager.Core.Services;
using GalleryViewManager.Core.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GalleryViewManager.Views
{
    public partial class GaleryView : UserControl
    {
        public GaleryView()
        {
            InitializeComponent();
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var imageSource = (sender as Image)?.Source as BitmapImage;


            Locator.EditImageViewModel.Image = new ImageModel
            {
                Image = imageSource
            };
                
            Locator.EditImageView.ShowDialog();
        }
    }
}
