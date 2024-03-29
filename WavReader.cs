using System.Collections;

namespace AudioSteganography_Winforms
{
    internal class WavReader
    {
        public byte[] chunkId;
        public UInt32 chunkSize;
        public byte[] format;
        public byte[] subchunk1Id;
        public UInt32 subchunk1Size;
        public UInt16 audioFormat;
        public UInt16 numChannels;
        public UInt32 sampleRate;
        public UInt32 byteRate;
        public UInt16 blockAlign;
        public UInt16 bitsPerSample;
        public byte[] subchunk2Id;
        public UInt32 subchunk2Size;
        private byte[] dataBytes;
        public BitArray dataBits;
        public byte[] headerBytes;
        public UInt32 samplesCount;
        private readonly BinaryReader reader;

        public WavReader(string filepath)
        {
            try
            {
                reader = new BinaryReader(File.Open(filepath, FileMode.Open));
                chunkId = reader.ReadBytes(4);
                chunkSize = reader.ReadUInt32();
                format = reader.ReadBytes(4);
                subchunk1Id = reader.ReadBytes(4);
                subchunk1Size = reader.ReadUInt32();
                audioFormat = reader.ReadUInt16();
                numChannels = reader.ReadUInt16();
                sampleRate = reader.ReadUInt32();
                byteRate = reader.ReadUInt32();
                blockAlign = reader.ReadUInt16();
                bitsPerSample = reader.ReadUInt16();
                subchunk2Id = reader.ReadBytes(4);
                subchunk2Size = reader.ReadUInt32();
                dataBytes = reader.ReadBytes(Convert.ToInt32(subchunk2Size));
                dataBits = new BitArray(dataBytes);
                samplesCount = subchunk2Size / bitsPerSample * 8;
                reader.BaseStream.Position = 0;
                headerBytes = reader.ReadBytes(44);
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null)
                    reader.Close();
                throw new Exception();
            }
        }
    }
}
