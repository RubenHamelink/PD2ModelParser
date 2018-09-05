// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.Bones
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.Collections.Generic;
using System.IO;

namespace PD2ModelParser.Sections
{
    internal class Bones : Section
    {
        private static readonly uint bones_tag = 246692983;
        public List<Bone> bones = new List<Bone>();
        public uint count;
        public uint id;
        public byte[] remaining_data;
        public uint size;

        public Bones()
        {
        }

        public Bones(BinaryReader instream, SectionHeader section)
        {
            StreamReadData(instream, section);
        }

        public Bones(BinaryReader instream)
        {
            count = instream.ReadUInt32();
            for (int index1 = 0; (long) index1 < (long) count; ++index1)
            {
                Bone bone = new Bone();
                bone.vert_count = instream.ReadUInt32();
                for (int index2 = 0; (long) index2 < (long) bone.vert_count; ++index2)
                    bone.verts.Add(instream.ReadUInt32());
                bones.Add(bone);
            }

            remaining_data = null;
        }


        public sealed override void StreamReadData(BinaryReader instream, SectionHeader section)
        {
            id = section.id;
            size = section.size;
            count = instream.ReadUInt32();
            for (int index1 = 0; (long) index1 < (long) count; ++index1)
            {
                Bone bone = new Bone();
                bone.vert_count = instream.ReadUInt32();
                for (int index2 = 0; (long) index2 < (long) bone.vert_count; ++index2)
                    bone.verts.Add(instream.ReadUInt32());
                bones.Add(bone);
            }

            remaining_data = null;
            if (section.offset + 12L + section.size <= instream.BaseStream.Position)
                return;
            remaining_data =
                instream.ReadBytes((int) (section.offset + 12L + section.size - instream.BaseStream.Position));
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(bones_tag);
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
            outstream.Write(count);
            foreach (Bone bone in bones)
            {
                outstream.Write(bone.vert_count);
                foreach (uint vert in bone.verts)
                    outstream.Write(vert);
            }

            if (remaining_data == null)
                return;
            outstream.Write(remaining_data);
        }

        public override string ToString()
        {
            string str = bones.Count == 0 ? "none" : "";
            foreach (Bone bone in bones)
                str = str + bone + ", ";
            return "[Bones] ID: " + id + " size: " + size + " count: " + count + " bones:[ " + str + " ]" +
                   (remaining_data != null ? " REMAINING DATA! " + remaining_data.Length + " bytes" : (object) "");
        }
    }
}