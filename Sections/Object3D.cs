// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.Object3D
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using Nexus;
using System.Collections.Generic;
using System.IO;

namespace PD2ModelParser.Sections
{
  internal class Object3D
  {
    private static uint object3D_tag = 268226816;
    public List<Vector3D> items = new List<Vector3D>();
    public Matrix3D rotation = new Matrix3D();
    public Vector3D position = new Vector3D();
    public uint id;
    public uint size;
    public ulong hashname;
    public uint count;
    public uint parentID;
    public byte[] remaining_data;

    public Object3D(string object_name, uint parent)
    {
      this.id = 0U;
      this.size = 0U;
      this.hashname = Hash64.HashString(object_name, 0UL);
      this.count = 0U;
      this.items = new List<Vector3D>();
      this.rotation = new Matrix3D(1f, 0.0f, 0.0f, 0.0f, 0.0f, 1f, 0.0f, 0.0f, 0.0f, 0.0f, 1f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
      this.position = new Vector3D(0.0f, 0.0f, 0.0f);
      this.parentID = parent;
    }

    public Object3D(BinaryReader instream, SectionHeader section)
    {
      this.id = section.id;
      this.size = section.size;
      this.hashname = instream.ReadUInt64();
      StaticStorage.objects_list.Add(StaticStorage.hashindex.GetString(this.hashname));
      this.count = instream.ReadUInt32();
      for (int index = 0; (long) index < (long) this.count; ++index)
        this.items.Add(new Vector3D()
        {
          X = instream.ReadSingle(),
          Y = instream.ReadSingle(),
          Z = instream.ReadSingle()
        });
      this.rotation.M11 = instream.ReadSingle();
      this.rotation.M12 = instream.ReadSingle();
      this.rotation.M13 = instream.ReadSingle();
      this.rotation.M14 = instream.ReadSingle();
      this.rotation.M21 = instream.ReadSingle();
      this.rotation.M22 = instream.ReadSingle();
      this.rotation.M23 = instream.ReadSingle();
      this.rotation.M24 = instream.ReadSingle();
      this.rotation.M31 = instream.ReadSingle();
      this.rotation.M32 = instream.ReadSingle();
      this.rotation.M33 = instream.ReadSingle();
      this.rotation.M34 = instream.ReadSingle();
      this.rotation.M41 = instream.ReadSingle();
      this.rotation.M42 = instream.ReadSingle();
      this.rotation.M43 = instream.ReadSingle();
      this.rotation.M44 = instream.ReadSingle();
      this.position.X = instream.ReadSingle();
      this.position.Y = instream.ReadSingle();
      this.position.Z = instream.ReadSingle();
      this.parentID = instream.ReadUInt32();
      this.remaining_data = (byte[]) null;
      if (section.offset + 12L + (long) section.size <= instream.BaseStream.Position)
        return;
      this.remaining_data = instream.ReadBytes((int) (section.offset + 12L + (long) section.size - instream.BaseStream.Position));
    }

    public Object3D(BinaryReader instream)
    {
      this.hashname = instream.ReadUInt64();
      this.count = instream.ReadUInt32();
      for (int index = 0; (long) index < (long) this.count; ++index)
        this.items.Add(new Vector3D()
        {
          X = instream.ReadSingle(),
          Y = instream.ReadSingle(),
          Z = instream.ReadSingle()
        });
      this.rotation.M11 = instream.ReadSingle();
      this.rotation.M12 = instream.ReadSingle();
      this.rotation.M13 = instream.ReadSingle();
      this.rotation.M14 = instream.ReadSingle();
      this.rotation.M21 = instream.ReadSingle();
      this.rotation.M22 = instream.ReadSingle();
      this.rotation.M23 = instream.ReadSingle();
      this.rotation.M24 = instream.ReadSingle();
      this.rotation.M31 = instream.ReadSingle();
      this.rotation.M32 = instream.ReadSingle();
      this.rotation.M33 = instream.ReadSingle();
      this.rotation.M34 = instream.ReadSingle();
      this.rotation.M41 = instream.ReadSingle();
      this.rotation.M42 = instream.ReadSingle();
      this.rotation.M43 = instream.ReadSingle();
      this.rotation.M44 = instream.ReadSingle();
      this.position.X = instream.ReadSingle();
      this.position.Y = instream.ReadSingle();
      this.position.Z = instream.ReadSingle();
      this.parentID = instream.ReadUInt32();
      this.remaining_data = (byte[]) null;
    }

    public void StreamWrite(BinaryWriter outstream)
    {
      outstream.Write(Object3D.object3D_tag);
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
      outstream.Write(this.hashname);
      outstream.Write(this.count);
      foreach (Vector3D vector3D in this.items)
      {
        outstream.Write(vector3D.X);
        outstream.Write(vector3D.Y);
        outstream.Write(vector3D.Z);
      }
      outstream.Write(this.rotation.M11);
      outstream.Write(this.rotation.M12);
      outstream.Write(this.rotation.M13);
      outstream.Write(this.rotation.M14);
      outstream.Write(this.rotation.M21);
      outstream.Write(this.rotation.M22);
      outstream.Write(this.rotation.M23);
      outstream.Write(this.rotation.M24);
      outstream.Write(this.rotation.M31);
      outstream.Write(this.rotation.M32);
      outstream.Write(this.rotation.M33);
      outstream.Write(this.rotation.M34);
      outstream.Write(this.rotation.M41);
      outstream.Write(this.rotation.M42);
      outstream.Write(this.rotation.M43);
      outstream.Write(this.rotation.M44);
      outstream.Write(this.position.X);
      outstream.Write(this.position.Y);
      outstream.Write(this.position.Z);
      outstream.Write(this.parentID);
      if (this.remaining_data == null)
        return;
      outstream.Write(this.remaining_data);
    }

    public override string ToString()
    {
      Vector3D scale = new Vector3D();
      Quaternion rotation = new Quaternion();
      Vector3D translation = new Vector3D();
      this.rotation.Decompose(out scale, out rotation, out translation);
      return "[Object3D] ID: " + (object) this.id + " size: " + (object) this.size + " hashname: " + StaticStorage.hashindex.GetString(this.hashname) + " count: " + (object) this.count + " items: " + (object) this.items.Count + " mat.scale: " + (object) scale + " mat.rotation: [x: " + (object) rotation.X + " y: " + (object) rotation.Y + " z: " + (object) rotation.Z + " w: " + (object) rotation.W + "] mat.position: " + (object) this.position + " position: [" + (object) this.position.X + " " + (object) this.position.Y + " " + (object) this.position.Z + "] Parent ID: " + (object) this.parentID + (this.remaining_data != null ? (object) (" REMAINING DATA! " + (object) this.remaining_data.Length + " bytes") : (object) "");
    }
  }
}
