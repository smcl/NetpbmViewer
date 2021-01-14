using SkiaSharp;
using System.IO;

namespace NetpbmViewer.Readers
{
    public class PgmAsciiPixelReader : INetpbmPixelReader
    {
        private double _colorMax;

        public void SetColorMax(Stream stream)
        {
            _colorMax = stream.ReadNextInt();
        }

        public SKColor ReadPixel(Stream stream)
        {
            var rawTone = stream.ReadNextByte();
            var tone = ScaleTone(rawTone);
            return new SKColor(tone, tone, tone);
        }

        private byte ScaleTone(byte rawTone)
        {
            var scale = rawTone / _colorMax;
            return (byte)(scale * 255);
        }
    }
}
