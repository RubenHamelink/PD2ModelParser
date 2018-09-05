// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.PassthroughGP
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.IO;

namespace PD2ModelParser.Sections
{
  internal class PassthroughGP
  {
    private static uint passthroughGP_tag = 3819155914;
    public uint id;
    public uint size;
    public uint geometry_section;
    public uint topology_section;
    public byte[] remaining_data;

    public PassthroughGP(uint sec_id, uint geom_id, uint face_id)
    {
      this.id = sec_id;
      this.size = 8U;
      this.geometry_section = geom_id;
      this.topology_section = face_id;
    }

    public PassthroughGP(BinaryReader instream, SectionHeader section)
    {
      this.id = section.id;
      this.size = section.size;
      this.geometry_section = instream.ReadUInt32();
      this.topology_section = instream.ReadUInt32();
      this.remaining_data = (byte[]) null;
      if (section.offset + 12L + (long) section.size <= instream.BaseStream.Position)
        return;
      this.remaining_data = instream.ReadBytes((int) (section.offset + 12L + (long) section.size - instream.BaseStream.Position));
    }

    public void StreamWrite(BinaryWriter outstream)
    {
      outstream.Write(PassthroughGP.passthroughGP_tag);
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
      outstream.Write(this.geometry_section);
      outstream.Write(this.topology_section);
    }

    public override string ToString()
    {
      return "[PassthroughGP] ID: " + (object) this.id + " size: " + (object) this.size + " PassthroughGP_geometry_section: " + (object) this.geometry_section + " PassthroughGP_facelist_section: " + (object) this.topology_section + (this.remaining_data != null ? (object) (" REMAINING DATA! " + (object) this.remaining_data.Length + " bytes") : (object) "");
    }
  }
}
