using GalleryViewManager.Common.Utils;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GalleryViewManager.Core.Services
{
    public class GoogleImageService
    {
        private BitmapImageUtils _bitmapUtils;

        public GoogleImageService(BitmapImageUtils bitmapUtils)
        {
            _bitmapUtils = bitmapUtils;
        }
        public async IAsyncEnumerable<BitmapImage> GetGoogleImages(string phrase)
        {
           var imagesUrls = GetImageUrls(phrase);

           foreach(var image in imagesUrls)
           {
                var downoladedImage = await DownloadImageFromUrl(image);
                if (downoladedImage == null) 
                    continue;

                yield return downoladedImage;
           };
        }

        private async Task<BitmapImage> DownloadImageFromUrl(string url)
        {
            Uri uri;
            if(Uri.TryCreate(url,UriKind.Absolute,out uri))
            {
                var imageBytesArray = await new WebClient().DownloadDataTaskAsync(url);
                return _bitmapUtils.ByteArrayToImage(imageBytesArray);
            }
            else
            {
                return null;
            }
        }

        private IEnumerable<string> GetImageUrls(string phrase)
        {

            var doc = new HtmlDocument();

            string requestUrl =
                  string.Format("http://images.google.com/images?" +
                   "q={0}&tbm=isch", phrase);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);

            using (HttpWebResponse httpWebResponse = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = httpWebResponse.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        doc.LoadHtml(reader.ReadToEnd());
                    }
                }
            }
            return doc.DocumentNode.SelectNodes("//img").Select(x=>x.Attributes["src"].Value);
        }

    }
}
