// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.Topology
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.Collections.Generic;
using System.IO;

namespace PD2ModelParser.Sections
{
  internal class Topology
  {
    private static uint topology_tag = 1280342547;
    public List<Face> facelist = new List<Face>();
    public uint id;
    public uint size;
    public uint unknown1;
    public uint count1;
    public uint count2;
    public byte[] items2;
    public ulong hashname;
    public byte[] remaining_data;

    public Topology(uint sec_id, obj_data obj)
    {
      this.id = sec_id;
      this.size = 0U;
      this.unknown1 = 0U;
      this.count1 = (uint) (obj.faces.Count / 3);
      this.facelist = obj.faces;
      this.count2 = 0U;
      this.items2 = new byte[0];
      this.hashname = Hash64.HashString(obj.object_name + ".Topology", 0UL);
    }

    public Topology(BinaryReader instream, SectionHeader section)
    {
      this.id = section.id;
      this.size = section.size;
      this.unknown1 = instream.ReadUInt32();
      this.count1 = instream.ReadUInt32();
      for (int index = 0; (long) index < (long) (this.count1 / 3U); ++index)
        this.facelist.Add(new Face()
        {
          x = instream.ReadUInt16(),
          y = instream.ReadUInt16(),
          z = instream.ReadUInt16()
        });
      this.count2 = instream.ReadUInt32();
      this.items2 = instream.ReadBytes((int) this.count2);
      this.hashname = instream.ReadUInt64();
      this.remaining_data = (byte[]) null;
      if (section.offset + 12L + (long) section.size <= instream.BaseStream.Position)
        return;
      this.remaining_data = instream.ReadBytes((int) (section.offset + 12L + (long) section.size - instream.BaseStream.Position));
    }

    public void StreamWrite(BinaryWriter outstream)
    {
      outstream.Write(Topology.topology_tag);
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
      outstream.Write(this.unknown1);
      outstream.Write(this.count1);
      foreach (Face face in this.facelist)
      {
        outstream.Write(face.x);
        outstream.Write(face.y);
        outstream.Write(face.z);
      }
      outstream.Write(this.count2);
      outstream.Write(this.items2);
      outstream.Write(this.hashname);
      if (this.remaining_data == null)
        return;
      outstream.Write(this.remaining_data);
    }

    public override string ToString()
    {
      return "[Topology] ID: " + (object) this.id + " size: " + (object) this.size + " unknown1: " + (object) this.unknown1 + " count1: " + (object) this.count1 + " facelist: " + (object) this.facelist.Count + " count2: " + (object) this.count2 + " items2: " + (object) this.items2.Length + " hashname: " + StaticStorage.hashindex.GetString(this.hashname) + (this.remaining_data != null ? (object) (" REMAINING DATA! " + (object) this.remaining_data.Length + " bytes") : (object) "");
    }
  }
}
