// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.QuatLinearRotationController_KeyFrame
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using Nexus;
using System.IO;

namespace PD2ModelParser.Sections
{
  public class QuatLinearRotationController_KeyFrame
  {
    public float timestamp;
    public Quaternion rotation;

    public QuatLinearRotationController_KeyFrame(BinaryReader instream)
    {
      this.timestamp = instream.ReadSingle();
      this.rotation = new Quaternion(instream.ReadSingle(), instream.ReadSingle(), instream.ReadSingle(), instream.ReadSingle());
    }

    public void StreamWriteData(BinaryWriter outstream)
    {
      outstream.Write(this.timestamp);
      outstream.Write(this.rotation.X);
      outstream.Write(this.rotation.Y);
      outstream.Write(this.rotation.Z);
      outstream.Write(this.rotation.W);
    }

    public override string ToString()
    {
      return "Timestamp=" + (object) this.timestamp + " Rotation=[X=" + (object) this.rotation.X + ", Y=" + (object) this.rotation.Y + ", Z=" + (object) this.rotation.Z + ", W=" + (object) this.rotation.W + "]";
    }
  }
}
