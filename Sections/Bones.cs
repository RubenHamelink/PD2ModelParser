// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.Bones
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.Collections.Generic;
using System.IO;

namespace PD2ModelParser.Sections
{
  internal class Bones
  {
    private static uint bones_tag = 246692983;
    public List<Bone> bones = new List<Bone>();
    public uint id;
    public uint size;
    public uint count;
    public byte[] remaining_data;

    public Bones(BinaryReader instream, SectionHeader section)
    {
      this.id = section.id;
      this.size = section.size;
      this.count = instream.ReadUInt32();
      for (int index1 = 0; (long) index1 < (long) this.count; ++index1)
      {
        Bone bone = new Bone();
        bone.vert_count = instream.ReadUInt32();
        for (int index2 = 0; (long) index2 < (long) bone.vert_count; ++index2)
          bone.verts.Add(instream.ReadUInt32());
        this.bones.Add(bone);
      }
      this.remaining_data = (byte[]) null;
      if (section.offset + 12L + (long) section.size <= instream.BaseStream.Position)
        return;
      this.remaining_data = instream.ReadBytes((int) (section.offset + 12L + (long) section.size - instream.BaseStream.Position));
    }

    public Bones(BinaryReader instream)
    {
      this.count = instream.ReadUInt32();
      for (int index1 = 0; (long) index1 < (long) this.count; ++index1)
      {
        Bone bone = new Bone();
        bone.vert_count = instream.ReadUInt32();
        for (int index2 = 0; (long) index2 < (long) bone.vert_count; ++index2)
          bone.verts.Add(instream.ReadUInt32());
        this.bones.Add(bone);
      }
      this.remaining_data = (byte[]) null;
    }

    public void StreamWrite(BinaryWriter outstream)
    {
      outstream.Write(Bones.bones_tag);
      outstream.Write(this.id);
      long position1 = outstream.BaseStream.Position;
      outstream.Write(this.size);
      this.StreamWriteData(outstream);
      long position2 = outstream.BaseStream.Position;
      outstream.BaseStream.Position = position1;
      outstream.Write((uint) (position2 - (position1 + 4L)));
      outstream.BaseStream.Position = position2;
    }

    public void StreamWriteData(BinaryWriter outstream)
    {
      outstream.Write(this.count);
      foreach (Bone bone in this.bones)
      {
        outstream.Write(bone.vert_count);
        foreach (uint vert in bone.verts)
          outstream.Write(vert);
      }
      if (this.remaining_data == null)
        return;
      outstream.Write(this.remaining_data);
    }

    public override string ToString()
    {
      string str = this.bones.Count == 0 ? "none" : "";
      foreach (Bone bone in this.bones)
        str = str + (object) bone + ", ";
      return "[Bones] ID: " + (object) this.id + " size: " + (object) this.size + " count: " + (object) this.count + " bones:[ " + str + " ]" + (this.remaining_data != null ? (object) (" REMAINING DATA! " + (object) this.remaining_data.Length + " bytes") : (object) "");
    }
  }
}
