// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.Object3D
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.Collections.Generic;
using System.IO;
using Nexus;

namespace PD2ModelParser.Sections
{
    internal class Object3D : Section
    {
        private static readonly uint object3D_tag = 268226816;
        public uint count;
        public ulong hashname;
        public uint id;
        public List<Vector3D> items = new List<Vector3D>();
        public uint parentID;
        public Vector3D position;
        public byte[] remaining_data;
        public Matrix3D rotation;
        public uint size;

        public Object3D(string object_name, uint parent)
        {
            id = 0U;
            size = 0U;
            hashname = Hash64.HashString(object_name, 0UL);
            count = 0U;
            items = new List<Vector3D>();
            rotation = new Matrix3D(1f, 0.0f, 0.0f, 0.0f, 0.0f, 1f, 0.0f, 0.0f, 0.0f, 0.0f, 1f, 0.0f, 0.0f, 0.0f, 0.0f,
                0.0f);
            position = new Vector3D(0.0f, 0.0f, 0.0f);
            parentID = parent;
        }

        public Object3D(BinaryReader instream, SectionHeader section)
        {
            StreamReadData(instream, section);
        }

        public Object3D(BinaryReader instream)
        {
            hashname = instream.ReadUInt64();
            count = instream.ReadUInt32();
            for (int index = 0; (long) index < (long) count; ++index)
                items.Add(new Vector3D
                {
                    X = instream.ReadSingle(),
                    Y = instream.ReadSingle(),
                    Z = instream.ReadSingle()
                });
            rotation.M11 = instream.ReadSingle();
            rotation.M12 = instream.ReadSingle();
            rotation.M13 = instream.ReadSingle();
            rotation.M14 = instream.ReadSingle();
            rotation.M21 = instream.ReadSingle();
            rotation.M22 = instream.ReadSingle();
            rotation.M23 = instream.ReadSingle();
            rotation.M24 = instream.ReadSingle();
            rotation.M31 = instream.ReadSingle();
            rotation.M32 = instream.ReadSingle();
            rotation.M33 = instream.ReadSingle();
            rotation.M34 = instream.ReadSingle();
            rotation.M41 = instream.ReadSingle();
            rotation.M42 = instream.ReadSingle();
            rotation.M43 = instream.ReadSingle();
            rotation.M44 = instream.ReadSingle();
            position.X = instream.ReadSingle();
            position.Y = instream.ReadSingle();
            position.Z = instream.ReadSingle();
            parentID = instream.ReadUInt32();
            remaining_data = null;
        }

        public Object3D()
        {
        }

        public sealed override void StreamReadData(BinaryReader instream, SectionHeader section)
        {
            id = section.id;
            size = section.size;
            hashname = instream.ReadUInt64();
            StaticStorage.objects_list.Add(StaticStorage.hashindex.GetString(hashname));
            count = instream.ReadUInt32();
            for (int index = 0; (long) index < (long) count; ++index)
                items.Add(new Vector3D
                {
                    X = instream.ReadSingle(),
                    Y = instream.ReadSingle(),
                    Z = instream.ReadSingle()
                });
            rotation.M11 = instream.ReadSingle();
            rotation.M12 = instream.ReadSingle();
            rotation.M13 = instream.ReadSingle();
            rotation.M14 = instream.ReadSingle();
            rotation.M21 = instream.ReadSingle();
            rotation.M22 = instream.ReadSingle();
            rotation.M23 = instream.ReadSingle();
            rotation.M24 = instream.ReadSingle();
            rotation.M31 = instream.ReadSingle();
            rotation.M32 = instream.ReadSingle();
            rotation.M33 = instream.ReadSingle();
            rotation.M34 = instream.ReadSingle();
            rotation.M41 = instream.ReadSingle();
            rotation.M42 = instream.ReadSingle();
            rotation.M43 = instream.ReadSingle();
            rotation.M44 = instream.ReadSingle();
            position.X = instream.ReadSingle();
            position.Y = instream.ReadSingle();
            position.Z = instream.ReadSingle();
            parentID = instream.ReadUInt32();
            remaining_data = null;
            if (section.offset + 12L + section.size <= instream.BaseStream.Position)
                return;
            remaining_data =
                instream.ReadBytes((int) (section.offset + 12L + section.size - instream.BaseStream.Position));
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(object3D_tag);
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
            outstream.Write(hashname);
            outstream.Write(count);
            foreach (Vector3D vector3D in items)
            {
                outstream.Write(vector3D.X);
                outstream.Write(vector3D.Y);
                outstream.Write(vector3D.Z);
            }

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
            outstream.Write(position.X);
            outstream.Write(position.Y);
            outstream.Write(position.Z);
            outstream.Write(parentID);
            if (remaining_data == null)
                return;
            outstream.Write(remaining_data);
        }

        public override string ToString()
        {
            Vector3D scale = new Vector3D();
            Quaternion rotation = new Quaternion();
            Vector3D translation = new Vector3D();
            this.rotation.Decompose(out scale, out rotation, out translation);
            return "[Object3D] ID: " + id + " size: " + size + " hashname: " +
                   StaticStorage.hashindex.GetString(hashname) + " count: " + count + " items: " + items.Count +
                   " mat.scale: " + scale + " mat.rotation: [x: " + rotation.X + " y: " + rotation.Y + " z: " +
                   rotation.Z + " w: " + rotation.W + "] mat.position: " + position + " position: [" + position.X +
                   " " + position.Y + " " + position.Z + "] Parent ID: " + parentID + (remaining_data != null
                       ? " REMAINING DATA! " + remaining_data.Length + " bytes"
                       : (object) "");
        }
    }
}