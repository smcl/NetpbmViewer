using System;
using System.Collections.Generic;
using System.IO;

namespace NetpbmViewer
{
    /// <summary>
    /// Extension methods which simplify retrieving the next digit, byte, short or int from 
    /// an input Stream while taking into account variable whitespace and comments.
    /// TODO: comments 
    /// </summary>
    public static class StreamExtensions
    {
        public static byte ReadNextSingleDigit(this Stream stream)
        {
            return MunchWhitespaceUntilDigit(stream);
        }

        public static byte ReadNextByte(this Stream stream)
        {
            var bytes = new List<byte>
            {
                // advance the stream to the next non-whitespace
                stream.ReadNextSingleDigit()
            };

            // read bytes until we hit whitespace again
            byte b = (byte)stream.ReadByte();
            while (IsDigit(b))
            {
                bytes.Add(b);
                b = (byte)stream.ReadByte();
            }

            var byteString = System.Text.Encoding.UTF8.GetString(bytes.ToArray()).ToCharArray();
            if (byte.TryParse(byteString, out byte result))
            {
                return result;
            }

            throw new Exception($"Unable to parse sequence as byte: {byteString}");
        }

        // TODO: is this required?
        public static short ReadNextShort(this Stream stream)
        {
            throw new NotImplementedException();
        }

        public static int ReadNextInt(this Stream stream)
        {
            var bytes = new List<byte>
            {
                // advance the stream to the next non-whitespace
                stream.ReadNextSingleDigit()
            };

            // read bytes until we hit whitespace again
            byte b = (byte)stream.ReadByte();
            while (IsDigit(b))
            {
                bytes.Add(b);
                b = (byte)stream.ReadByte();
            }

            var byteString = System.Text.Encoding.UTF8.GetString(bytes.ToArray()).ToCharArray();
            if (int.TryParse(byteString, out int result))
            {
                return result;
            }

            throw new Exception($"Unable to parse sequence as int: {byteString}");
        }

        private static byte MunchWhitespaceUntilDigit(Stream stream)
        {
            while (true)
            {
                var b = (byte)stream.ReadByte();
                if (IsDigit(b))
                {
                    return b;
                }
                if (b == 255)
                {
                    throw new Exception("End of file reached");
                }
                else if (IsWhitespace(b))
                {
                    // nothing
                }
                else
                {
                    // ok we've hit something that's not whitespace OR a digit
                    // something fucky is afoot, so lets let throw
                    throw new Exception($"Unexpected character {b}");
                }
            }
        }

        private static bool IsWhitespace(byte b)
        {
            // TODO: identify ok separators in netpbm format, suspect it's just space, tab, CR, LF
            //       for now just pass it
            return true;
        }

        private static bool IsDigit(byte b)
        {
            return b >= '0' && b <= '9';
        }
    }
}
