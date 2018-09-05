// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.ModelItem
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

namespace PD2ModelParser.Sections
{
  public class ModelItem
  {
    public uint unknown1;
    public uint vertCount;
    public uint unknown2;
    public uint faceCount;
    public uint material_id;

    public override string ToString()
    {
      return "{unknown1=" + (object) this.unknown1 + " vertCount=" + (object) this.vertCount + " unknown2=" + (object) this.unknown2 + " faceCount=" + (object) this.faceCount + " material_id=" + (object) this.material_id + "}";
    }
  }
}
