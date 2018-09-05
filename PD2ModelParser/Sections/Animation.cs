// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.Animation
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.Collections.Generic;
using System.IO;

namespace PD2ModelParser.Sections
{
  public class Animation
  {
    private static uint animation_data_tag = 1572868536;
    public List<float> items = new List<float>();
    public uint id;
    public uint size;
    public ulong hashname;
    public uint unknown2;
    public float keyframe_length;
    public uint count;
    public byte[] remaining_data;

    public Animation(BinaryReader instream, SectionHeader section)
    {
      this.id = section.id;
      this.size = section.size;
      this.hashname = instream.ReadUInt64();
      this.unknown2 = instream.ReadUInt32();
      this.keyframe_length = instream.ReadSingle();
      this.count = instream.ReadUInt32();
      List<float> floatList = new List<float>();
      for (int index = 0; (long) index < (long) this.count; ++index)
        this.items.Add(instream.ReadSingle());
      this.remaining_data = (byte[]) null;
      if (section.offset + 12L + (long) section.size <= instream.BaseStream.Position)
        return;
      this.remaining_data = instream.ReadBytes((int) (section.offset + 12L + (long) section.size - instream.BaseStream.Position));
    }

    public void StreamWrite(BinaryWriter outstream)
    {
      outstream.Write(Animation.animation_data_tag);
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
      outstream.Write(this.hashname);
      outstream.Write(this.unknown2);
      outstream.Write(this.keyframe_length);
      outstream.Write(this.count);
      foreach (float num in this.items)
        outstream.Write(num);
      if (this.remaining_data == null)
        return;
      outstream.Write(this.remaining_data);
    }

    public override string ToString()
    {
      return "[Animation] ID: " + (object) this.id + " size: " + (object) this.size + " hashname: " + StaticStorage.hashindex.GetString(this.hashname) + " unknown2: " + (object) this.unknown2 + " keyframe_length: " + (object) this.keyframe_length + " count: " + (object) this.count + " items: (count=" + (object) this.items.Count + ")" + (this.remaining_data != null ? (object) (" REMAINING DATA! " + (object) this.remaining_data.Length + " bytes") : (object) "");
    }
  }
}
