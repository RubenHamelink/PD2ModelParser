// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.SectionHeader
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.IO;

namespace PD2ModelParser
{
  public class SectionHeader
  {
    public uint type;
    public uint id;
    public uint size;
    public long offset;

    public SectionHeader(uint sec_id)
    {
      this.id = sec_id;
    }

    public SectionHeader(BinaryReader instream)
    {
      this.offset = instream.BaseStream.Position;
      this.type = instream.ReadUInt32();
      this.id = instream.ReadUInt32();
      this.size = instream.ReadUInt32();
    }

    public override string ToString()
    {
      return "[SectionHeader] Type: " + (object) this.type + "  ID: " + (object) this.id + " Size: " + (object) this.size + " Offset: " + (object) this.offset;
    }
  }
}
