// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.GeometryWeightGroups
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.IO;

namespace PD2ModelParser.Sections
{
  public class GeometryWeightGroups
  {
    public ushort Bones1;
    public ushort Bones2;
    public ushort Bones3;
    public ushort Bones4;

    public GeometryWeightGroups(BinaryReader instream)
    {
      this.Bones1 = instream.ReadUInt16();
      this.Bones2 = instream.ReadUInt16();
      this.Bones3 = instream.ReadUInt16();
      this.Bones4 = instream.ReadUInt16();
    }

    public void StreamWrite(BinaryWriter outstream)
    {
      outstream.Write(this.Bones1);
      outstream.Write(this.Bones2);
      outstream.Write(this.Bones3);
      outstream.Write(this.Bones4);
    }

    public override string ToString()
    {
      return "{ Bones1=" + (object) this.Bones1 + ", Bones2=" + (object) this.Bones2 + ", Bones3=" + (object) this.Bones3 + ", Bones4=" + (object) this.Bones4 + " }";
    }
  }
}
