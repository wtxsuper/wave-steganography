namespace AudioSteganography_Winforms
{
    internal static class WavWriter
    {
        public static void Write(string filepath, byte[] headerBytes, byte[] dataBytes)
        {
            var writer = new BinaryWriter(File.Open(filepath, FileMode.OpenOrCreate));
            writer.Write(headerBytes);
            writer.Write(dataBytes);
            writer.Close();
        }
    }
}
