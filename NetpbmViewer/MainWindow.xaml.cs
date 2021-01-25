using Feep;
using SkiaSharp;
using System;
using System.Collections.Generic;
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
            var filename = args != null && args.Length > 1
                ? args[1]
                : @"C:\Users\Sean\source\repos\raytrace\t.ppm";

            var shite = NetpbmFile.Process(filename);
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
                //canvas.Clear(new SKColor(130, 130, 130));
                //canvas.DrawText("SkiaSharp in Wpf!", 50, 200, new SKPaint() { Color = new SKColor(0, 0, 0), TextSize = 100 });

                //var args = Environment.GetCommandLineArgs();
                //var argsString = args != null && args.Any()
                //    ? string.Join("~~~", args)
                //    : "No args provided";

                //canvas.DrawText(argsString, new SKPoint(50, 500), new SKPaint(new SKFont(SKTypeface.FromFamilyName("Microsoft YaHei UI")))
                //{
                //    Color = new SKColor(0, 0, 0),
                //    TextSize = 20
                //});

                canvas.DrawBitmap(image, new SKPoint(0, 0));
            }

            writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
            writeableBitmap.Unlock();
        }
    }
}
