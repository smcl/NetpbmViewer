using SkiaSharp;
using System.IO;

namespace Feep.Readers
{
    public interface INetpbmPixelReader
    {
        SKColor ReadPixel(Stream stream);
        void SetColorMax(Stream stream);
    }
}
