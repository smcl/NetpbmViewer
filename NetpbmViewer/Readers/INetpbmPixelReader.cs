using SkiaSharp;
using System.IO;

namespace NetpbmViewer.Readers
{
    public interface INetpbmPixelReader
    {
        SKColor ReadPixel(Stream stream);
        void SetColorMax(Stream stream);
    }
}
