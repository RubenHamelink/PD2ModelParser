// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.TopologyIP
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.IO;

namespace PD2ModelParser.Sections
{
  internal class TopologyIP
  {
    private static uint topologyIP_tag = 62272701;
    public uint id;
    public uint size;
    public uint sectionID;
    public byte[] remaining_data;

    public TopologyIP(uint sec_id, uint top_id)
    {
      this.id = sec_id;
      this.size = 0U;
      this.sectionID = top_id;
    }

    public TopologyIP(BinaryReader br, SectionHeader sh)
    {
      this.id = sh.id;
      this.size = sh.size;
      this.sectionID = br.ReadUInt32();
      this.remaining_data = (byte[]) null;
      if (sh.offset + 12L + (long) sh.size <= br.BaseStream.Position)
        return;
      this.remaining_data = br.ReadBytes((int) (sh.offset + 12L + (long) sh.size - br.BaseStream.Position));
    }

    public void StreamWrite(BinaryWriter outstream)
    {
      outstream.Write(TopologyIP.topologyIP_tag);
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
      outstream.Write(this.sectionID);
      if (this.remaining_data == null)
        return;
      outstream.Write(this.remaining_data);
    }

    public override string ToString()
    {
      return "[TopologyIP] ID: " + (object) this.id + " size: " + (object) this.size + " TopologyIP sectionID: " + (object) this.sectionID + (this.remaining_data != null ? (object) (" REMAINING DATA! " + (object) this.remaining_data.Length + " bytes") : (object) "");
    }
  }
}
