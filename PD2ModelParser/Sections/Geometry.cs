// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.Geometry
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using Nexus;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace PD2ModelParser.Sections
{
    internal class Geometry : Section
    {
        private static uint geometry_tag = 2058384083;
        public List<GeometryHeader> headers = new List<GeometryHeader>();
        public List<Vector3D> verts = new List<Vector3D>();
        public List<Vector2D> uvs = new List<Vector2D>();
        public List<Vector2D> pattern_uvs = new List<Vector2D>();
        public List<Vector3D> normals = new List<Vector3D>();
        public List<GeometryColor> vertex_colors = new List<GeometryColor>();
        public List<GeometryWeightGroups> weight_groups = new List<GeometryWeightGroups>();
        public List<Vector3D> weights = new List<Vector3D>();
        public List<Vector3D> unknown20 = new List<Vector3D>();
        public List<Vector3D> unknown21 = new List<Vector3D>();
        public List<byte[]> unknown_item_data = new List<byte[]>();
        public uint id;
        public uint size;
        public uint vert_count;
        public uint header_count;
        public uint geometry_size;
        public ulong hashname;
        public byte[] remaining_data;

        public Geometry()
        {
        }

        public Geometry(uint sec_id, obj_data newobject)
        {
            this.id = sec_id;
            this.size = 0U;
            this.vert_count = (uint) newobject.verts.Count;
            this.header_count = 5U;
            this.headers.Add(new GeometryHeader(3U, 1U));
            this.headers.Add(new GeometryHeader(2U, 7U));
            this.headers.Add(new GeometryHeader(3U, 2U));
            this.headers.Add(new GeometryHeader(3U, 20U));
            this.headers.Add(new GeometryHeader(3U, 21U));
            this.verts = newobject.verts;
            this.uvs = newobject.uv;
            this.normals = newobject.normals;
            this.hashname = Hash64.HashString(newobject.object_name + ".Geometry", 0UL);
        }

        public Geometry(BinaryReader instream, SectionHeader section)
        {
            StreamReadData(instream, section);
        }

        public sealed override void StreamReadData(BinaryReader instream, SectionHeader section)
        {
            this.id = section.id;
            this.size = section.size;
            uint[] numArray = new uint[9]
            {
                0U,
                4U,
                8U,
                12U,
                16U,
                4U,
                4U,
                8U,
                12U
            };
            this.vert_count = instream.ReadUInt32();
            this.header_count = instream.ReadUInt32();
            uint num = 0;
            for (int index = 0; (long) index < (long) this.header_count; ++index)
            {
                GeometryHeader geometryHeader = new GeometryHeader();
                geometryHeader.item_size = instream.ReadUInt32();
                geometryHeader.item_type = instream.ReadUInt32();
                num += numArray[(int) geometryHeader.item_size];
                this.headers.Add(geometryHeader);
            }

            this.geometry_size = this.vert_count * num;
            foreach (GeometryHeader header in this.headers)
            {
                if (header.item_type == 1U)
                {
                    for (int index = 0; (long) index < (long) this.vert_count; ++index)
                        this.verts.Add(new Vector3D()
                        {
                            X = instream.ReadSingle(),
                            Y = instream.ReadSingle(),
                            Z = instream.ReadSingle()
                        });
                }
                else if (header.item_type == 7U)
                {
                    for (int index = 0; (long) index < (long) this.vert_count; ++index)
                        this.uvs.Add(new Vector2D()
                        {
                            X = instream.ReadSingle(),
                            Y = -instream.ReadSingle()
                        });
                }
                else if (header.item_type == 2U)
                {
                    for (int index = 0; (long) index < (long) this.vert_count; ++index)
                        this.normals.Add(new Vector3D()
                        {
                            X = instream.ReadSingle(),
                            Y = instream.ReadSingle(),
                            Z = instream.ReadSingle()
                        });
                }
                else if (header.item_type == 8U)
                {
                    for (int index = 0; (long) index < (long) this.vert_count; ++index)
                        this.pattern_uvs.Add(new Vector2D()
                        {
                            X = instream.ReadSingle(),
                            Y = instream.ReadSingle()
                        });
                }
                else if (header.item_type == 5U)
                {
                    for (int index = 0; (long) index < (long) this.vert_count; ++index)
                        this.vertex_colors.Add(new GeometryColor(instream));
                }
                else if (header.item_type == 20U)
                {
                    for (int index = 0; (long) index < (long) this.vert_count; ++index)
                        this.unknown20.Add(new Vector3D()
                        {
                            X = instream.ReadSingle(),
                            Y = instream.ReadSingle(),
                            Z = instream.ReadSingle()
                        });
                }
                else if (header.item_type == 21U)
                {
                    for (int index = 0; (long) index < (long) this.vert_count; ++index)
                        this.unknown21.Add(new Vector3D()
                        {
                            X = instream.ReadSingle(),
                            Y = instream.ReadSingle(),
                            Z = instream.ReadSingle()
                        });
                }
                else if (header.item_type == 15U)
                {
                    for (int index = 0; (long) index < (long) this.vert_count; ++index)
                        this.weight_groups.Add(new GeometryWeightGroups(instream));
                }
                else if (header.item_type == 17U)
                {
                    for (int index = 0; (long) index < (long) this.vert_count; ++index)
                        this.weights.Add(new Vector3D()
                        {
                            X = instream.ReadSingle(),
                            Y = instream.ReadSingle(),
                            Z = instream.ReadSingle()
                        });
                }
                else
                    this.unknown_item_data.Add(
                        instream.ReadBytes((int) numArray[header.item_size] * (int) this.vert_count));
            }

            this.hashname = instream.ReadUInt64();
            this.remaining_data = (byte[]) null;
            if (section.offset + 12L + (long) section.size <= instream.BaseStream.Position)
                return;
            this.remaining_data =
                instream.ReadBytes((int) (section.offset + 12L + (long) section.size - instream.BaseStream.Position));
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(Geometry.geometry_tag);
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
            outstream.Write(this.vert_count);
            outstream.Write(this.header_count);
            foreach (GeometryHeader header in this.headers)
            {
                outstream.Write(header.item_size);
                outstream.Write(header.item_type);
            }

            List<Vector3D> verts = this.verts;
            int index1 = 0;
            List<Vector2D> uvs = this.uvs;
            int index2 = 0;
            List<Vector3D> normals = this.normals;
            int index3 = 0;
            List<Vector2D> patternUvs = this.pattern_uvs;
            int index4 = 0;
            List<GeometryWeightGroups> weightGroups = this.weight_groups;
            int num1 = 0;
            List<Vector3D> weights = this.weights;
            int num2 = 0;
            List<Vector3D> unknown20 = this.unknown20;
            int num3 = 0;
            List<Vector3D> unknown21 = this.unknown21;
            int num4 = 0;
            List<byte[]> unknownItemData = this.unknown_item_data;
            int index5 = 0;
            foreach (GeometryHeader header in this.headers)
            {
                if (header.item_type == 1U)
                {
                    for (int index6 = 0; (long) index6 < (long) this.vert_count; ++index6)
                    {
                        Vector3D vector3D = verts[index1];
                        outstream.Write(vector3D.X);
                        outstream.Write(vector3D.Y);
                        outstream.Write(vector3D.Z);
                        ++index1;
                    }
                }
                else if (header.item_type == 7U)
                {
                    for (int index6 = 0; (long) index6 < (long) this.vert_count; ++index6)
                    {
                        Vector2D vector2D = uvs[index2];
                        outstream.Write(vector2D.X);
                        outstream.Write(-vector2D.Y);
                        ++index2;
                    }
                }
                else if (header.item_type == 2U)
                {
                    for (int index6 = 0; (long) index6 < (long) this.vert_count; ++index6)
                    {
                        Vector3D vector3D = normals[index3];
                        outstream.Write(vector3D.X);
                        outstream.Write(vector3D.Y);
                        outstream.Write(vector3D.Z);
                        ++index3;
                    }
                }
                else if (header.item_type == 8U)
                {
                    for (int index6 = 0; (long) index6 < (long) this.vert_count; ++index6)
                    {
                        if ((long) this.pattern_uvs.Count != (long) this.vert_count)
                        {
                            outstream.Write(0.0f);
                            outstream.Write(0.0f);
                        }
                        else
                        {
                            Vector2D vector2D = patternUvs[index4];
                            outstream.Write(vector2D.X);
                            outstream.Write(-vector2D.Y);
                            ++index4;
                        }
                    }
                }
                else if (header.item_type == 20U)
                {
                    for (int index6 = 0; (long) index6 < (long) this.vert_count; ++index6)
                    {
                        if ((long) this.unknown20.Count != (long) this.vert_count)
                        {
                            outstream.Write(0.0f);
                            outstream.Write(0.0f);
                            outstream.Write(0.0f);
                        }
                        else
                        {
                            Vector3D vector3D = unknown20[index6];
                            outstream.Write(vector3D.X);
                            outstream.Write(vector3D.Y);
                            outstream.Write(vector3D.Z);
                            ++num3;
                        }
                    }
                }
                else if (header.item_type == 21U)
                {
                    for (int index6 = 0; (long) index6 < (long) this.vert_count; ++index6)
                    {
                        if ((long) this.unknown21.Count != (long) this.vert_count)
                        {
                            outstream.Write(0.0f);
                            outstream.Write(0.0f);
                            outstream.Write(0.0f);
                        }
                        else
                        {
                            Vector3D vector3D = unknown21[index6];
                            outstream.Write(vector3D.X);
                            outstream.Write(vector3D.Y);
                            outstream.Write(vector3D.Z);
                            ++num4;
                        }
                    }
                }
                else if (header.item_type == 15U)
                {
                    for (int index6 = 0; (long) index6 < (long) this.vert_count; ++index6)
                    {
                        if ((long) this.weight_groups.Count != (long) this.vert_count)
                        {
                            outstream.Write(0.0f);
                            outstream.Write(0.0f);
                        }
                        else
                        {
                            weightGroups[index6].StreamWrite(outstream);
                            ++num1;
                        }
                    }
                }
                else if (header.item_type == 17U)
                {
                    for (int index6 = 0; (long) index6 < (long) this.vert_count; ++index6)
                    {
                        if ((long) this.weights.Count != (long) this.vert_count)
                        {
                            outstream.Write(1f);
                            outstream.Write(0.0f);
                            outstream.Write(0.0f);
                        }
                        else
                        {
                            Vector3D vector3D = weights[index6];
                            outstream.Write(vector3D.X);
                            outstream.Write(vector3D.Y);
                            outstream.Write(vector3D.Z);
                            ++num2;
                        }
                    }
                }
                else
                {
                    outstream.Write(unknownItemData[index5]);
                    ++index5;
                }
            }

            outstream.Write(this.hashname);
            if (this.remaining_data == null)
                return;
            outstream.Write(this.remaining_data);
        }

        public void PrintDetailedOutput(StreamWriter outstream)
        {
            if (this.weight_groups.Count <= 0 || this.unknown20.Count <= 0 ||
                (this.unknown21.Count <= 0 || this.weights.Count <= 0))
                return;
            outstream.WriteLine("Printing weights table for " + StaticStorage.hashindex.GetString(this.hashname));
            outstream.WriteLine("====================================================");
            outstream.WriteLine(
                "unkn15_1\tunkn15_2\tunkn15_3\tunkn15_4\tunkn17_X\tunkn17_Y\tunk17_Z\ttotalsum\tunk_20_X\tunk_20_Y\tunk_20_Z\tunk21_X\tunk21_Y\tunk21_Z");
            for (int index = 0; index < this.weight_groups.Count; ++index)
                outstream.WriteLine(this.weight_groups[index].Bones1.ToString() + "\t" +
                                    this.weight_groups[index].Bones2.ToString() + "\t" +
                                    this.weight_groups[index].Bones3.ToString() + "\t" +
                                    this.weight_groups[index].Bones4.ToString() + "\t" +
                                    this.weights[index].X.ToString("0.000000",
                                        (IFormatProvider) CultureInfo.InvariantCulture) + "\t" +
                                    this.weights[index].Y.ToString("0.000000",
                                        (IFormatProvider) CultureInfo.InvariantCulture) + "\t" +
                                    this.weights[index].Z.ToString("0.000000",
                                        (IFormatProvider) CultureInfo.InvariantCulture) + "\t" +
                                    (this.weights[index].X + this.weights[index].Y + this.weights[index].Z).ToString(
                                        "0.000000", (IFormatProvider) CultureInfo.InvariantCulture) + "\t" +
                                    this.unknown20[index].X.ToString("0.000000",
                                        (IFormatProvider) CultureInfo.InvariantCulture) + "\t" +
                                    this.unknown20[index].Y.ToString("0.000000",
                                        (IFormatProvider) CultureInfo.InvariantCulture) + "\t" +
                                    this.unknown20[index].Z.ToString("0.000000",
                                        (IFormatProvider) CultureInfo.InvariantCulture) + "\t" +
                                    this.unknown21[index].X.ToString("0.000000",
                                        (IFormatProvider) CultureInfo.InvariantCulture) + "\t" +
                                    this.unknown21[index].Y.ToString("0.000000",
                                        (IFormatProvider) CultureInfo.InvariantCulture) + "\t" + this.unknown21[index].Z
                                        .ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture));
            outstream.WriteLine("====================================================");
        }

        public override string ToString()
        {
            return "[Geometry] ID: " + (object) this.id + " size: " + (object) this.size + " Count: " +
                   (object) this.vert_count + " Count2: " + (object) this.header_count + " Headers: " +
                   (object) this.headers.Count + " Size: " + (object) this.geometry_size + " Verts: " +
                   (object) this.verts.Count + " UVs: " + (object) this.uvs.Count + " Pattern UVs: " +
                   (object) this.pattern_uvs.Count + " Normals: " + (object) this.normals.Count + " unknown_15: " +
                   (object) this.weight_groups.Count + " unknown_17: " + (object) this.weights.Count + " unknown_20: " +
                   (object) this.unknown20.Count + " unknown_21: " + (object) this.unknown21.Count +
                   " Geometry_unknown_item_data: " + (object) this.unknown_item_data.Count + " unknown_hash: " +
                   StaticStorage.hashindex.GetString(this.hashname) + (this.remaining_data != null
                       ? (object) (" REMAINING DATA! " + (object) this.remaining_data.Length + " bytes")
                       : (object) "");
        }
    }
}