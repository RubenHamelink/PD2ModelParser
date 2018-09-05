// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.Animation
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.Collections.Generic;
using System.IO;

namespace PD2ModelParser.Sections
{
    public class Animation : Section
    {
        private static readonly uint animation_data_tag = 1572868536;
        public uint count;
        public ulong hashname;
        public uint id;
        public List<float> items = new List<float>();
        public float keyframe_length;
        public byte[] remaining_data;
        public uint size;
        public uint unknown2;

        public Animation()
        {
        }

        public Animation(BinaryReader instream, SectionHeader section)
        {
            StreamReadData(instream, section);
        }

        public sealed override void StreamReadData(BinaryReader instream, SectionHeader section)
        {
            id = section.id;
            size = section.size;
            hashname = instream.ReadUInt64();
            unknown2 = instream.ReadUInt32();
            keyframe_length = instream.ReadSingle();
            count = instream.ReadUInt32();
            List<float> floatList = new List<float>();
            for (int index = 0; (long) index < (long) count; ++index)
                items.Add(instream.ReadSingle());
            remaining_data = null;
            if (section.offset + 12L + section.size <= instream.BaseStream.Position)
                return;
            remaining_data =
                instream.ReadBytes((int) (section.offset + 12L + section.size - instream.BaseStream.Position));
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(animation_data_tag);
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
            outstream.Write(unknown2);
            outstream.Write(keyframe_length);
            outstream.Write(count);
            foreach (float num in items)
                outstream.Write(num);
            if (remaining_data == null)
                return;
            outstream.Write(remaining_data);
        }

        public override string ToString()
        {
            return "[Animation] ID: " + id + " size: " + size + " hashname: " +
                   StaticStorage.hashindex.GetString(hashname) + " unknown2: " + unknown2 + " keyframe_length: " +
                   keyframe_length + " count: " + count + " items: (count=" + items.Count + ")" +
                   (remaining_data != null ? " REMAINING DATA! " + remaining_data.Length + " bytes" : (object) "");
        }
    }
}