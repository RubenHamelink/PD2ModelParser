using System.Collections.Generic;
using System.IO;

namespace PD2ModelParser.Sections
{
    internal class Material : Section
    {
        private static readonly uint material_tag = 1012162716;
        public uint count;
        public ulong hashname;
        public uint id;
        public List<MaterialItem> items = new List<MaterialItem>();
        public byte[] remaining_data;
        public uint size;
        public byte[] skipped;

        public Material(uint sec_id, string mat_name)
        {
            id = sec_id;
            size = 0U;
            hashname = Hash64.HashString(mat_name, 0UL);
            skipped = new byte[48];
            count = 0U;
        }

        public Material()
        {
        }

        public Material(BinaryReader instream, SectionHeader section)
        {
            StreamReadData(instream, section);
        }

        public sealed override void StreamReadData(BinaryReader instream, SectionHeader section)
        {
            id = section.id;
            size = section.size;
            hashname = instream.ReadUInt64();
            skipped = instream.ReadBytes(48);
            count = instream.ReadUInt32();
            for (int index = 0; (long) index < (long) count; ++index)
                items.Add(new MaterialItem
                {
                    unknown1 = instream.ReadUInt32(),
                    unknown2 = instream.ReadUInt32()
                });
            remaining_data = null;
            if (section.offset + 12L + section.size <= instream.BaseStream.Position)
                return;
            remaining_data =
                instream.ReadBytes((int) (section.offset + 12L + section.size - instream.BaseStream.Position));
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(material_tag);
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
            outstream.Write(hashname);
            outstream.Write(skipped);
            outstream.Write(count);
            foreach (MaterialItem materialItem in items)
            {
                outstream.Write(materialItem.unknown1);
                outstream.Write(materialItem.unknown2);
            }

            if (remaining_data == null)
                return;
            outstream.Write(remaining_data);
        }

        public override string ToString()
        {
            string str = items.Count == 0 ? "none" : "";
            foreach (MaterialItem materialItem in items)
                str = str + materialItem + ", ";
            return "[Material] ID: " + id + " size: " + size + " hashname: " +
                   StaticStorage.hashindex.GetString(hashname) + " count: " + count + " items: [ " +
                   str + " ] " + (remaining_data != null
                       ? " REMAINING DATA! " + remaining_data.Length + " bytes"
                       : (object) "");
        }
    }
}