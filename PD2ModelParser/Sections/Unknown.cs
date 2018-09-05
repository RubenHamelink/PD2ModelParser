// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.Unknown
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.IO;

namespace PD2ModelParser.Sections
{
  internal class Unknown
  {
    public uint id;
    public uint size;
    public uint tag;
    public byte[] data;

    public Unknown(BinaryReader instream, SectionHeader section)
    {
      this.id = section.id;
      this.size = section.size;
      this.tag = instream.ReadUInt32();
      instream.BaseStream.Position = section.offset + 12L;
      this.data = instream.ReadBytes((int) section.size);
    }

    public void StreamWrite(BinaryWriter outstream)
    {
      outstream.Write(this.tag);
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
      outstream.Write(this.data);
    }

    public override string ToString()
    {
      return "[UNKNOWN] ID: " + (object) this.id + " size: " + (object) this.size + " tag: " + (object) this.tag + " Unknown_data: " + (object) this.data.Length;
    }
  }
}
