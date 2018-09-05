using System.Collections.Generic;
using System.IO;
using System.Text;
using Nexus;

namespace PD2ModelParser.Sections
{
    internal class Model : Section
    {
        private static readonly uint model_data_tag = 1646341512;
        public Vector3D bounds_max;
        public Vector3D bounds_min;
        public uint count;
        public uint id;
        public List<ModelItem> items = new List<ModelItem>();
        public uint material_group_section_id;
        public Object3D object3D;
        public uint passthroughGP_ID;
        public byte[] remaining_data;
        public uint size;
        public uint skinbones_ID;
        public uint topologyIP_ID;
        public uint unknown10;
        public uint unknown11;
        public uint unknown12;
        public uint unknown13;
        public Vector3D v6_unknown5;
        public Vector3D v6_unknown6;
        public uint v6_unknown7;
        public uint v6_unknown8;
        public uint version;

        public Model(obj_data obj, uint passGP_ID, uint topoIP_ID, uint matg_id)
        {
            id = (uint) obj.object_name.GetHashCode();
            size = 0U;
            object3D = new Object3D(obj.object_name, StaticStorage.rp_id);
            version = 3U;
            passthroughGP_ID = passGP_ID;
            topologyIP_ID = topoIP_ID;
            count = 1U;
            items = new List<ModelItem>();
            items.Add(new ModelItem
            {
                unknown1 = 0U,
                vertCount = (uint) obj.verts.Count,
                unknown2 = 0U,
                faceCount = (uint) obj.faces.Count,
                material_id = 0U
            });
            material_group_section_id = matg_id;
            unknown10 = 0U;
            bounds_min.Z = 0.0f;
            bounds_min.X = 0.0f;
            bounds_min.Y = 0.0f;
            bounds_max.Z = 0.0f;
            bounds_max.X = 0.0f;
            bounds_max.Y = 0.0f;
            unknown11 = 0U;
            unknown12 = 1U;
            unknown13 = 6U;
            skinbones_ID = 0U;
        }

        public Model(BinaryReader instream, SectionHeader section)
        {
            StreamReadData(instream, section);
        }

        public Model()
        {
        }

        public sealed override void StreamReadData(BinaryReader instream, SectionHeader section)
        {
            id = section.id;
            size = section.size;
            object3D = new Object3D(instream);
            version = instream.ReadUInt32();
            if (version == 6U)
            {
                v6_unknown5.X = instream.ReadSingle();
                v6_unknown5.Y = instream.ReadSingle();
                v6_unknown5.Z = instream.ReadSingle();
                v6_unknown6.X = instream.ReadSingle();
                v6_unknown6.Y = instream.ReadSingle();
                v6_unknown6.Z = instream.ReadSingle();
                v6_unknown7 = instream.ReadUInt32();
                v6_unknown8 = instream.ReadUInt32();
            }
            else
            {
                passthroughGP_ID = instream.ReadUInt32();
                topologyIP_ID = instream.ReadUInt32();
                count = instream.ReadUInt32();
                for (int index = 0; (long) index < (long) count; ++index)
                    items.Add(new ModelItem
                    {
                        unknown1 = instream.ReadUInt32(),
                        vertCount = instream.ReadUInt32(),
                        unknown2 = instream.ReadUInt32(),
                        faceCount = instream.ReadUInt32(),
                        material_id = instream.ReadUInt32()
                    });
                material_group_section_id = instream.ReadUInt32();
                unknown10 = instream.ReadUInt32();
                bounds_min.Z = instream.ReadSingle();
                bounds_min.X = instream.ReadSingle();
                bounds_min.Y = instream.ReadSingle();
                bounds_max.Z = instream.ReadSingle();
                bounds_max.X = instream.ReadSingle();
                bounds_max.Y = instream.ReadSingle();
                unknown11 = instream.ReadUInt32();
                unknown12 = instream.ReadUInt32();
                unknown13 = instream.ReadUInt32();
                skinbones_ID = instream.ReadUInt32();
            }

            remaining_data = null;
            if (section.offset + 12L + section.size <= instream.BaseStream.Position)
                return;
            remaining_data =
                instream.ReadBytes((int) (section.offset + 12L + section.size - instream.BaseStream.Position));
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(model_data_tag);
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
            object3D.StreamWriteData(outstream);
            outstream.Write(version);
            if (version == 6U)
            {
                outstream.Write(v6_unknown5.X);
                outstream.Write(v6_unknown5.Y);
                outstream.Write(v6_unknown5.Z);
                outstream.Write(v6_unknown6.X);
                outstream.Write(v6_unknown6.Y);
                outstream.Write(v6_unknown6.Z);
                outstream.Write(v6_unknown7);
                outstream.Write(v6_unknown8);
            }
            else
            {
                outstream.Write(passthroughGP_ID);
                outstream.Write(topologyIP_ID);
                outstream.Write(count);
                foreach (ModelItem modelItem in items)
                {
                    outstream.Write(modelItem.unknown1);
                    outstream.Write(modelItem.vertCount);
                    outstream.Write(modelItem.unknown2);
                    outstream.Write(modelItem.faceCount);
                    outstream.Write(modelItem.material_id);
                }

                outstream.Write(material_group_section_id);
                outstream.Write(unknown10);
                outstream.Write(bounds_min.Z);
                outstream.Write(bounds_min.X);
                outstream.Write(bounds_min.Y);
                outstream.Write(bounds_max.Z);
                outstream.Write(bounds_max.X);
                outstream.Write(bounds_max.Y);
                outstream.Write(unknown11);
                outstream.Write(unknown12);
                outstream.Write(unknown13);
                outstream.Write(skinbones_ID);
            }

            if (remaining_data == null)
                return;
            outstream.Write(remaining_data);
        }

        public override string ToString()
        {
            if (version == 6U)
                return "[Model_v6] ID: " + id + " size: " + size + " Object3D: [ " + object3D + " ] version: " +
                       version + " unknown5: " + v6_unknown5 + " unknown6: " + v6_unknown6 + " unknown7: " +
                       v6_unknown7 + " unknown8: " + v6_unknown8 + (remaining_data != null
                           ? " REMAINING DATA! " + remaining_data.Length + " bytes"
                           : (object) "");
            StringBuilder stringBuilder = new StringBuilder();
            bool flag = true;
            foreach (ModelItem modelItem in items)
            {
                stringBuilder.Append((flag ? "" : ", ") + modelItem);
                flag = false;
            }

            return "[Model] ID: " + id + " size: " + size + " Object3D: [ " + object3D + " ] version: " + version +
                   " passthroughGP_ID: " + passthroughGP_ID + " topologyIP_ID: " + topologyIP_ID + " count: " + count +
                   " items: [" + stringBuilder + "] material_group_section_id: " + material_group_section_id +
                   " unknown10: " + unknown10 + " bounds_min: " + bounds_min + " bounds_max: " + bounds_max +
                   " unknown11: " + unknown11 + " unknown12: " + unknown12 + " unknown13: " + unknown13 +
                   " skinbones_ID: " + skinbones_ID + (remaining_data != null
                       ? " REMAINING DATA! " + remaining_data.Length + " bytes"
                       : (object) "");
        }
    }
}