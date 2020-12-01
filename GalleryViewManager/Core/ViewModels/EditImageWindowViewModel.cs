using GalleryViewManager.Common.Utils;
using GalleryViewManager.Core.Model;

namespace GalleryViewManager.Core.ViewModels
{
    public class EditImageWindowViewModel : AbstractViewModel
    {
        private ImageModel image;
        private int scale = 1;

        public ImageModel Image
        {
            get
            {
                return image;
            }
            set
            {
                if (value != image)
                {
                    image = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Scale 
        { 
            get 
            { 
                return scale; 
            } 
            set 
            { 
                if(value != scale)
                {
                    scale = value;
                    NotifyPropertyChanged();
                }
            } 
        }
        public EditImageWindowViewModel()
        {
            Image = new ImageModel();
        }
    }
}
