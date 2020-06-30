using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestTaskRecognizing.Api;
using TestTaskRecognizing.Entities;

namespace TestTaskRecognizing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Recognizer.Recognizer _recognizer;
        public MainWindow()
        {
            InitializeComponent();
            _recognizer = new Recognizer.Recognizer(ApiManager.GetImage());
            var page = _recognizer._page as CustomizeEntity;
            TilePosition.Content = page.Tiles.FirstOrDefault(x => x.IsActive).Position;
            ResultImage.Source = BitmapToImageSource((Bitmap)_recognizer.ResultImage);
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        private void StartTilePositionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int value = Int32.Parse(StartTilePositionTextBox.Text);
            RouteLabel.Content = _recognizer._page.FindShortestRoute(value);
            }
            catch (Exception exception)
            {
            }
        }
    }
}