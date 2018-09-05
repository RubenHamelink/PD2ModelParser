// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.Material_Group
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.Collections.Generic;
using System.IO;

namespace PD2ModelParser.Sections
{
  internal class Material_Group
  {
    private static uint material_group_tag = 690449181;
    public List<uint> items = new List<uint>();
    public uint id;
    public uint size;
    public uint count;
    public byte[] remaining_data;

    public Material_Group(uint sec_id, uint mat_id)
    {
      this.id = sec_id;
      this.size = 0U;
      this.count = 1U;
      this.items.Add(mat_id);
    }

    public Material_Group(BinaryReader instream, SectionHeader section)
    {
      this.id = section.id;
      this.size = section.size;
      this.count = instream.ReadUInt32();
      for (int index = 0; (long) index < (long) this.count; ++index)
        this.items.Add(instream.ReadUInt32());
      if (section.offset + 12L + (long) section.size <= instream.BaseStream.Position)
        return;
      instream.ReadBytes((int) (section.offset + 12L + (long) section.size - instream.BaseStream.Position));
    }

    public void StreamWrite(BinaryWriter outstream)
    {
      outstream.Write(Material_Group.material_group_tag);
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
      foreach (uint num in this.items)
        outstream.Write(num);
      if (this.remaining_data == null)
        return;
      outstream.Write(this.remaining_data);
    }

    public override string ToString()
    {
      string str = this.items.Count == 0 ? "none" : "";
      foreach (uint num in this.items)
        str = str + (object) num + ", ";
      return "[Material Group] ID: " + (object) this.id + " size: " + (object) this.size + " Count: " + (object) this.count + " Items: [ " + str + " ] " + (this.remaining_data != null ? (object) (" REMAINING DATA! " + (object) this.remaining_data.Length + " bytes") : (object) "");
    }
  }
}
