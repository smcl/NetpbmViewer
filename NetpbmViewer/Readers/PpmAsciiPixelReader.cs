using SkiaSharp;
using System;
using System.IO;

namespace NetpbmViewer.Readers
{
    public class PpmAsciiPixelReader : INetpbmPixelReader
    {
        private double _colorMax;

        public void SetColorMax(Stream stream)
        {
            _colorMax = stream.ReadNextInt();
        }

        public SKColor ReadPixel(Stream stream)
        {
            var r = ScaleTone(stream.ReadNextByte());
            var g = ScaleTone(stream.ReadNextByte());
            var b = ScaleTone(stream.ReadNextByte());

            return new SKColor(r, g, b);
        }

        private byte ScaleTone(byte rawTone)
        {
            var scale = rawTone / _colorMax;
            return (byte)(scale * 255);
        }
    }
}
