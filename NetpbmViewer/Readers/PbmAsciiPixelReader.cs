using SkiaSharp;
using System;
using System.IO;

namespace Feep.Readers
{
    public class PbmAsciiPixelReader : INetpbmPixelReader
    {
        public void SetColorMax(Stream stream)
        {
            // there is no "max" param in pbm files, it's either 0 or 1
        }

        public SKColor ReadPixel(Stream stream)
        {
            var tone = (char)stream.ReadNextSingleDigit();
            return tone switch
            {
                '0' => new SKColor(0, 0, 0),
                '1' => new SKColor(255, 255, 255),
                _ => throw new Exception($"Unable to map pixel PBM color: {tone}"),
            };
        }
    }
}
