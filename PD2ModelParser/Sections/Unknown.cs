using System.IO;

namespace PD2ModelParser.Sections
{
    internal class Unknown : Section
    {
        public byte[] data;
        public uint id;
        public uint size;
        public uint tag;

        public Unknown()
        {
        }

        public Unknown(BinaryReader instream, SectionHeader section)
        {
            StreamReadData(instream, section);
        }

        public sealed override void StreamReadData(BinaryReader instream, SectionHeader section)
        {
            id = section.id;
            size = section.size;
            tag = instream.ReadUInt32();
            instream.BaseStream.Position = section.offset + 12L;
            data = instream.ReadBytes((int) section.size);
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(tag);
            outstream.Write(id);
            long position1 = outstream.BaseStream.Position;
            outstream.Write(size);
            StreamWriteData(outstream);
            long position2 = outstream.BaseStream.Position;
            outstream.BaseStream.Position = position1;
            outstream.Write((uint) (position2 - (position1 + 4L)));
            outstream.BaseStream.Position = position2;
        }

        public void StreamWriteData(BinaryWriter outstream)
        {
            outstream.Write(data);
        }

        public override string ToString()
        {
            return "[UNKNOWN] ID: " + id + " size: " + size + " tag: " + tag + " Unknown_data: " + data.Length;
        }
    }
}