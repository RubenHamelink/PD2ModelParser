// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.Bone
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.Collections.Generic;

namespace PD2ModelParser.Sections
{
  internal class Bone
  {
    public List<uint> verts = new List<uint>();
    public uint vert_count;

    public override string ToString()
    {
      string str = this.verts.Count == 0 ? "none" : "";
      foreach (uint vert in this.verts)
        str = str + (object) vert + ", ";
      return "vert_count: " + (object) this.vert_count + " verts: [" + str + "]";
    }
  }
}
