using System.Collections;
using System.Text;

namespace AudioSteganography_Winforms
{
    internal static class Steganography
    {
        public static BitArray Encode(BitArray original, string message, UInt16 bitsPerSample)
        {
            var bitsMessage = new BitArray(Encoding.UTF8.GetBytes(message));
            var eof = new BitArray(Encoding.UTF8.GetBytes("\r\n"));
            var encodedData = original;
            int i = bitsPerSample - 1;
            for (int j = 0; j < bitsMessage.Count; i+=bitsPerSample, j++) 
            {
                encodedData[i] = bitsMessage[j];
            }

            for (int j = 0; j < eof.Count; i += bitsPerSample, j++)
            {
                encodedData[i] = eof[j];
            }
            return encodedData;
        }

        public static string Decode(BitArray encodedData, UInt16 bitsPerSample)
        {
            var bitsMessage = new BitArray(encodedData.Count);
            for (int i = bitsPerSample - 1, j = 0; i < encodedData.Count; i += bitsPerSample, j++)
            {
                bitsMessage[j] = encodedData[i];
            }
            byte[] byteMessage = new byte[(bitsMessage.Count + 7) / 8];
            bitsMessage.CopyTo(byteMessage, 0);
            var message = Encoding.UTF8.GetString(byteMessage);
            message = message.Substring(0, message.IndexOf("\r\n"));
            return message;
        }
    }
}
