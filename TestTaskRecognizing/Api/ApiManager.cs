using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace TestTaskRecognizing.Api
{
    public static class ApiManager
    {
        private static HttpWebRequest request;
        private static HttpWebResponse response;
        private static CookieContainer container;


        public static Image GetImage()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://185.80.129.249:4200/getImage");
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = container;
            
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Stream stream = response.GetResponseStream();
                if (stream == null) throw new ArgumentNullException();

                var image = Bitmap.FromStream(stream);
                
                #region Saving image
                
                Encoder myEncoder;
                EncoderParameter myEncoderParameter;
                EncoderParameters myEncoderParameters;
                myEncoderParameters = new EncoderParameters(1);
                myEncoder = Encoder.Quality;
                ImageCodecInfo  myImageCodecInfo = GetEncoderInfo("image/jpeg");
                myEncoderParameter = new EncoderParameter(myEncoder, 25L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                image.Save("Image.jpg", myImageCodecInfo, myEncoderParameters);
                
                #endregion
                
                return image;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return null;
        }
        
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for(j = 0; j < encoders.Length; ++j)
            {
                if(encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}