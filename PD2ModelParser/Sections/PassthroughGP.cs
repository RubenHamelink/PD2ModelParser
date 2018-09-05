// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.PassthroughGP
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.IO;

namespace PD2ModelParser.Sections
{
    internal class PassthroughGP : Section
    {
        private static readonly uint passthroughGP_tag = 3819155914;
        public uint geometry_section;
        public uint id;
        public byte[] remaining_data;
        public uint size;
        public uint topology_section;

        public PassthroughGP(uint sec_id, uint geom_id, uint face_id)
        {
            id = sec_id;
            size = 8U;
            geometry_section = geom_id;
            topology_section = face_id;
        }

        public PassthroughGP(BinaryReader instream, SectionHeader section)
        {
            StreamReadData(instream, section);
        }

        public PassthroughGP()
        {
        }

        public sealed override void StreamReadData(BinaryReader instream, SectionHeader section)
        {
            id = section.id;
            size = section.size;
            geometry_section = instream.ReadUInt32();
            topology_section = instream.ReadUInt32();
            remaining_data = null;
            if (section.offset + 12L + section.size <= instream.BaseStream.Position)
                return;
            remaining_data =
                instream.ReadBytes((int) (section.offset + 12L + section.size - instream.BaseStream.Position));
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(passthroughGP_tag);
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
            outstream.Write(geometry_section);
            outstream.Write(topology_section);
        }

        public override string ToString()
        {
            return "[PassthroughGP] ID: " + id + " size: " + size + " PassthroughGP_geometry_section: " +
                   geometry_section + " PassthroughGP_facelist_section: " + topology_section + (remaining_data != null
                       ? " REMAINING DATA! " + remaining_data.Length + " bytes"
                       : (object) "");
        }
    }
}