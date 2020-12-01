using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace GalleryViewManager.Common.Utils
{
    public class BitmapImageUtils
    {
        public virtual BitmapImage ByteArrayToImage(byte[] byteArrayIn)
        {
            using (var stream = new MemoryStream(byteArrayIn))
            {
                var _image = new BitmapImage();
                _image = new BitmapImage();
                _image.BeginInit();
                _image.StreamSource = stream;
                _image.CacheOption = BitmapCacheOption.OnLoad;
                _image.EndInit();
                return _image;
            }
        }
        public virtual byte[] ImageToByteArray(BitmapImage image)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }
    }
}
