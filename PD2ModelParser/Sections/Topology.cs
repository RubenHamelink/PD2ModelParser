// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.Topology
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.Collections.Generic;
using System.IO;

namespace PD2ModelParser.Sections
{
    internal class Topology : Section
    {
        private static readonly uint topology_tag = 1280342547;
        public uint count1;
        public uint count2;
        public List<Face> facelist = new List<Face>();
        public ulong hashname;
        public uint id;
        public byte[] items2;
        public byte[] remaining_data;
        public uint size;
        public uint unknown1;

        public Topology()
        {
        }

        public Topology(uint sec_id, obj_data obj)
        {
            id = sec_id;
            size = 0U;
            unknown1 = 0U;
            count1 = (uint) (obj.faces.Count / 3);
            facelist = obj.faces;
            count2 = 0U;
            items2 = new byte[0];
            hashname = Hash64.HashString(obj.object_name + ".Topology", 0UL);
        }

        public Topology(BinaryReader instream, SectionHeader section)
        {
            StreamReadData(instream, section);
        }

        public sealed override void StreamReadData(BinaryReader instream, SectionHeader section)
        {
            id = section.id;
            size = section.size;
            unknown1 = instream.ReadUInt32();
            count1 = instream.ReadUInt32();
            for (int index = 0; (long) index < (long) (count1 / 3U); ++index)
                facelist.Add(new Face
                {
                    x = instream.ReadUInt16(),
                    y = instream.ReadUInt16(),
                    z = instream.ReadUInt16()
                });
            count2 = instream.ReadUInt32();
            items2 = instream.ReadBytes((int) count2);
            hashname = instream.ReadUInt64();
            remaining_data = null;
            if (section.offset + 12L + section.size <= instream.BaseStream.Position)
                return;
            remaining_data =
                instream.ReadBytes((int) (section.offset + 12L + section.size - instream.BaseStream.Position));
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(topology_tag);
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
            outstream.Write(unknown1);
            outstream.Write(count1);
            foreach (Face face in facelist)
            {
                outstream.Write(face.x);
                outstream.Write(face.y);
                outstream.Write(face.z);
            }

            outstream.Write(count2);
            outstream.Write(items2);
            outstream.Write(hashname);
            if (remaining_data == null)
                return;
            outstream.Write(remaining_data);
        }

        public override string ToString()
        {
            return "[Topology] ID: " + id + " size: " + size + " unknown1: " + unknown1 + " count1: " + count1 +
                   " facelist: " + facelist.Count + " count2: " + count2 + " items2: " + items2.Length + " hashname: " +
                   StaticStorage.hashindex.GetString(hashname) + (remaining_data != null
                       ? " REMAINING DATA! " + remaining_data.Length + " bytes"
                       : (object) "");
        }
    }
}