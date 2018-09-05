// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.Material
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.Collections.Generic;
using System.IO;

namespace PD2ModelParser.Sections
{
  internal class Material
  {
    private static uint material_tag = 1012162716;
    public List<MaterialItem> items = new List<MaterialItem>();
    public uint id;
    public uint size;
    public ulong hashname;
    public byte[] skipped;
    public uint count;
    public byte[] remaining_data;

    public Material(uint sec_id, string mat_name)
    {
      this.id = sec_id;
      this.size = 0U;
      this.hashname = Hash64.HashString(mat_name, 0UL);
      this.skipped = new byte[48];
      this.count = 0U;
    }

    public Material(BinaryReader instream, SectionHeader section)
    {
      this.id = section.id;
      this.size = section.size;
      this.hashname = instream.ReadUInt64();
      this.skipped = instream.ReadBytes(48);
      this.count = instream.ReadUInt32();
      for (int index = 0; (long) index < (long) this.count; ++index)
        this.items.Add(new MaterialItem()
        {
          unknown1 = instream.ReadUInt32(),
          unknown2 = instream.ReadUInt32()
        });
      this.remaining_data = (byte[]) null;
      if (section.offset + 12L + (long) section.size <= instream.BaseStream.Position)
        return;
      this.remaining_data = instream.ReadBytes((int) (section.offset + 12L + (long) section.size - instream.BaseStream.Position));
    }

    public void StreamWrite(BinaryWriter outstream)
    {
      outstream.Write(Material.material_tag);
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
      outstream.Write(this.skipped);
      outstream.Write(this.count);
      foreach (MaterialItem materialItem in this.items)
      {
        outstream.Write(materialItem.unknown1);
        outstream.Write(materialItem.unknown2);
      }
      if (this.remaining_data == null)
        return;
      outstream.Write(this.remaining_data);
    }

    public override string ToString()
    {
      string str = this.items.Count == 0 ? "none" : "";
      foreach (MaterialItem materialItem in this.items)
        str = str + (object) materialItem + ", ";
      return "[Material] ID: " + (object) this.id + " size: " + (object) this.size + " hashname: " + StaticStorage.hashindex.GetString(this.hashname) + " count: " + (object) this.count + " items: [ " + str + " ] " + (this.remaining_data != null ? (object) (" REMAINING DATA! " + (object) this.remaining_data.Length + " bytes") : (object) "");
    }
  }
}
