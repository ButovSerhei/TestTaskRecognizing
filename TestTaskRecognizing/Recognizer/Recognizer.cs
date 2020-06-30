using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using TestTaskRecognizing.Entities;
using Image = System.Drawing.Image;

namespace TestTaskRecognizing.Recognizer
{
    public class Recognizer
    {
        private readonly Image<Bgr, byte> _image;
        public readonly Page _page;

        #region Example pictures

        private const string newItemPicPath =
            @"..\Resources\NewItemExample.jpg";

        private const string newItemKeepItemTile = 
            @"..\Resources\keepItemsTile.png";

        private const string customizeItemPicPath =
            @"..\Resources\CustomizeHeader.png";

        private const string customizeTalePicPath =
            @"..\Resources\customizeTileActive.png";

        #endregion

        public Image ResultImage { get; set; }

        public Recognizer(Image image)
        {
            //var tempImage = Image.FromFile("C:\\Users\\Kwazar\\Desktop\\getImage.png");
            //var tempImage = Image.FromFile("C:\\Users\\Kwazar\\Desktop\\Image.jpg");
            // _image = new Bitmap(tempImage).ToImage<Bgr,byte>();
            _image = new Bitmap(image).ToImage<Bgr,byte>();
            ResultImage = _image.ToBitmap();

            _page = Recognize();

            if (_page is NewItemEntity)
            {
                _page = RecognizeTiles(newItemKeepItemTile, _page as NewItemEntity);
            }
            else if(_page is CustomizeEntity)
            {
                _page = RecognizeTiles(customizeTalePicPath, _page as CustomizeEntity);
            }
            else
            {
                return;
            }
        }

        private Page Recognize()
        {
            var isNewItemPage = RecognizePage(newItemPicPath);
            var isCustomizePage = RecognizePage(customizeItemPicPath);

            if (isNewItemPage)
            {
                return new NewItemEntity();
            }

            return isCustomizePage ? new CustomizeEntity() : null;

        }

        private bool RecognizePage(string exampleImagePath)
        {
            double threshold = 0.002;
            var example = (Bitmap) Image.FromFile(exampleImagePath);
            var template = new Bitmap(example).ToImage<Bgr, byte>();

            #region Recognize

            var imageOut = new Mat();
            CvInvoke.MatchTemplate(_image, template, imageOut, TemplateMatchingType.Sqdiff);
            double minVal = 0.0,
                maxVal = 0.0;
            Point minLoc = new Point();
            Point maxLoc = new Point();

            CvInvoke.MinMaxLoc(imageOut, ref minVal, ref maxVal, ref minLoc, ref maxLoc);

            #endregion

            double prob = minVal / imageOut.ToImage<Gray, byte>().GetSum().Intensity;

            var isContains = prob < threshold;
            
            return isContains;
        }


        private CustomizeEntity RecognizeTiles(string exampleImagePath, CustomizeEntity entity)
        {
            var exampleImage = (Bitmap)Image.FromFile(exampleImagePath);
            var template = new Bitmap(exampleImage).ToImage<Bgr, byte>();

            entity.Tiles = new List<CustomItemTile>();

            #region Recognize

            var imgOut = new Mat();
            var imgNormalized = new Mat();
            double minVal = 0.0,
                maxVal = 0.0;
            var minLoc = new Point();
            var maxLoc = new Point();

            CvInvoke.MatchTemplate(_image, template, imgOut, TemplateMatchingType.Sqdiff);
            CvInvoke.Normalize(imgOut, imgNormalized, 0, 1, NormType.MinMax, DepthType.Cv64F);
            CvInvoke.MinMaxLoc(imgNormalized, ref minVal, ref maxVal, ref minLoc, ref maxLoc);
            Rectangle rect = new Rectangle(minLoc, template.Size);
            CvInvoke.Rectangle(_image, rect, new MCvScalar(255,0,0), 1);

            #endregion
            entity.Tiles.Add(new CustomItemTile(template.Height - 2, template.Width - 2, true) { indentX = minLoc.X, indentY = minLoc.Y });

            return entity;
        }


        private NewItemEntity RecognizeTiles(string exampleImagePath, NewItemEntity entity)
        {
            var exampleImage = (Bitmap)Image.FromFile(exampleImagePath);
            var template = new Bitmap(exampleImage).ToImage<Bgr, byte>();

            entity.Tiles = new List<NewItemTile>();

            #region Recognize

            var imgOut = new Mat();
            var imgNormalized = new Mat();
            double minVal = 0.0,
                maxVal = 0.0;
            var minLoc = new Point();
            var maxLoc = new Point();

            CvInvoke.MatchTemplate(_image, template, imgOut, TemplateMatchingType.Sqdiff);
            CvInvoke.Normalize(imgOut, imgNormalized, 0, 1, NormType.MinMax, DepthType.Cv64F);
            CvInvoke.MinMaxLoc(imgNormalized, ref minVal, ref maxVal, ref minLoc, ref maxLoc);
            Rectangle rect = new Rectangle(minLoc, template.Size);
            CvInvoke.Rectangle(_image, rect, new MCvScalar(255, 0, 0), 1);

            #endregion
            entity.Tiles.Add(new NewItemTile(template.Height - 10, template.Width - 10, true) { indentX = minLoc.X, indentY = minLoc.Y });

            return entity;
        }
    }

}