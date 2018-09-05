// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.LinearVector3Controller_KeyFrame
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using Nexus;
using System.IO;

namespace PD2ModelParser.Sections
{
  public class LinearVector3Controller_KeyFrame
  {
    public float timestamp;
    public Vector3D vector;

    public LinearVector3Controller_KeyFrame(BinaryReader instream)
    {
      this.timestamp = instream.ReadSingle();
      this.vector = new Vector3D(instream.ReadSingle(), instream.ReadSingle(), instream.ReadSingle());
    }

    public void StreamWriteData(BinaryWriter outstream)
    {
      outstream.Write(this.timestamp);
      outstream.Write(this.vector.X);
      outstream.Write(this.vector.Y);
      outstream.Write(this.vector.Z);
    }

    public override string ToString()
    {
      return "Timestamp=" + (object) this.timestamp + " Vector=[X=" + (object) this.vector.X + ", Y=" + (object) this.vector.Y + ", Z=" + (object) this.vector.Z + "]";
    }
  }
}
