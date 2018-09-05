// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.obj_data
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using Nexus;
using PD2ModelParser.Sections;
using System.Collections.Generic;

namespace PD2ModelParser
{
  internal class obj_data
  {
    public List<Vector3D> verts { get; set; }

    public List<Vector2D> uv { get; set; }

    public List<Vector3D> normals { get; set; }

    public string object_name { get; set; }

    public List<Face> faces { get; set; }

    public string material_name { get; set; }

    public Dictionary<string, List<int>> shading_groups { get; set; }

    public obj_data()
    {
      this.verts = new List<Vector3D>();
      this.uv = new List<Vector2D>();
      this.normals = new List<Vector3D>();
      this.object_name = "";
      this.faces = new List<Face>();
      this.material_name = "";
      this.shading_groups = new Dictionary<string, List<int>>();
    }
  }
}
