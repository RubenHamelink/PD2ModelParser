using System.Collections.Generic;
using System.IO;
using Nexus;

namespace PD2ModelParser.Sections
{
    internal class SkinBones : Section
    {
        private static readonly uint skinbones_tag = 1707874341;
        public Bones bones;
        public uint count;
        public uint id;
        public uint object3D_section_id;
        public List<uint> objects = new List<uint>();
        public byte[] remaining_data;
        public List<Matrix3D> rotations = new List<Matrix3D>();
        public uint size;
        public Matrix3D unknown_matrix;

        public SkinBones()
        {
        }

        public SkinBones(BinaryReader instream, SectionHeader section)
        {
            StreamReadData(instream, section);
        }

        public sealed override void StreamReadData(BinaryReader instream, SectionHeader section)
        {
            id = section.id;
            size = section.size;
            bones = new Bones(instream);
            object3D_section_id = instream.ReadUInt32();
            count = instream.ReadUInt32();
            for (int index = 0; (long) index < (long) count; ++index)
                objects.Add(instream.ReadUInt32());
            for (int index = 0; (long) index < (long) count; ++index)
                rotations.Add(new Matrix3D
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
            unknown_matrix.M11 = instream.ReadSingle();
            unknown_matrix.M12 = instream.ReadSingle();
            unknown_matrix.M13 = instream.ReadSingle();
            unknown_matrix.M14 = instream.ReadSingle();
            unknown_matrix.M21 = instream.ReadSingle();
            unknown_matrix.M22 = instream.ReadSingle();
            unknown_matrix.M23 = instream.ReadSingle();
            unknown_matrix.M24 = instream.ReadSingle();
            unknown_matrix.M31 = instream.ReadSingle();
            unknown_matrix.M32 = instream.ReadSingle();
            unknown_matrix.M33 = instream.ReadSingle();
            unknown_matrix.M34 = instream.ReadSingle();
            unknown_matrix.M41 = instream.ReadSingle();
            unknown_matrix.M42 = instream.ReadSingle();
            unknown_matrix.M43 = instream.ReadSingle();
            unknown_matrix.M44 = instream.ReadSingle();
            remaining_data = null;
            if (section.offset + 12L + section.size <= instream.BaseStream.Position)
                return;
            remaining_data =
                instream.ReadBytes((int) (section.offset + 12L + section.size - instream.BaseStream.Position));
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(skinbones_tag);
            outstream.Write(id);
            long position1 = outstream.BaseStream.Position;
            outstream.Write(size);
            StreamWriteData(outstream);
            long position2 = outstream.BaseStream.Position;
            outstream.BaseStream.Position = position1;
            outstream.Write((uint) (position2 - (position1 + 4L)));
            outstream.BaseStream.Position = position2;
        }

        public void StreamWriteData(BinaryWriter outstream)
        {
            bones.StreamWriteData(outstream);
            outstream.Write(object3D_section_id);
            outstream.Write(count);
            foreach (uint num in objects)
                outstream.Write(num);
            foreach (Matrix3D rotation in rotations)
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

            outstream.Write(unknown_matrix.M11);
            outstream.Write(unknown_matrix.M12);
            outstream.Write(unknown_matrix.M13);
            outstream.Write(unknown_matrix.M14);
            outstream.Write(unknown_matrix.M21);
            outstream.Write(unknown_matrix.M22);
            outstream.Write(unknown_matrix.M23);
            outstream.Write(unknown_matrix.M24);
            outstream.Write(unknown_matrix.M31);
            outstream.Write(unknown_matrix.M32);
            outstream.Write(unknown_matrix.M33);
            outstream.Write(unknown_matrix.M34);
            outstream.Write(unknown_matrix.M41);
            outstream.Write(unknown_matrix.M42);
            outstream.Write(unknown_matrix.M43);
            outstream.Write(unknown_matrix.M44);
            if (remaining_data == null)
                return;
            outstream.Write(remaining_data);
        }

        public override string ToString()
        {
            string str1 = objects.Count == 0 ? "none" : "";
            foreach (uint num in objects)
                str1 = str1 + num + ", ";
            string str2 = rotations.Count == 0 ? "none" : "";
            foreach (Matrix3D rotation in rotations)
                str2 = str2 + rotation + ", ";
            return "[SkinBones] ID: " + id + " size: " + size + " bones: [ " + bones + " ] object3D_section_id: " +
                   object3D_section_id + " count: " + count + " objects count: " + objects.Count + " objects:[ " +
                   str1 + " ] rotations count: " + rotations.Count + " rotations:[ " + str2 + " ] unknown_matrix: " +
                   unknown_matrix + (remaining_data != null
                       ? " REMAINING DATA! " + remaining_data.Length + " bytes"
                       : (object) "");
        }
    }
}