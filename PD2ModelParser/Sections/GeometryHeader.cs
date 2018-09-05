// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.GeometryHeader
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

namespace PD2ModelParser.Sections
{
  public class GeometryHeader
  {
    public uint item_size;
    public uint item_type;

    public GeometryHeader()
    {
    }

    public GeometryHeader(uint size, uint type)
    {
      this.item_size = size;
      this.item_type = type;
    }
  }
}
