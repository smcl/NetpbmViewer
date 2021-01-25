using Feep;
using SkiaSharp;
using System;
using System.Collections.Generic;
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

namespace Feep
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var args = Environment.GetCommandLineArgs();
            using var fileStream = args != null && args.Length > 1
                ? GetFileStream(args[1])
                : GetFeepStream();

            var shite = NetpbmFile.Process(fileStream);
            var writeableBitmap = new WriteableBitmap(shite.Width, shite.Height, 96, 96, PixelFormats.Bgra32, BitmapPalettes.Halftone256Transparent);
            DisplayBitmap(shite, writeableBitmap);
            Image.Source = writeableBitmap;
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            //var writeableBitmap = CreateImage(1920, 1080);
            //UpdateImage(writeableBitmap);
            //Image.Source = writeableBitmap;
        }

        private void DisplayBitmap(SKBitmap image, WriteableBitmap writeableBitmap)
        {
            int width = (int)writeableBitmap.Width,
                height = (int)writeableBitmap.Height;
            
            writeableBitmap.Lock();

            var skImageInfo = new SKImageInfo()
            {
                Width = width,
                Height = height,
                ColorType = SKColorType.Bgra8888,
                AlphaType = SKAlphaType.Premul,
                ColorSpace = SKColorSpace.CreateSrgb()
            };

            using (var surface = SKSurface.Create(skImageInfo, writeableBitmap.BackBuffer))
            {
                SKCanvas canvas = surface.Canvas;
                canvas.DrawBitmap(image, new SKPoint(0, 0));
            }

            writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
            writeableBitmap.Unlock();
        }

        private static Stream GetFileStream(string fileName)
        {
            return new FileStream(fileName, FileMode.Open);
        }

        private static Stream GetFeepStream()
        {
            var feepString = @"P2
12 14
15
0  0  0  0  0  0  0  0  0  0  0  0
0  5  5  5  5  0  0  7  7  7  7  0
0  5  0  0  0  0  0  7  0  0  0  0
0  5  5  5  0  0  0  7  7  7  0  0
0  5  0  0  0  0  0  7  0  0  0  0
0  5  0  0  0  0  0  7  7  7  7  0
0  0  0  0  0  0  0  0  0  0  0  0
0  0  0  0  0  0  0  0  0  0  0  0
0 11 11 11 11  0  0 15 15 15 15  0
0 11  0  0  0  0  0 15  0  0 15  0
0 11 11 11  0  0  0 15 15 15 15  0
0 11  0  0  0  0  0 15  0  0  0  0
0 11 11 11 11  0  0 15  0  0  0  0
0  0  0  0  0  0  0  0  0  0  0  0";

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(feepString);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
