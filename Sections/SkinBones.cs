// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.SkinBones
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using Nexus;
using System.Collections.Generic;
using System.IO;

namespace PD2ModelParser.Sections
{
  internal class SkinBones
  {
    private static uint skinbones_tag = 1707874341;
    public List<uint> objects = new List<uint>();
    public List<Matrix3D> rotations = new List<Matrix3D>();
    public Matrix3D unknown_matrix = new Matrix3D();
    public uint id;
    public uint size;
    public Bones bones;
    public uint object3D_section_id;
    public uint count;
    public byte[] remaining_data;

    public SkinBones(BinaryReader instream, SectionHeader section)
    {
      this.id = section.id;
      this.size = section.size;
      this.bones = new Bones(instream);
      this.object3D_section_id = instream.ReadUInt32();
      this.count = instream.ReadUInt32();
      for (int index = 0; (long) index < (long) this.count; ++index)
        this.objects.Add(instream.ReadUInt32());
      for (int index = 0; (long) index < (long) this.count; ++index)
        this.rotations.Add(new Matrix3D()
        {
          M11 = instream.ReadSingle(),
          M12 = instream.ReadSingle(),
          M13 = instream.ReadSingle(),
          M14 = instream.ReadSingle(),
          M21 = instream.ReadSingle(),
          M22 = instream.ReadSingle(),
          M23 = instream.ReadSingle(),
          M24 = instream.ReadSingle(),
          M31 = instream.ReadSingle(),
          M32 = instream.ReadSingle(),
          M33 = instream.ReadSingle(),
          M34 = instream.ReadSingle(),
          M41 = instream.ReadSingle(),
          M42 = instream.ReadSingle(),
          M43 = instream.ReadSingle(),
          M44 = instream.ReadSingle()
        });
      this.unknown_matrix.M11 = instream.ReadSingle();
      this.unknown_matrix.M12 = instream.ReadSingle();
      this.unknown_matrix.M13 = instream.ReadSingle();
      this.unknown_matrix.M14 = instream.ReadSingle();
      this.unknown_matrix.M21 = instream.ReadSingle();
      this.unknown_matrix.M22 = instream.ReadSingle();
      this.unknown_matrix.M23 = instream.ReadSingle();
      this.unknown_matrix.M24 = instream.ReadSingle();
      this.unknown_matrix.M31 = instream.ReadSingle();
      this.unknown_matrix.M32 = instream.ReadSingle();
      this.unknown_matrix.M33 = instream.ReadSingle();
      this.unknown_matrix.M34 = instream.ReadSingle();
      this.unknown_matrix.M41 = instream.ReadSingle();
      this.unknown_matrix.M42 = instream.ReadSingle();
      this.unknown_matrix.M43 = instream.ReadSingle();
      this.unknown_matrix.M44 = instream.ReadSingle();
      this.remaining_data = (byte[]) null;
      if (section.offset + 12L + (long) section.size <= instream.BaseStream.Position)
        return;
      this.remaining_data = instream.ReadBytes((int) (section.offset + 12L + (long) section.size - instream.BaseStream.Position));
    }

    public void StreamWrite(BinaryWriter outstream)
    {
      outstream.Write(SkinBones.skinbones_tag);
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
      this.bones.StreamWriteData(outstream);
      outstream.Write(this.object3D_section_id);
      outstream.Write(this.count);
      foreach (uint num in this.objects)
        outstream.Write(num);
      foreach (Matrix3D rotation in this.rotations)
      {
        outstream.Write(rotation.M11);
        outstream.Write(rotation.M12);
        outstream.Write(rotation.M13);
        outstream.Write(rotation.M14);
        outstream.Write(rotation.M21);
        outstream.Write(rotation.M22);
        outstream.Write(rotation.M23);
        outstream.Write(rotation.M24);
        outstream.Write(rotation.M31);
        outstream.Write(rotation.M32);
        outstream.Write(rotation.M33);
        outstream.Write(rotation.M34);
        outstream.Write(rotation.M41);
        outstream.Write(rotation.M42);
        outstream.Write(rotation.M43);
        outstream.Write(rotation.M44);
      }
      outstream.Write(this.unknown_matrix.M11);
      outstream.Write(this.unknown_matrix.M12);
      outstream.Write(this.unknown_matrix.M13);
      outstream.Write(this.unknown_matrix.M14);
      outstream.Write(this.unknown_matrix.M21);
      outstream.Write(this.unknown_matrix.M22);
      outstream.Write(this.unknown_matrix.M23);
      outstream.Write(this.unknown_matrix.M24);
      outstream.Write(this.unknown_matrix.M31);
      outstream.Write(this.unknown_matrix.M32);
      outstream.Write(this.unknown_matrix.M33);
      outstream.Write(this.unknown_matrix.M34);
      outstream.Write(this.unknown_matrix.M41);
      outstream.Write(this.unknown_matrix.M42);
      outstream.Write(this.unknown_matrix.M43);
      outstream.Write(this.unknown_matrix.M44);
      if (this.remaining_data == null)
        return;
      outstream.Write(this.remaining_data);
    }

    public override string ToString()
    {
      string str1 = this.objects.Count == 0 ? "none" : "";
      foreach (uint num in this.objects)
        str1 = str1 + (object) num + ", ";
      string str2 = this.rotations.Count == 0 ? "none" : "";
      foreach (Matrix3D rotation in this.rotations)
        str2 = str2 + (object) rotation + ", ";
      return "[SkinBones] ID: " + (object) this.id + " size: " + (object) this.size + " bones: [ " + (object) this.bones + " ] object3D_section_id: " + (object) this.object3D_section_id + " count: " + (object) this.count + " objects count: " + (object) this.objects.Count + " objects:[ " + str1 + " ] rotations count: " + (object) this.rotations.Count + " rotations:[ " + str2 + " ] unknown_matrix: " + (object) this.unknown_matrix + (this.remaining_data != null ? (object) (" REMAINING DATA! " + (object) this.remaining_data.Length + " bytes") : (object) "");
    }
  }
}
