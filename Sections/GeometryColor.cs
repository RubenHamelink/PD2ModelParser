// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.GeometryColor
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.IO;

namespace PD2ModelParser.Sections
{
  public class GeometryColor
  {
    public byte red;
    public byte green;
    public byte blue;
    public byte alpha;

    public GeometryColor(BinaryReader instream)
    {
      this.blue = instream.ReadByte();
      this.green = instream.ReadByte();
      this.red = instream.ReadByte();
      this.alpha = instream.ReadByte();
    }

    public void StreamWrite(BinaryWriter outstream)
    {
      outstream.Write(this.blue);
      outstream.Write(this.green);
      outstream.Write(this.red);
      outstream.Write(this.alpha);
    }

    public override string ToString()
    {
      return "{Red=" + (object) this.red + ", Green=" + (object) this.green + ", Blue=" + (object) this.blue + ", Alpha=" + (object) this.alpha + "}";
    }
  }
}
