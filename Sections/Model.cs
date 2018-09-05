// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.Model
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using Nexus;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PD2ModelParser.Sections
{
  internal class Model
  {
    private static uint model_data_tag = 1646341512;
    public Vector3D v6_unknown5 = new Vector3D();
    public Vector3D v6_unknown6 = new Vector3D();
    public List<ModelItem> items = new List<ModelItem>();
    public uint id;
    public uint size;
    public Object3D object3D;
    public uint version;
    public uint v6_unknown7;
    public uint v6_unknown8;
    public uint passthroughGP_ID;
    public uint topologyIP_ID;
    public uint count;
    public uint material_group_section_id;
    public uint unknown10;
    public Vector3D bounds_min;
    public Vector3D bounds_max;
    public uint unknown11;
    public uint unknown12;
    public uint unknown13;
    public uint skinbones_ID;
    public byte[] remaining_data;

    public Model(obj_data obj, uint passGP_ID, uint topoIP_ID, uint matg_id)
    {
      this.id = (uint) obj.object_name.GetHashCode();
      this.size = 0U;
      this.object3D = new Object3D(obj.object_name, StaticStorage.rp_id);
      this.version = 3U;
      this.passthroughGP_ID = passGP_ID;
      this.topologyIP_ID = topoIP_ID;
      this.count = 1U;
      this.items = new List<ModelItem>();
      this.items.Add(new ModelItem()
      {
        unknown1 = 0U,
        vertCount = (uint) obj.verts.Count,
        unknown2 = 0U,
        faceCount = (uint) obj.faces.Count,
        material_id = 0U
      });
      this.material_group_section_id = matg_id;
      this.unknown10 = 0U;
      this.bounds_min.Z = 0.0f;
      this.bounds_min.X = 0.0f;
      this.bounds_min.Y = 0.0f;
      this.bounds_max.Z = 0.0f;
      this.bounds_max.X = 0.0f;
      this.bounds_max.Y = 0.0f;
      this.unknown11 = 0U;
      this.unknown12 = 1U;
      this.unknown13 = 6U;
      this.skinbones_ID = 0U;
    }

    public Model(BinaryReader instream, SectionHeader section)
    {
      this.id = section.id;
      this.size = section.size;
      this.object3D = new Object3D(instream);
      this.version = instream.ReadUInt32();
      if (this.version == 6U)
      {
        this.v6_unknown5.X = instream.ReadSingle();
        this.v6_unknown5.Y = instream.ReadSingle();
        this.v6_unknown5.Z = instream.ReadSingle();
        this.v6_unknown6.X = instream.ReadSingle();
        this.v6_unknown6.Y = instream.ReadSingle();
        this.v6_unknown6.Z = instream.ReadSingle();
        this.v6_unknown7 = instream.ReadUInt32();
        this.v6_unknown8 = instream.ReadUInt32();
      }
      else
      {
        this.passthroughGP_ID = instream.ReadUInt32();
        this.topologyIP_ID = instream.ReadUInt32();
        this.count = instream.ReadUInt32();
        for (int index = 0; (long) index < (long) this.count; ++index)
          this.items.Add(new ModelItem()
          {
            unknown1 = instream.ReadUInt32(),
            vertCount = instream.ReadUInt32(),
            unknown2 = instream.ReadUInt32(),
            faceCount = instream.ReadUInt32(),
            material_id = instream.ReadUInt32()
          });
        this.material_group_section_id = instream.ReadUInt32();
        this.unknown10 = instream.ReadUInt32();
        this.bounds_min.Z = instream.ReadSingle();
        this.bounds_min.X = instream.ReadSingle();
        this.bounds_min.Y = instream.ReadSingle();
        this.bounds_max.Z = instream.ReadSingle();
        this.bounds_max.X = instream.ReadSingle();
        this.bounds_max.Y = instream.ReadSingle();
        this.unknown11 = instream.ReadUInt32();
        this.unknown12 = instream.ReadUInt32();
        this.unknown13 = instream.ReadUInt32();
        this.skinbones_ID = instream.ReadUInt32();
      }
      this.remaining_data = (byte[]) null;
      if (section.offset + 12L + (long) section.size <= instream.BaseStream.Position)
        return;
      this.remaining_data = instream.ReadBytes((int) (section.offset + 12L + (long) section.size - instream.BaseStream.Position));
    }

    public void StreamWrite(BinaryWriter outstream)
    {
      outstream.Write(Model.model_data_tag);
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
      this.object3D.StreamWriteData(outstream);
      outstream.Write(this.version);
      if (this.version == 6U)
      {
        outstream.Write(this.v6_unknown5.X);
        outstream.Write(this.v6_unknown5.Y);
        outstream.Write(this.v6_unknown5.Z);
        outstream.Write(this.v6_unknown6.X);
        outstream.Write(this.v6_unknown6.Y);
        outstream.Write(this.v6_unknown6.Z);
        outstream.Write(this.v6_unknown7);
        outstream.Write(this.v6_unknown8);
      }
      else
      {
        outstream.Write(this.passthroughGP_ID);
        outstream.Write(this.topologyIP_ID);
        outstream.Write(this.count);
        foreach (ModelItem modelItem in this.items)
        {
          outstream.Write(modelItem.unknown1);
          outstream.Write(modelItem.vertCount);
          outstream.Write(modelItem.unknown2);
          outstream.Write(modelItem.faceCount);
          outstream.Write(modelItem.material_id);
        }
        outstream.Write(this.material_group_section_id);
        outstream.Write(this.unknown10);
        outstream.Write(this.bounds_min.Z);
        outstream.Write(this.bounds_min.X);
        outstream.Write(this.bounds_min.Y);
        outstream.Write(this.bounds_max.Z);
        outstream.Write(this.bounds_max.X);
        outstream.Write(this.bounds_max.Y);
        outstream.Write(this.unknown11);
        outstream.Write(this.unknown12);
        outstream.Write(this.unknown13);
        outstream.Write(this.skinbones_ID);
      }
      if (this.remaining_data == null)
        return;
      outstream.Write(this.remaining_data);
    }

    public override string ToString()
    {
      if (this.version == 6U)
        return "[Model_v6] ID: " + (object) this.id + " size: " + (object) this.size + " Object3D: [ " + (object) this.object3D + " ] version: " + (object) this.version + " unknown5: " + (object) this.v6_unknown5 + " unknown6: " + (object) this.v6_unknown6 + " unknown7: " + (object) this.v6_unknown7 + " unknown8: " + (object) this.v6_unknown8 + (this.remaining_data != null ? (object) (" REMAINING DATA! " + (object) this.remaining_data.Length + " bytes") : (object) "");
      StringBuilder stringBuilder = new StringBuilder();
      bool flag = true;
      foreach (ModelItem modelItem in this.items)
      {
        stringBuilder.Append((flag ? "" : ", ") + modelItem.ToString());
        flag = false;
      }
      return "[Model] ID: " + (object) this.id + " size: " + (object) this.size + " Object3D: [ " + (object) this.object3D + " ] version: " + (object) this.version + " passthroughGP_ID: " + (object) this.passthroughGP_ID + " topologyIP_ID: " + (object) this.topologyIP_ID + " count: " + (object) this.count + " items: [" + (object) stringBuilder + "] material_group_section_id: " + (object) this.material_group_section_id + " unknown10: " + (object) this.unknown10 + " bounds_min: " + (object) this.bounds_min + " bounds_max: " + (object) this.bounds_max + " unknown11: " + (object) this.unknown11 + " unknown12: " + (object) this.unknown12 + " unknown13: " + (object) this.unknown13 + " skinbones_ID: " + (object) this.skinbones_ID + (this.remaining_data != null ? (object) (" REMAINING DATA! " + (object) this.remaining_data.Length + " bytes") : (object) "");
    }
  }
}
