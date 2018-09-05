using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Nexus;
using PD2ModelParser.Abstract;
using PD2ModelParser.Sections;

namespace PD2ModelParser.Concrete
{
    public class ObjParser : IObjParser
    {
        public byte[] Parse(List<SectionHeader> sections, Dictionary<uint, Section> parsedSections)
        {
            ushort num7 = 0;
            uint num8 = 0;
            uint num9 = 0;
            MemoryStream result = new MemoryStream();
            using (StreamWriter streamWriter1 = new StreamWriter(result))
            {
                foreach (SectionHeader section in sections)
                    if ((int) section.type == (int) FileManager.model_data_tag)
                    {
                        Model parsedSection1 = (Model) parsedSections[section.id];
                        if (parsedSection1.version != 6U)
                        {
                            PassthroughGP parsedSection2 =
                                (PassthroughGP) parsedSections[parsedSection1.passthroughGP_ID];
                            Geometry parsedSection3 = (Geometry) parsedSections[parsedSection2.geometry_section];
                            Topology parsedSection4 = (Topology) parsedSections[parsedSection2.topology_section];
                            streamWriter1.WriteLine("#");
                            streamWriter1.WriteLine("# object " +
                                                    StaticStorage.hashindex.GetString(parsedSection1.object3D
                                                        .hashname));
                            streamWriter1.WriteLine("#");
                            streamWriter1.WriteLine();
                            streamWriter1.WriteLine(
                                "o " + StaticStorage.hashindex.GetString(parsedSection1.object3D.hashname));
                            foreach (Vector3D vert in parsedSection3.verts)
                            {
                                StreamWriter streamWriter2 = streamWriter1;
                                string[] strArray1 = new string[6];
                                strArray1[0] = "v ";
                                string[] strArray2 = strArray1;
                                int index1 = 1;
                                float num2 = vert.X;
                                string str1 = num2.ToString("0.000000", CultureInfo.InvariantCulture);
                                strArray2[index1] = str1;
                                strArray1[2] = " ";
                                string[] strArray3 = strArray1;
                                int index2 = 3;
                                num2 = vert.Y;
                                string str2 = num2.ToString("0.000000", CultureInfo.InvariantCulture);
                                strArray3[index2] = str2;
                                strArray1[4] = " ";
                                string[] strArray4 = strArray1;
                                int index3 = 5;
                                num2 = vert.Z;
                                string str3 = num2.ToString("0.000000", CultureInfo.InvariantCulture);
                                strArray4[index3] = str3;
                                string str5 = string.Concat(strArray1);
                                streamWriter2.WriteLine(str5);
                            }

                            streamWriter1.WriteLine("# " + parsedSection3.verts.Count + " vertices");
                            streamWriter1.WriteLine();
                            foreach (Vector2D uv in parsedSection3.uvs)
                            {
                                StreamWriter streamWriter2 = streamWriter1;
                                string str1 = "vt ";
                                float num2 = uv.X;
                                string str2 = num2.ToString("0.000000", CultureInfo.InvariantCulture);
                                string str3 = " ";
                                num2 = uv.Y;
                                string str5 = num2.ToString("0.000000", CultureInfo.InvariantCulture);
                                string str6 = str1 + str2 + str3 + str5;
                                streamWriter2.WriteLine(str6);
                            }

                            streamWriter1.WriteLine("# " + parsedSection3.uvs.Count + " UVs");
                            streamWriter1.WriteLine();
                            foreach (Vector3D normal in parsedSection3.normals)
                            {
                                StreamWriter streamWriter2 = streamWriter1;
                                string[] strArray1 = new string[6];
                                strArray1[0] = "vn ";
                                string[] strArray2 = strArray1;
                                int index1 = 1;
                                float num2 = normal.X;
                                string str1 = num2.ToString("0.000000", CultureInfo.InvariantCulture);
                                strArray2[index1] = str1;
                                strArray1[2] = " ";
                                string[] strArray3 = strArray1;
                                int index2 = 3;
                                num2 = normal.Y;
                                string str2 = num2.ToString("0.000000", CultureInfo.InvariantCulture);
                                strArray3[index2] = str2;
                                strArray1[4] = " ";
                                string[] strArray4 = strArray1;
                                int index3 = 5;
                                num2 = normal.Z;
                                string str3 = num2.ToString("0.000000", CultureInfo.InvariantCulture);
                                strArray4[index3] = str3;
                                string str5 = string.Concat(strArray1);
                                streamWriter2.WriteLine(str5);
                            }

                            streamWriter1.WriteLine("# " + parsedSection3.normals.Count + " Normals");
                            streamWriter1.WriteLine();
                            streamWriter1.WriteLine(
                                "g " + StaticStorage.hashindex.GetString(parsedSection1.object3D.hashname));
                            foreach (Face face in parsedSection4.facelist)
                            {
                                streamWriter1.Write("f " + (num7 + face.x + 1));
                                streamWriter1.Write('/');
                                if (parsedSection3.uvs.Count > 0)
                                    streamWriter1.Write((uint) ((int) num8 + face.x + 1));
                                streamWriter1.Write('/');
                                if (parsedSection3.normals.Count > 0)
                                    streamWriter1.Write((uint) ((int) num9 + face.x + 1));
                                streamWriter1.Write(" " + (num7 + face.y + 1));
                                streamWriter1.Write('/');
                                if (parsedSection3.uvs.Count > 0)
                                    streamWriter1.Write((uint) ((int) num8 + face.y + 1));
                                streamWriter1.Write('/');
                                if (parsedSection3.normals.Count > 0)
                                    streamWriter1.Write((uint) ((int) num9 + face.y + 1));
                                streamWriter1.Write(" " + (num7 + face.z + 1));
                                streamWriter1.Write('/');
                                if (parsedSection3.uvs.Count > 0)
                                    streamWriter1.Write((uint) ((int) num8 + face.z + 1));
                                streamWriter1.Write('/');
                                if (parsedSection3.normals.Count > 0)
                                    streamWriter1.Write((uint) ((int) num9 + face.z + 1));
                                streamWriter1.WriteLine();
                            }

                            streamWriter1.WriteLine("# " + parsedSection4.facelist.Count + " Faces");
                            streamWriter1.WriteLine();
                            num7 += (ushort) parsedSection3.verts.Count;
                            num8 += (ushort) parsedSection3.uvs.Count;
                            num9 += (ushort) parsedSection3.normals.Count;
                        }
                    }
            }

            return result.ToArray();
        }
    }
}