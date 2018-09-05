using Nexus;
using PD2ModelParser.Sections;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace PD2ModelParser
{
    [Obsolete("Please use IModelParser instead")]
  public class FileManager
  {
    public static uint animation_data_tag = 1572868536;
    public static uint author_tag = 1982055525;
    public static uint material_group_tag = 690449181;
    public static uint material_tag = 1012162716;
    public static uint object3D_tag = 268226816;
    public static uint model_data_tag = 1646341512;
    public static uint geometry_tag = 2058384083;
    public static uint topology_tag = 1280342547;
    public static uint passthroughGP_tag = 3819155914;
    public static uint topologyIP_tag = 62272701;
    public static uint quatLinearRotationController_tag = 1686773868;
    public static uint quatBezRotationController_tag = 426984869;
    public static uint skinbones_tag = 1707874341;
    public static uint bones_tag = 246692983;
    public static uint light_tag = 4288756608;
    public static uint lightSet_tag = 861218179;
    public static uint linearVector3Controller_tag = 648352396;
    public static uint linearFloatController_tag = 1992252262;
    public static uint lookAtConstrRotationController = 1738369371;
    public static uint camera_tag = 1186935207;
    public List<SectionHeader> sections = new List<SectionHeader>();
    public Dictionary<uint, object> parsed_sections = new Dictionary<uint, object>();
    public byte[] leftover_data;

    public void Open(string filepath, string rp = null, string output = null)
    {
      StaticStorage.hashindex.Load();
      Console.WriteLine("Loading: " + filepath);
      uint num1 = 0;
      using (FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) fileStream))
        {
          int num2 = binaryReader.ReadInt32();
          uint num3 = num1 + 4U;
          int num4 = binaryReader.ReadInt32();
          uint num5 = num3 + 4U;
          int num6;
          if (num2 == -1)
          {
            num6 = binaryReader.ReadInt32();
            num5 += 4U;
          }
          else
            num6 = num2;
          Console.WriteLine("Size: " + (object) num4 + " bytes, Sections: " + (object) num6);
          for (int index = 0; index < num6; ++index)
          {
            SectionHeader sectionHeader = new SectionHeader(binaryReader);
            this.sections.Add(sectionHeader);
            Console.WriteLine((object) sectionHeader);
            num5 += sectionHeader.size + 12U;
            Console.WriteLine("Next offset: " + (object) num5);
            fileStream.Position = (long) num5;
          }
          foreach (SectionHeader section in this.sections)
          {
            object obj1 = new object();
            fileStream.Position = section.offset + 12L;
            object obj2 = null;
            if ((int) section.type == (int) FileManager.animation_data_tag)
            {
              Console.WriteLine("Animation Tag at " + (object) section.offset + " Size: " + (object) section.size);
              obj2 = (object) new Animation(binaryReader, section);
              Console.WriteLine(obj2);
            }
            else if ((int) section.type == (int) FileManager.author_tag)
            {
              Console.WriteLine("Author Tag at " + (object) section.offset + " Size: " + (object) section.size);
              obj2 = (object) new Author(binaryReader, section);
              Console.WriteLine(obj2);
            }
            else if ((int) section.type == (int) FileManager.material_group_tag)
            {
              Console.WriteLine("Material Group Tag at " + (object) section.offset + " Size: " + (object) section.size);
              obj2 = (object) new MaterialGroup(binaryReader, section);
              Console.WriteLine(obj2);
            }
            else if ((int) section.type == (int) FileManager.material_tag)
            {
              Console.WriteLine("Material Tag at " + (object) section.offset + " Size: " + (object) section.size);
              obj2 = (object) new Material(binaryReader, section);
              Console.WriteLine(obj2);
            }
            else if ((int) section.type == (int) FileManager.object3D_tag)
            {
              Console.WriteLine("Object 3D Tag at " + (object) section.offset + " Size: " + (object) section.size);
              obj2 = (object) new Object3D(binaryReader, section);
              if ((obj2 as Object3D).hashname == 4921176767231919846UL)
                Console.WriteLine();
              Console.WriteLine(obj2);
            }
            else if ((int) section.type == (int) FileManager.geometry_tag)
            {
              Console.WriteLine("Geometry Tag at " + (object) section.offset + " Size: " + (object) section.size);
              obj2 = (object) new Geometry(binaryReader, section);
              Console.WriteLine(obj2);
            }
            else if ((int) section.type == (int) FileManager.model_data_tag)
            {
              Console.WriteLine("Model Data Tag at " + (object) section.offset + " Size: " + (object) section.size);
              obj2 = (object) new Model(binaryReader, section);
              Console.WriteLine(obj2);
            }
            else if ((int) section.type == (int) FileManager.topology_tag)
            {
              Console.WriteLine("Topology Tag at " + (object) section.offset + " Size: " + (object) section.size);
              obj2 = (object) new Topology(binaryReader, section);
              Console.WriteLine(obj2);
            }
            else if ((int) section.type == (int) FileManager.passthroughGP_tag)
            {
              Console.WriteLine("passthroughGP Tag at " + (object) section.offset + " Size: " + (object) section.size);
              obj2 = (object) new PassthroughGP(binaryReader, section);
              Console.WriteLine(obj2);
            }
            else if ((int) section.type == (int) FileManager.topologyIP_tag)
            {
              Console.WriteLine("TopologyIP Tag at " + (object) section.offset + " Size: " + (object) section.size);
              obj2 = (object) new TopologyIP(binaryReader, section);
              Console.WriteLine(obj2);
            }
            else if ((int) section.type == (int) FileManager.bones_tag)
            {
              Console.WriteLine("Bones Tag at " + (object) section.offset + " Size: " + (object) section.size);
              obj2 = (object) new Bones(binaryReader, section);
              Console.WriteLine(obj2);
            }
            else if ((int) section.type == (int) FileManager.skinbones_tag)
            {
              Console.WriteLine("SkinBones Tag at " + (object) section.offset + " Size: " + (object) section.size);
              obj2 = (object) new SkinBones(binaryReader, section);
              Console.WriteLine(obj2);
            }
            else if ((int) section.type == (int) FileManager.quatLinearRotationController_tag)
            {
              Console.WriteLine("QuatLinearRotationController Tag at " + (object) section.offset + " Size: " + (object) section.size);
              obj2 = (object) new QuatLinearRotationController(binaryReader, section);
              Console.WriteLine(obj2);
            }
            else if ((int) section.type == (int) FileManager.linearVector3Controller_tag)
            {
              Console.WriteLine("QuatLinearRotationController Tag at " + (object) section.offset + " Size: " + (object) section.size);
              obj2 = (object) new LinearVector3Controller(binaryReader, section);
              Console.WriteLine(obj2);
            }
            else
            {
              Console.WriteLine("UNKNOWN Tag at " + (object) section.offset + " Size: " + (object) section.size);
              fileStream.Position = section.offset;
              obj2 = (object) new Unknown(binaryReader, section);
              Console.WriteLine(obj2);
            }
            this.parsed_sections.Add(section.id, obj2);
          }
          if (fileStream.Position < fileStream.Length)
            this.leftover_data = binaryReader.ReadBytes((int) (fileStream.Length - fileStream.Position));
          binaryReader.Close();
        }
        fileStream.Close();
      }
      if (rp != null)
        this.updateRP(rp);
      using (FileStream fileStream = new FileStream("outinfo.txt", FileMode.Create, FileAccess.Write))
      {
        using (StreamWriter outstream = new StreamWriter((Stream) fileStream))
        {
          foreach (SectionHeader section in this.sections)
          {
            if ((int) section.type == (int) FileManager.passthroughGP_tag)
            {
              PassthroughGP parsedSection1 = (PassthroughGP) this.parsed_sections[section.id];
              Geometry parsedSection2 = (Geometry) this.parsed_sections[parsedSection1.geometry_section];
              Topology parsedSection3 = (Topology) this.parsed_sections[parsedSection1.topology_section];
              outstream.WriteLine("Object ID: " + (object) section.id);
              outstream.WriteLine("Verts (x, z, y)");
              foreach (Vector3D vert in parsedSection2.verts)
              {
                StreamWriter streamWriter = outstream;
                string[] strArray1 = new string[5]
                {
                  vert.X.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture),
                  " ",
                  null,
                  null,
                  null
                };
                string[] strArray2 = strArray1;
                int index1 = 2;
                float num2 = vert.Z;
                string str1 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                strArray2[index1] = str1;
                strArray1[3] = " ";
                string[] strArray3 = strArray1;
                int index2 = 4;
                num2 = -vert.Y;
                string str2 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                strArray3[index2] = str2;
                string str3 = string.Concat(strArray1);
                streamWriter.WriteLine(str3);
              }
              outstream.WriteLine("UV (u, -v)");
              foreach (Vector2D uv in parsedSection2.uvs)
                outstream.WriteLine(uv.X.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture) + " " + uv.Y.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture));
              outstream.WriteLine("Normals (i, j, k)");
              List<Vector3D> vector3DList1 = new List<Vector3D>();
              List<Vector3D> vector3DList2 = new List<Vector3D>();
              foreach (Vector3D normal in parsedSection2.normals)
              {
                StreamWriter streamWriter = outstream;
                string[] strArray1 = new string[5]
                {
                  normal.X.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture),
                  " ",
                  null,
                  null,
                  null
                };
                string[] strArray2 = strArray1;
                int index1 = 2;
                float num2 = normal.Y;
                string str1 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                strArray2[index1] = str1;
                strArray1[3] = " ";
                string[] strArray3 = strArray1;
                int index2 = 4;
                num2 = normal.Z;
                string str2 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                strArray3[index2] = str2;
                string str3 = string.Concat(strArray1);
                streamWriter.WriteLine(str3);
                Vector3D vector3D1 = Vector3D.Cross(normal, Vector3D.Right);
                Vector3D vector3D2 = Vector3D.Cross(normal, Vector3D.Forward);
                Vector3D v2 = (double) vector3D1.Length() <= (double) vector3D2.Length() ? vector3D2 : vector3D1;
                v2.Normalize();
                vector3DList1.Add(v2);
                Vector3D vector3D3 = Vector3D.Cross(normal, v2);
                vector3D3 = new Vector3D(vector3D3.X * -1f, vector3D3.Y * -1f, vector3D3.Z * -1f);
                vector3D3.Normalize();
                vector3DList2.Add(vector3D3);
              }
              if (vector3DList2.Count > 0 && vector3DList1.Count > 0)
              {
                Vector3D[] array1 = vector3DList2.ToArray();
                Vector3D[] array2 = vector3DList1.ToArray();
                for (int index = 0; index < parsedSection3.facelist.Count; ++index)
                {
                  Face face = parsedSection3.facelist[index];
                  array1[(int) face.x] = vector3DList2[(int) parsedSection3.facelist[index].x];
                  array1[(int) face.y] = vector3DList2[(int) parsedSection3.facelist[index].y];
                  array1[(int) face.z] = vector3DList2[(int) parsedSection3.facelist[index].z];
                  array2[(int) face.x] = vector3DList1[(int) parsedSection3.facelist[index].x];
                  array2[(int) face.y] = vector3DList1[(int) parsedSection3.facelist[index].y];
                  array2[(int) face.z] = vector3DList1[(int) parsedSection3.facelist[index].z];
                }
                vector3DList2 = ((IEnumerable<Vector3D>) array1).ToList<Vector3D>();
                vector3DList1 = ((IEnumerable<Vector3D>) array2).ToList<Vector3D>();
              }
              outstream.WriteLine("Pattern UVs (u, v)");
              foreach (Vector2D patternUv in parsedSection2.pattern_uvs)
                outstream.WriteLine(patternUv.X.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture) + " " + patternUv.Y.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture));
              int index3 = 0;
              int index4 = 0;
              outstream.WriteLine("Unknown 20 (float, float, float) - Normal tangents???");
              foreach (Vector3D vector3D1 in parsedSection2.unknown20)
              {
                StreamWriter streamWriter = outstream;
                string[] strArray1 = new string[5]
                {
                  vector3D1.X.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture),
                  " ",
                  null,
                  null,
                  null
                };
                string[] strArray2 = strArray1;
                int index1 = 2;
                float num2 = vector3D1.Y;
                string str1 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                strArray2[index1] = str1;
                strArray1[3] = " ";
                string[] strArray3 = strArray1;
                int index2 = 4;
                num2 = vector3D1.Z;
                string str2 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                strArray3[index2] = str2;
                string str3 = string.Concat(strArray1);
                streamWriter.WriteLine(str3);
                Vector3D vector3D2 = vector3DList1[index3];
                outstream.WriteLine("* " + vector3D2.X.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture) + " " + vector3D2.Y.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture) + " " + vector3D2.Z.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture));
                ++index3;
              }
              outstream.WriteLine("Unknown 21 (float, float, float) - Normal tangents???");
              foreach (Vector3D vector3D1 in parsedSection2.unknown21)
              {
                StreamWriter streamWriter = outstream;
                string[] strArray1 = new string[5]
                {
                  vector3D1.X.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture),
                  " ",
                  null,
                  null,
                  null
                };
                string[] strArray2 = strArray1;
                int index1 = 2;
                float num2 = vector3D1.Y;
                string str1 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                strArray2[index1] = str1;
                strArray1[3] = " ";
                string[] strArray3 = strArray1;
                int index2 = 4;
                num2 = vector3D1.Z;
                string str2 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                strArray3[index2] = str2;
                string str3 = string.Concat(strArray1);
                streamWriter.WriteLine(str3);
                Vector3D vector3D2 = vector3DList2[index4];
                outstream.WriteLine("* " + vector3D2.X.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture) + " " + vector3D2.Y.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture) + " " + vector3D2.Z.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture));
                ++index4;
              }
              outstream.WriteLine("Unknown 15 (float, float) - Weights???");
              foreach (GeometryWeightGroups weightGroup in parsedSection2.weight_groups)
                outstream.WriteLine(weightGroup.Bones1.ToString() + " " + weightGroup.Bones2.ToString() + " " + weightGroup.Bones3.ToString() + " " + weightGroup.Bones4.ToString());
              outstream.WriteLine("Unknown 17 (float, float, float) - Weights???");
              foreach (Vector3D weight in parsedSection2.weights)
                outstream.WriteLine(weight.X.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture) + " " + weight.Y.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture));
              foreach (byte[] numArray in parsedSection2.unknown_item_data)
              {
                if (numArray.Length % 8 == 0)
                {
                  outstream.WriteLine("Unknown X (float, float)");
                  int startIndex1 = 0;
                  while (startIndex1 < numArray.Length)
                  {
                    outstream.Write(BitConverter.ToSingle(numArray, startIndex1).ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture) + " ");
                    int startIndex2 = startIndex1 + 4;
                    outstream.Write(BitConverter.ToSingle(numArray, startIndex2).ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture));
                    startIndex1 = startIndex2 + 4;
                    outstream.WriteLine();
                  }
                }
                else if (numArray.Length % 12 == 0)
                {
                  outstream.WriteLine("Unknown X (float, float, float)");
                  int startIndex1 = 0;
                  while (startIndex1 < numArray.Length)
                  {
                    outstream.Write(BitConverter.ToSingle(numArray, startIndex1).ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture) + " ");
                    int startIndex2 = startIndex1 + 4;
                    outstream.Write(BitConverter.ToSingle(numArray, startIndex2).ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture) + " ");
                    int startIndex3 = startIndex2 + 4;
                    outstream.Write(BitConverter.ToSingle(numArray, startIndex3).ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture));
                    startIndex1 = startIndex3 + 4;
                    outstream.WriteLine();
                  }
                }
                else if (numArray.Length % 4 == 0)
                {
                  outstream.WriteLine("Unknown X (float)");
                  int startIndex = 0;
                  while (startIndex < numArray.Length)
                  {
                    outstream.Write(BitConverter.ToSingle(numArray, startIndex).ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture));
                    startIndex += 4;
                    outstream.WriteLine();
                  }
                }
                else
                  outstream.Write("Something else... [for debugging]");
              }
              outstream.WriteLine("Faces (f1, f2, f3)");
              foreach (Face face in parsedSection3.facelist)
                outstream.WriteLine(((int) face.x + 1).ToString() + " " + (object) ((int) face.y + 1) + " " + (object) ((int) face.z + 1));
              outstream.WriteLine();
              parsedSection2.PrintDetailedOutput(outstream);
              outstream.WriteLine();
            }
          }
          outstream.Close();
        }
        fileStream.Close();
      }
      ushort num7 = 0;
      uint num8 = 0;
      uint num9 = 0;
      string str4 = "";
        string outputPath = string.IsNullOrEmpty(output)
            ? (str4 + filepath).Replace(".model", ".obj")
            : output;
        string outputPathPatternUv = outputPath.Replace(".obj", "_pattern_uv.obj");
        Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
      using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
      {
        using (StreamWriter streamWriter1 = new StreamWriter((Stream) fileStream))
        {
          foreach (SectionHeader section in this.sections)
          {
            if ((int) section.type == (int) FileManager.model_data_tag)
            {
              Model parsedSection1 = (Model) this.parsed_sections[section.id];
              if (parsedSection1.version != 6U)
              {
                PassthroughGP parsedSection2 = (PassthroughGP) this.parsed_sections[parsedSection1.passthroughGP_ID];
                Geometry parsedSection3 = (Geometry) this.parsed_sections[parsedSection2.geometry_section];
                Topology parsedSection4 = (Topology) this.parsed_sections[parsedSection2.topology_section];
                streamWriter1.WriteLine("#");
                streamWriter1.WriteLine("# object " + StaticStorage.hashindex.GetString(parsedSection1.object3D.hashname));
                streamWriter1.WriteLine("#");
                streamWriter1.WriteLine();
                streamWriter1.WriteLine("o " + StaticStorage.hashindex.GetString(parsedSection1.object3D.hashname));
                foreach (Vector3D vert in parsedSection3.verts)
                {
                  StreamWriter streamWriter2 = streamWriter1;
                  string[] strArray1 = new string[6];
                  strArray1[0] = "v ";
                  string[] strArray2 = strArray1;
                  int index1 = 1;
                  float num2 = vert.X;
                  string str1 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                  strArray2[index1] = str1;
                  strArray1[2] = " ";
                  string[] strArray3 = strArray1;
                  int index2 = 3;
                  num2 = vert.Y;
                  string str2 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                  strArray3[index2] = str2;
                  strArray1[4] = " ";
                  string[] strArray4 = strArray1;
                  int index3 = 5;
                  num2 = vert.Z;
                  string str3 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                  strArray4[index3] = str3;
                  string str5 = string.Concat(strArray1);
                  streamWriter2.WriteLine(str5);
                }
                streamWriter1.WriteLine("# " + (object) parsedSection3.verts.Count + " vertices");
                streamWriter1.WriteLine();
                foreach (Vector2D uv in parsedSection3.uvs)
                {
                  StreamWriter streamWriter2 = streamWriter1;
                  string str1 = "vt ";
                  float num2 = uv.X;
                  string str2 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                  string str3 = " ";
                  num2 = uv.Y;
                  string str5 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                  string str6 = str1 + str2 + str3 + str5;
                  streamWriter2.WriteLine(str6);
                }
                streamWriter1.WriteLine("# " + (object) parsedSection3.uvs.Count + " UVs");
                streamWriter1.WriteLine();
                foreach (Vector3D normal in parsedSection3.normals)
                {
                  StreamWriter streamWriter2 = streamWriter1;
                  string[] strArray1 = new string[6];
                  strArray1[0] = "vn ";
                  string[] strArray2 = strArray1;
                  int index1 = 1;
                  float num2 = normal.X;
                  string str1 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                  strArray2[index1] = str1;
                  strArray1[2] = " ";
                  string[] strArray3 = strArray1;
                  int index2 = 3;
                  num2 = normal.Y;
                  string str2 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                  strArray3[index2] = str2;
                  strArray1[4] = " ";
                  string[] strArray4 = strArray1;
                  int index3 = 5;
                  num2 = normal.Z;
                  string str3 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                  strArray4[index3] = str3;
                  string str5 = string.Concat(strArray1);
                  streamWriter2.WriteLine(str5);
                }
                streamWriter1.WriteLine("# " + (object) parsedSection3.normals.Count + " Normals");
                streamWriter1.WriteLine();
                streamWriter1.WriteLine("g " + StaticStorage.hashindex.GetString(parsedSection1.object3D.hashname));
                foreach (Face face in parsedSection4.facelist)
                {
                  streamWriter1.Write("f " + (object) ((int) num7 + (int) face.x + 1));
                  streamWriter1.Write('/');
                  if (parsedSection3.uvs.Count > 0)
                    streamWriter1.Write((uint) ((int) num8 + (int) face.x + 1));
                  streamWriter1.Write('/');
                  if (parsedSection3.normals.Count > 0)
                    streamWriter1.Write((uint) ((int) num9 + (int) face.x + 1));
                  streamWriter1.Write(" " + (object) ((int) num7 + (int) face.y + 1));
                  streamWriter1.Write('/');
                  if (parsedSection3.uvs.Count > 0)
                    streamWriter1.Write((uint) ((int) num8 + (int) face.y + 1));
                  streamWriter1.Write('/');
                  if (parsedSection3.normals.Count > 0)
                    streamWriter1.Write((uint) ((int) num9 + (int) face.y + 1));
                  streamWriter1.Write(" " + (object) ((int) num7 + (int) face.z + 1));
                  streamWriter1.Write('/');
                  if (parsedSection3.uvs.Count > 0)
                    streamWriter1.Write((uint) ((int) num8 + (int) face.z + 1));
                  streamWriter1.Write('/');
                  if (parsedSection3.normals.Count > 0)
                    streamWriter1.Write((uint) ((int) num9 + (int) face.z + 1));
                  streamWriter1.WriteLine();
                }
                streamWriter1.WriteLine("# " + (object) parsedSection4.facelist.Count + " Faces");
                streamWriter1.WriteLine();
                num7 += (ushort) parsedSection3.verts.Count;
                num8 += (uint) (ushort) parsedSection3.uvs.Count;
                num9 += (uint) (ushort) parsedSection3.normals.Count;
              }
            }
          }
          streamWriter1.Close();
        }
        fileStream.Close();
      }
      ushort num10 = 0;
      uint num11 = 0;
      uint num12 = 0;
      using (FileStream fileStream = new FileStream(outputPathPatternUv, FileMode.Create, FileAccess.Write))
      {
        using (StreamWriter streamWriter1 = new StreamWriter((Stream) fileStream))
        {
          foreach (SectionHeader section in this.sections)
          {
            if ((int) section.type == (int) FileManager.model_data_tag)
            {
              Model parsedSection1 = (Model) this.parsed_sections[section.id];
              if (parsedSection1.version != 6U)
              {
                PassthroughGP parsedSection2 = (PassthroughGP) this.parsed_sections[parsedSection1.passthroughGP_ID];
                Geometry parsedSection3 = (Geometry) this.parsed_sections[parsedSection2.geometry_section];
                Topology parsedSection4 = (Topology) this.parsed_sections[parsedSection2.topology_section];
                streamWriter1.WriteLine("#");
                streamWriter1.WriteLine("# object " + StaticStorage.hashindex.GetString(parsedSection1.object3D.hashname));
                streamWriter1.WriteLine("#");
                streamWriter1.WriteLine();
                streamWriter1.WriteLine("o " + StaticStorage.hashindex.GetString(parsedSection1.object3D.hashname));
                foreach (Vector3D vert in parsedSection3.verts)
                {
                  StreamWriter streamWriter2 = streamWriter1;
                  string[] strArray1 = new string[6];
                  strArray1[0] = "v ";
                  string[] strArray2 = strArray1;
                  int index1 = 1;
                  float num2 = vert.X;
                  string str1 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                  strArray2[index1] = str1;
                  strArray1[2] = " ";
                  string[] strArray3 = strArray1;
                  int index2 = 3;
                  num2 = vert.Y;
                  string str2 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                  strArray3[index2] = str2;
                  strArray1[4] = " ";
                  string[] strArray4 = strArray1;
                  int index3 = 5;
                  num2 = vert.Z;
                  string str3 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                  strArray4[index3] = str3;
                  string str5 = string.Concat(strArray1);
                  streamWriter2.WriteLine(str5);
                }
                streamWriter1.WriteLine("# " + (object) parsedSection3.verts.Count + " vertices");
                streamWriter1.WriteLine();
                foreach (Vector2D patternUv in parsedSection3.pattern_uvs)
                {
                  StreamWriter streamWriter2 = streamWriter1;
                  string str1 = "vt ";
                  float num2 = patternUv.X;
                  string str2 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                  string str3 = " ";
                  num2 = -patternUv.Y;
                  string str5 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                  string str6 = str1 + str2 + str3 + str5;
                  streamWriter2.WriteLine(str6);
                }
                streamWriter1.WriteLine("# " + (object) parsedSection3.pattern_uvs.Count + " UVs");
                streamWriter1.WriteLine();
                foreach (Vector3D normal in parsedSection3.normals)
                {
                  StreamWriter streamWriter2 = streamWriter1;
                  string[] strArray1 = new string[6];
                  strArray1[0] = "vn ";
                  string[] strArray2 = strArray1;
                  int index1 = 1;
                  float num2 = normal.X;
                  string str1 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                  strArray2[index1] = str1;
                  strArray1[2] = " ";
                  string[] strArray3 = strArray1;
                  int index2 = 3;
                  num2 = normal.Y;
                  string str2 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                  strArray3[index2] = str2;
                  strArray1[4] = " ";
                  string[] strArray4 = strArray1;
                  int index3 = 5;
                  num2 = normal.Z;
                  string str3 = num2.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture);
                  strArray4[index3] = str3;
                  string str5 = string.Concat(strArray1);
                  streamWriter2.WriteLine(str5);
                }
                streamWriter1.WriteLine("# " + (object) parsedSection3.normals.Count + " Normals");
                streamWriter1.WriteLine();
                streamWriter1.WriteLine("g " + StaticStorage.hashindex.GetString(parsedSection1.object3D.hashname));
                foreach (Face face in parsedSection4.facelist)
                {
                  streamWriter1.Write("f " + (object) ((int) num10 + (int) face.x + 1));
                  streamWriter1.Write('/');
                  if (parsedSection3.pattern_uvs.Count > 0)
                    streamWriter1.Write((uint) ((int) num11 + (int) face.x + 1));
                  streamWriter1.Write('/');
                  if (parsedSection3.normals.Count > 0)
                    streamWriter1.Write((uint) ((int) num12 + (int) face.x + 1));
                  streamWriter1.Write(" " + (object) ((int) num10 + (int) face.y + 1));
                  streamWriter1.Write('/');
                  if (parsedSection3.pattern_uvs.Count > 0)
                    streamWriter1.Write((uint) ((int) num11 + (int) face.y + 1));
                  streamWriter1.Write('/');
                  if (parsedSection3.normals.Count > 0)
                    streamWriter1.Write((uint) ((int) num12 + (int) face.y + 1));
                  streamWriter1.Write(" " + (object) ((int) num10 + (int) face.z + 1));
                  streamWriter1.Write('/');
                  if (parsedSection3.pattern_uvs.Count > 0)
                    streamWriter1.Write((uint) ((int) num11 + (int) face.z + 1));
                  streamWriter1.Write('/');
                  if (parsedSection3.normals.Count > 0)
                    streamWriter1.Write((uint) ((int) num12 + (int) face.z + 1));
                  streamWriter1.WriteLine();
                }
                streamWriter1.WriteLine("# " + (object) parsedSection4.facelist.Count + " Faces");
                streamWriter1.WriteLine();
                num10 += (ushort) parsedSection3.verts.Count;
                num11 += (uint) (ushort) parsedSection3.pattern_uvs.Count;
                num12 += (uint) (ushort) parsedSection3.normals.Count;
              }
            }
          }
          streamWriter1.Close();
        }
        fileStream.Close();
      }
    }

    public bool ImportNewObjPatternUV(string filepath)
    {
      Console.WriteLine("Importing new obj with file for UV patterns: " + filepath);
      List<obj_data> objDataList = new List<obj_data>();
      try
      {
        using (FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
          using (StreamReader streamReader = new StreamReader((Stream) fileStream))
          {
            obj_data objData = new obj_data();
            bool flag = false;
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            string str;
            while ((str = streamReader.ReadLine()) != null)
            {
              if (!str.StartsWith("#"))
              {
                if (str.StartsWith("o ") || str.StartsWith("g "))
                {
                  if (flag)
                  {
                    flag = false;
                    num1 += objData.verts.Count;
                    num2 += objData.uv.Count;
                    num3 += objData.normals.Count;
                    objDataList.Add(objData);
                    objData = new obj_data();
                  }
                  objData.object_name = str.Substring(2);
                }
                else if (str.StartsWith("usemtl "))
                  objData.material_name = str.Substring(2);
                else if (str.StartsWith("v "))
                {
                  if (flag)
                  {
                    flag = false;
                    num1 += objData.verts.Count;
                    num2 += objData.uv.Count;
                    num3 += objData.normals.Count;
                    objDataList.Add(objData);
                    objData = new obj_data();
                  }
                  string[] strArray = str.Replace("  ", " ").Split(' ');
                  objData.verts.Add(new Vector3D()
                  {
                    X = Convert.ToSingle(strArray[1], (IFormatProvider) CultureInfo.InvariantCulture),
                    Y = Convert.ToSingle(strArray[2], (IFormatProvider) CultureInfo.InvariantCulture),
                    Z = Convert.ToSingle(strArray[3], (IFormatProvider) CultureInfo.InvariantCulture)
                  });
                }
                else if (str.StartsWith("vt "))
                {
                  if (flag)
                  {
                    flag = false;
                    num1 += objData.verts.Count;
                    num2 += objData.uv.Count;
                    num3 += objData.normals.Count;
                    objDataList.Add(objData);
                    objData = new obj_data();
                  }
                  string[] strArray = str.Split(' ');
                  objData.uv.Add(new Vector2D()
                  {
                    X = Convert.ToSingle(strArray[1], (IFormatProvider) CultureInfo.InvariantCulture),
                    Y = Convert.ToSingle(strArray[2], (IFormatProvider) CultureInfo.InvariantCulture)
                  });
                }
                else if (str.StartsWith("vn "))
                {
                  if (flag)
                  {
                    flag = false;
                    num1 += objData.verts.Count;
                    num2 += objData.uv.Count;
                    num3 += objData.normals.Count;
                    objDataList.Add(objData);
                    objData = new obj_data();
                  }
                  string[] strArray = str.Split(' ');
                  objData.normals.Add(new Vector3D()
                  {
                    X = Convert.ToSingle(strArray[1], (IFormatProvider) CultureInfo.InvariantCulture),
                    Y = Convert.ToSingle(strArray[2], (IFormatProvider) CultureInfo.InvariantCulture),
                    Z = Convert.ToSingle(strArray[3], (IFormatProvider) CultureInfo.InvariantCulture)
                  });
                }
                else if (str.StartsWith("f "))
                {
                  flag = true;
                  string[] strArray = str.Substring(2).Split(' ');
                  for (int index = 0; index < 3; ++index)
                  {
                    Face face = new Face();
                    if (objData.verts.Count > 0)
                      face.x = (ushort) ((int) Convert.ToUInt16(strArray[index].Split('/')[0]) - num1 - 1);
                    if (objData.uv.Count > 0)
                      face.y = (ushort) ((int) Convert.ToUInt16(strArray[index].Split('/')[1]) - num2 - 1);
                    if (objData.normals.Count > 0)
                      face.z = (ushort) ((int) Convert.ToUInt16(strArray[index].Split('/')[2]) - num3 - 1);
                    if (face.x < (ushort) 0 || face.y < (ushort) 0 || face.z < (ushort) 0)
                      throw new Exception();
                    objData.faces.Add(face);
                  }
                }
              }
            }
            if (!objDataList.Contains(objData))
              objDataList.Add(objData);
          }
        }
        foreach (obj_data objData in objDataList)
        {
          uint index1 = 0;
          foreach (KeyValuePair<uint, object> parsedSection in this.parsed_sections)
          {
            if (index1 == 0U)
            {
              if (parsedSection.Value is Model)
              {
                ulong result;
                if (ulong.TryParse(objData.object_name, out result))
                {
                  if ((long) result == (long) ((Model) parsedSection.Value).object3D.hashname)
                    index1 = parsedSection.Key;
                }
                else if ((long) Hash64.HashString(objData.object_name, 0UL) == (long) ((Model) parsedSection.Value).object3D.hashname)
                  index1 = parsedSection.Key;
              }
            }
            else
              break;
          }
          if (index1 != 0U)
          {
            PassthroughGP parsedSection1 = (PassthroughGP) this.parsed_sections[((Model) this.parsed_sections[index1]).passthroughGP_ID];
            Geometry parsedSection2 = (Geometry) this.parsed_sections[parsedSection1.geometry_section];
            Topology parsedSection3 = (Topology) this.parsed_sections[parsedSection1.topology_section];
            Vector2D[] vector2DArray = new Vector2D[parsedSection2.verts.Count];
            for (int index2 = 0; index2 < vector2DArray.Length; ++index2)
              vector2DArray[index2] = new Vector2D(100f, 100f);
            Vector2D vector2D = new Vector2D(100f, 100f);
            if (parsedSection3.facelist.Count != objData.faces.Count / 3)
              return false;
            int index3 = 0;
            while (index3 < parsedSection3.facelist.Count)
            {
              Face face1 = objData.faces[index3];
              Face face2 = objData.faces[index3 + 1];
              Face face3 = objData.faces[index3 + 2];
              if (objData.uv.Count > 0)
              {
                if (vector2DArray[(int) parsedSection3.facelist[index3 / 3].x].Equals((object) vector2D))
                  vector2DArray[(int) parsedSection3.facelist[index3 / 3].x] = objData.uv[(int) face1.y];
                if (vector2DArray[(int) parsedSection3.facelist[index3 / 3].y].Equals((object) vector2D))
                  vector2DArray[(int) parsedSection3.facelist[index3 / 3].y] = objData.uv[(int) face2.y];
                if (vector2DArray[(int) parsedSection3.facelist[index3 / 3].z].Equals((object) vector2D))
                  vector2DArray[(int) parsedSection3.facelist[index3 / 3].z] = objData.uv[(int) face3.y];
              }
              index3 += 3;
            }
            parsedSection2.pattern_uvs = ((IEnumerable<Vector2D>) vector2DArray).ToList<Vector2D>();
            ((Geometry) this.parsed_sections[parsedSection1.geometry_section]).pattern_uvs = ((IEnumerable<Vector2D>) vector2DArray).ToList<Vector2D>();
          }
        }
      }
      catch (Exception ex)
      {
        //int num = (int) MessageBox.Show(ex.ToString());
        return false;
      }
      return true;
    }

    public bool updateRP(string rp)
    {
      ulong num = Hash64.HashString(rp, 0UL);
      foreach (object obj in this.parsed_sections.Values)
      {
        if (obj is Object3D && (long) (obj as Object3D).hashname == (long) num)
        {
          StaticStorage.rp_id = (obj as Object3D).id;
          return true;
        }
      }
      return false;
    }

    public bool ImportNewObj(string filepath, bool addNew = false)
    {
      Console.WriteLine("Importing new obj with file: " + filepath);
      List<obj_data> objDataList1 = new List<obj_data>();
      List<obj_data> objDataList2 = new List<obj_data>();
      try
      {
        using (FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
          using (StreamReader streamReader = new StreamReader((Stream) fileStream))
          {
            obj_data objData = new obj_data();
            bool flag = false;
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            string key = (string) null;
            string str;
            while ((str = streamReader.ReadLine()) != null)
            {
              if (!str.StartsWith("#"))
              {
                if (str.StartsWith("o ") || str.StartsWith("g "))
                {
                  if (flag)
                  {
                    flag = false;
                    num1 += objData.verts.Count;
                    num2 += objData.uv.Count;
                    num3 += objData.normals.Count;
                    objDataList1.Add(objData);
                    objData = new obj_data();
                    key = (string) null;
                  }
                  if (string.IsNullOrEmpty(objData.object_name))
                  {
                    objData.object_name = str.Substring(2);
                    Console.WriteLine("Object " + (object) (objDataList1.Count + 1) + " named: " + objData.object_name);
                  }
                }
                else if (str.StartsWith("usemtl "))
                  objData.material_name = str.Substring(7);
                else if (str.StartsWith("v "))
                {
                  if (flag)
                  {
                    flag = false;
                    num1 += objData.verts.Count;
                    num2 += objData.uv.Count;
                    num3 += objData.normals.Count;
                    objDataList1.Add(objData);
                    objData = new obj_data();
                  }
                  string[] strArray = str.Replace("  ", " ").Split(' ');
                  objData.verts.Add(new Vector3D()
                  {
                    X = Convert.ToSingle(strArray[1], (IFormatProvider) CultureInfo.InvariantCulture),
                    Y = Convert.ToSingle(strArray[2], (IFormatProvider) CultureInfo.InvariantCulture),
                    Z = Convert.ToSingle(strArray[3], (IFormatProvider) CultureInfo.InvariantCulture)
                  });
                }
                else if (str.StartsWith("vt "))
                {
                  if (flag)
                  {
                    flag = false;
                    num1 += objData.verts.Count;
                    num2 += objData.uv.Count;
                    num3 += objData.normals.Count;
                    objDataList1.Add(objData);
                    objData = new obj_data();
                  }
                  string[] strArray = str.Split(' ');
                  objData.uv.Add(new Vector2D()
                  {
                    X = Convert.ToSingle(strArray[1], (IFormatProvider) CultureInfo.InvariantCulture),
                    Y = Convert.ToSingle(strArray[2], (IFormatProvider) CultureInfo.InvariantCulture)
                  });
                }
                else if (str.StartsWith("vn "))
                {
                  if (flag)
                  {
                    flag = false;
                    num1 += objData.verts.Count;
                    num2 += objData.uv.Count;
                    num3 += objData.normals.Count;
                    objDataList1.Add(objData);
                    objData = new obj_data();
                  }
                  string[] strArray = str.Split(' ');
                  objData.normals.Add(new Vector3D()
                  {
                    X = Convert.ToSingle(strArray[1], (IFormatProvider) CultureInfo.InvariantCulture),
                    Y = Convert.ToSingle(strArray[2], (IFormatProvider) CultureInfo.InvariantCulture),
                    Z = Convert.ToSingle(strArray[3], (IFormatProvider) CultureInfo.InvariantCulture)
                  });
                }
                else if (str.StartsWith("s "))
                  key = str.Substring(2);
                else if (str.StartsWith("f "))
                {
                  flag = true;
                  if (key != null)
                  {
                    if (objData.shading_groups.ContainsKey(key))
                      objData.shading_groups[key].Add(objData.faces.Count);
                    else
                      objData.shading_groups.Add(key, new List<int>()
                      {
                        objData.faces.Count
                      });
                  }
                  string[] strArray = str.Substring(2).Split(' ');
                  for (int index = 0; index < 3; ++index)
                  {
                    Face face = new Face();
                    if (objData.verts.Count > 0)
                      face.x = (ushort) ((int) Convert.ToUInt16(strArray[index].Split('/')[0]) - num1 - 1);
                    if (objData.uv.Count > 0)
                      face.y = (ushort) ((int) Convert.ToUInt16(strArray[index].Split('/')[1]) - num2 - 1);
                    if (objData.normals.Count > 0)
                      face.z = (ushort) ((int) Convert.ToUInt16(strArray[index].Split('/')[2]) - num3 - 1);
                    if (face.x < (ushort) 0 || face.y < (ushort) 0 || face.z < (ushort) 0)
                      throw new Exception();
                    objData.faces.Add(face);
                  }
                }
              }
            }
            if (!objDataList1.Contains(objData))
              objDataList1.Add(objData);
          }
        }
        foreach (obj_data objData in objDataList1)
        {
          uint index1 = 0;
          foreach (KeyValuePair<uint, object> parsedSection in this.parsed_sections)
          {
            if (index1 == 0U)
            {
              if (parsedSection.Value is Model)
              {
                ulong result;
                if (ulong.TryParse(objData.object_name, out result))
                {
                  if ((long) result == (long) ((Model) parsedSection.Value).object3D.hashname)
                    index1 = parsedSection.Key;
                }
                else if ((long) Hash64.HashString(objData.object_name, 0UL) == (long) ((Model) parsedSection.Value).object3D.hashname)
                  index1 = parsedSection.Key;
              }
            }
            else
              break;
          }
          if (index1 == 0U)
          {
            objDataList2.Add(objData);
          }
          else
          {
            Model parsedSection1 = (Model) this.parsed_sections[index1];
            PassthroughGP parsedSection2 = (PassthroughGP) this.parsed_sections[parsedSection1.passthroughGP_ID];
            Geometry parsedSection3 = (Geometry) this.parsed_sections[parsedSection2.geometry_section];
            Topology parsedSection4 = (Topology) this.parsed_sections[parsedSection2.topology_section];
            int size = (int) parsedSection3.size;
            int count1 = parsedSection3.verts.Count;
            int count2 = parsedSection3.uvs.Count;
            int count3 = parsedSection3.normals.Count;
            uint num1 = parsedSection4.size - (uint) (parsedSection4.facelist.Count * 6);
            List<Face> faceList = new List<Face>();
            List<int> intList1 = new List<int>();
            Dictionary<int, Face> dictionary1 = new Dictionary<int, Face>();
            for (int index2 = 0; index2 < objData.faces.Count; ++index2)
            {
              Face face1 = objData.faces[index2];
              bool flag = false;
              foreach (Face face2 in faceList)
              {
                if ((int) face2.x == (int) face1.x && (int) face2.y != (int) face1.y)
                {
                  intList1.Add(index2);
                  flag = true;
                  break;
                }
              }
              if (!flag)
                faceList.Add(face1);
            }
            Dictionary<int, Face> dictionary2 = new Dictionary<int, Face>();
            foreach (int key in intList1)
            {
              int index2 = -1;
              foreach (KeyValuePair<int, Face> keyValuePair in dictionary2)
              {
                Face face = keyValuePair.Value;
                if ((int) face.x == (int) objData.faces[key].x && (int) face.y == (int) objData.faces[key].y)
                  index2 = keyValuePair.Key;
              }
              Face face1 = new Face();
              if (index2 > -1)
              {
                face1.x = objData.faces[index2].x;
                face1.y = objData.faces[index2].y;
                face1.z = objData.faces[key].z;
              }
              else
              {
                face1.x = (ushort) objData.verts.Count;
                face1.y = objData.faces[key].y;
                face1.z = objData.faces[key].z;
                objData.verts.Add(objData.verts[(int) objData.faces[key].x]);
                dictionary2.Add(key, objData.faces[key]);
              }
              objData.faces[key] = face1;
            }
            Vector3D vector3D1 = new Vector3D();
            Vector3D vector3D2 = new Vector3D();
            foreach (Vector3D vert in objData.verts)
            {
              if ((double) vert.Z > (double) vector3D1.Z)
                vector3D1.Z = vert.Z;
              if ((double) vert.Z < (double) vector3D2.Z)
                vector3D2.Z = vert.Z;
              if ((double) vert.X < (double) vector3D1.X)
                vector3D1.X = vert.X;
              if ((double) vert.X > (double) vector3D2.X)
                vector3D2.X = vert.X;
              if ((double) vert.Y < (double) vector3D1.Y)
                vector3D1[2] = vert.Y;
              if ((double) vert.Y > (double) vector3D2.Y)
                vector3D2.Y = vert.Y;
            }
            List<Vector2D> vector2DList = new List<Vector2D>();
            List<Vector3D> vector3DList1 = new List<Vector3D>();
            List<Vector3D> vector3DList2 = new List<Vector3D>();
            List<Vector3D> vector3DList3 = new List<Vector3D>();
            List<int> intList2 = new List<int>();
            List<int> intList3 = new List<int>();
            Vector2D[] uvs = new Vector2D[objData.verts.Count];
            for (int index2 = 0; index2 < uvs.Length; ++index2)
              uvs[index2] = new Vector2D(100f, 100f);
            Vector2D vector2D = new Vector2D(100f, 100f);
            Vector3D[] normals = new Vector3D[objData.verts.Count];
            Vector3D[] tangents = new Vector3D[objData.verts.Count];
            Vector3D[] binormals = new Vector3D[objData.verts.Count];
            List<Face> faces = new List<Face>();
            int index3 = 0;
            while (index3 < objData.faces.Count)
            {
              Face face1 = objData.faces[index3];
              Face face2 = objData.faces[index3 + 1];
              Face face3 = objData.faces[index3 + 2];
              if (objData.uv.Count > 0)
              {
                if (uvs[(int) face1.x].Equals((object) vector2D))
                  uvs[(int) face1.x] = objData.uv[(int) face1.y];
                if (uvs[(int) face2.x].Equals((object) vector2D))
                  uvs[(int) face2.x] = objData.uv[(int) face2.y];
                if (uvs[(int) face3.x].Equals((object) vector2D))
                  uvs[(int) face3.x] = objData.uv[(int) face3.y];
              }
              if (objData.normals.Count > 0)
              {
                normals[(int) face1.x] = objData.normals[(int) face1.z];
                normals[(int) face2.x] = objData.normals[(int) face2.z];
                normals[(int) face3.x] = objData.normals[(int) face3.z];
              }
              faces.Add(new Face()
              {
                x = face1.x,
                y = face2.x,
                z = face3.x
              });
              index3 += 3;
            }
            List<Vector3D> verts = objData.verts;
            FileManager.ComputeTangentBasis(ref faces, ref verts, ref uvs, ref normals, ref tangents, ref binormals);
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
            uint num2 = 0;
            foreach (GeometryHeader header in parsedSection3.headers)
              num2 += numArray[(int) header.item_size];
            List<ModelItem> modelItemList = new List<ModelItem>();
            foreach (ModelItem modelItem in parsedSection1.items)
              modelItemList.Add(new ModelItem()
              {
                unknown1 = modelItem.unknown1,
                vertCount = (uint) faces.Count,
                unknown2 = modelItem.unknown2,
                faceCount = (uint) objData.verts.Count,
                material_id = modelItem.material_id
              });
            parsedSection1.items = modelItemList;
            parsedSection3.vert_count = (uint) objData.verts.Count;
            parsedSection3.verts = objData.verts;
            parsedSection4.facelist = faces;
            parsedSection3.uvs = ((IEnumerable<Vector2D>) uvs).ToList<Vector2D>();
            parsedSection3.normals = ((IEnumerable<Vector3D>) normals).ToList<Vector3D>();
            if (parsedSection1.version != 6U)
            {
              parsedSection1.bounds_min = vector3D1;
              parsedSection1.bounds_max = vector3D2;
            }
            uint num3 = (uint) ((int) num2 * objData.verts.Count + (8 + parsedSection3.headers.Count * 8) + parsedSection3.unknown_item_data.Count);
            if (parsedSection3.remaining_data != null)
              num3 += (uint) parsedSection3.remaining_data.Length;
            ((Geometry) this.parsed_sections[parsedSection2.geometry_section]).size = num3;
            ((Topology) this.parsed_sections[parsedSection2.topology_section]).size = num1 + (uint) (faces.Count * 6);
            ((Geometry) this.parsed_sections[parsedSection2.geometry_section]).size = num2 * (uint) objData.verts.Count;
            ((Geometry) this.parsed_sections[parsedSection2.geometry_section]).vert_count = (uint) objData.verts.Count;
            ((Geometry) this.parsed_sections[parsedSection2.geometry_section]).verts = objData.verts;
            ((Geometry) this.parsed_sections[parsedSection2.geometry_section]).normals = ((IEnumerable<Vector3D>) normals).ToList<Vector3D>();
            ((Geometry) this.parsed_sections[parsedSection2.geometry_section]).uvs = ((IEnumerable<Vector2D>) uvs).ToList<Vector2D>();
            ((Geometry) this.parsed_sections[parsedSection2.geometry_section]).unknown20 = ((IEnumerable<Vector3D>) tangents).ToList<Vector3D>();
            ((Geometry) this.parsed_sections[parsedSection2.geometry_section]).unknown21 = ((IEnumerable<Vector3D>) binormals).ToList<Vector3D>();
            ((Topology) this.parsed_sections[parsedSection2.topology_section]).count1 = (uint) (faces.Count * 3);
            ((Topology) this.parsed_sections[parsedSection2.topology_section]).facelist = faces;
          }
        }
        if (addNew)
        {
          foreach (obj_data newobject in objDataList2)
          {
            Material material = new Material((uint) (newobject.object_name + ".material").GetHashCode(), newobject.material_name);
            MaterialGroup materialGroup = new MaterialGroup((uint) (newobject.object_name + ".materialGroup").GetHashCode(), material.id);
            Geometry geometry = new Geometry((uint) (newobject.object_name + ".geom").GetHashCode(), newobject);
            Topology topology = new Topology((uint) (newobject.object_name + ".topo").GetHashCode(), newobject);
            PassthroughGP passthroughGp = new PassthroughGP((uint) (newobject.object_name + ".passGP").GetHashCode(), geometry.id, topology.id);
            TopologyIP topologyIp = new TopologyIP((uint) (newobject.object_name + ".topoIP").GetHashCode(), topology.id);
            Model model = new Model(newobject, passthroughGp.id, topologyIp.id, materialGroup.id);
            List<Face> faceList = new List<Face>();
            List<int> intList1 = new List<int>();
            Dictionary<int, Face> dictionary1 = new Dictionary<int, Face>();
            for (int index = 0; index < newobject.faces.Count; ++index)
            {
              Face face1 = newobject.faces[index];
              bool flag = false;
              foreach (Face face2 in faceList)
              {
                if ((int) face2.x == (int) face1.x && (int) face2.y != (int) face1.y)
                {
                  intList1.Add(index);
                  flag = true;
                  break;
                }
              }
              if (!flag)
                faceList.Add(face1);
            }
            Dictionary<int, Face> dictionary2 = new Dictionary<int, Face>();
            foreach (int key in intList1)
            {
              int index = -1;
              foreach (KeyValuePair<int, Face> keyValuePair in dictionary2)
              {
                Face face = keyValuePair.Value;
                if ((int) face.x == (int) newobject.faces[key].x && (int) face.y == (int) newobject.faces[key].y)
                  index = keyValuePair.Key;
              }
              Face face1 = new Face();
              if (index > -1)
              {
                face1.x = newobject.faces[index].x;
                face1.y = newobject.faces[index].y;
                face1.z = newobject.faces[key].z;
              }
              else
              {
                face1.x = (ushort) newobject.verts.Count;
                face1.y = newobject.faces[key].y;
                face1.z = newobject.faces[key].z;
                newobject.verts.Add(newobject.verts[(int) newobject.faces[key].x]);
                dictionary2.Add(key, newobject.faces[key]);
              }
              newobject.faces[key] = face1;
            }
            Vector3D vector3D1 = new Vector3D();
            Vector3D vector3D2 = new Vector3D();
            foreach (Vector3D vert in newobject.verts)
            {
              if ((double) vert.Z > (double) vector3D1.Z)
                vector3D1.Z = vert.Z;
              if ((double) vert.Z < (double) vector3D2.Z)
                vector3D2.Z = vert.Z;
              if ((double) vert.X < (double) vector3D1.X)
                vector3D1.X = vert.X;
              if ((double) vert.X > (double) vector3D2.X)
                vector3D2.X = vert.X;
              if ((double) vert.Y < (double) vector3D1.Y)
                vector3D1[2] = vert.Y;
              if ((double) vert.Y > (double) vector3D2.Y)
                vector3D2.Y = vert.Y;
            }
            List<Vector2D> vector2DList = new List<Vector2D>();
            List<Vector3D> vector3DList1 = new List<Vector3D>();
            List<Vector3D> vector3DList2 = new List<Vector3D>();
            List<Vector3D> vector3DList3 = new List<Vector3D>();
            List<int> intList2 = new List<int>();
            List<int> intList3 = new List<int>();
            Vector2D[] uvs = new Vector2D[newobject.verts.Count];
            for (int index = 0; index < uvs.Length; ++index)
              uvs[index] = new Vector2D(100f, 100f);
            Vector2D vector2D = new Vector2D(100f, 100f);
            Vector3D[] normals = new Vector3D[newobject.verts.Count];
            for (int index = 0; index < normals.Length; ++index)
              normals[index] = new Vector3D(0.0f, 0.0f, 0.0f);
            Vector3D[] tangents = new Vector3D[newobject.verts.Count];
            Vector3D[] binormals = new Vector3D[newobject.verts.Count];
            List<Face> faces = new List<Face>();
            int index1 = 0;
            while (index1 < newobject.faces.Count)
            {
              Face face1 = newobject.faces[index1];
              Face face2 = newobject.faces[index1 + 1];
              Face face3 = newobject.faces[index1 + 2];
              if (newobject.uv.Count > 0)
              {
                if (uvs[(int) face1.x].Equals((object) vector2D))
                  uvs[(int) face1.x] = newobject.uv[(int) face1.y];
                if (uvs[(int) face2.x].Equals((object) vector2D))
                  uvs[(int) face2.x] = newobject.uv[(int) face2.y];
                if (uvs[(int) face3.x].Equals((object) vector2D))
                  uvs[(int) face3.x] = newobject.uv[(int) face3.y];
              }
              if (newobject.normals.Count > 0)
              {
                normals[(int) face1.x] += newobject.normals[(int) face1.z];
                normals[(int) face2.x] += newobject.normals[(int) face2.z];
                normals[(int) face3.x] += newobject.normals[(int) face3.z];
              }
              faces.Add(new Face()
              {
                x = face1.x,
                y = face2.x,
                z = face3.x
              });
              index1 += 3;
            }
            for (int index2 = 0; index2 < normals.Length; ++index2)
              normals[index2].Normalize();
            List<Vector3D> verts = newobject.verts;
            FileManager.ComputeTangentBasis(ref faces, ref verts, ref uvs, ref normals, ref tangents, ref binormals);
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
            uint num = 0;
            foreach (GeometryHeader header in geometry.headers)
              num += numArray[(int) header.item_size];
            List<ModelItem> modelItemList = new List<ModelItem>();
            foreach (ModelItem modelItem in model.items)
              modelItemList.Add(new ModelItem()
              {
                unknown1 = modelItem.unknown1,
                vertCount = (uint) faces.Count,
                unknown2 = modelItem.unknown2,
                faceCount = (uint) newobject.verts.Count,
                material_id = modelItem.material_id
              });
            model.items = modelItemList;
            geometry.vert_count = (uint) newobject.verts.Count;
            geometry.verts = newobject.verts;
            topology.facelist = faces;
            geometry.uvs = ((IEnumerable<Vector2D>) uvs).ToList<Vector2D>();
            geometry.normals = ((IEnumerable<Vector3D>) normals).ToList<Vector3D>();
            if (model.version != 6U)
            {
              model.bounds_min = vector3D1;
              model.bounds_max = vector3D2;
            }
            geometry.vert_count = (uint) newobject.verts.Count;
            geometry.verts = newobject.verts;
            geometry.normals = ((IEnumerable<Vector3D>) normals).ToList<Vector3D>();
            geometry.uvs = ((IEnumerable<Vector2D>) uvs).ToList<Vector2D>();
            geometry.unknown20 = ((IEnumerable<Vector3D>) tangents).ToList<Vector3D>();
            geometry.unknown21 = ((IEnumerable<Vector3D>) binormals).ToList<Vector3D>();
            topology.count1 = (uint) (faces.Count * 3);
            topology.facelist = faces;
            this.parsed_sections.Add(material.id, (object) material);
            this.sections.Add(new SectionHeader(material.id));
            this.parsed_sections.Add(materialGroup.id, (object) materialGroup);
            this.sections.Add(new SectionHeader(materialGroup.id));
            this.parsed_sections.Add(geometry.id, (object) geometry);
            this.sections.Add(new SectionHeader(geometry.id));
            this.parsed_sections.Add(topology.id, (object) topology);
            this.sections.Add(new SectionHeader(topology.id));
            this.parsed_sections.Add(passthroughGp.id, (object) passthroughGp);
            this.sections.Add(new SectionHeader(passthroughGp.id));
            this.parsed_sections.Add(topologyIp.id, (object) topologyIp);
            this.sections.Add(new SectionHeader(topologyIp.id));
            this.parsed_sections.Add(model.id, (object) model);
            this.sections.Add(new SectionHeader(model.id));
          }
        }
      }
      catch (Exception ex)
      {
        //int num = (int) MessageBox.Show(ex.ToString());
        return false;
      }
      return true;
    }

    public bool GenerateModelFromObj(string filepath)
    {
      Console.WriteLine("Importing new obj with file: " + filepath);
      List<obj_data> objDataList = new List<obj_data>();
      try
      {
        using (FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
          using (StreamReader streamReader = new StreamReader((Stream) fileStream))
          {
            obj_data objData = new obj_data();
            bool flag = false;
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            string str1;
            while ((str1 = streamReader.ReadLine()) != null)
            {
              if (!str1.StartsWith("#"))
              {
                if (str1.StartsWith("o ") || str1.StartsWith("g "))
                {
                  if (flag && objData.faces.Count > 0)
                  {
                    flag = false;
                    num1 += objData.verts.Count;
                    num2 += objData.uv.Count;
                    num3 += objData.normals.Count;
                    objDataList.Add(objData);
                    objData = new obj_data();
                  }
                  objData.object_name = str1.Substring(2);
                }
                else if (str1.StartsWith("v "))
                {
                  if (flag)
                  {
                    flag = false;
                    num1 += objData.verts.Count;
                    num2 += objData.uv.Count;
                    num3 += objData.normals.Count;
                    objDataList.Add(objData);
                    objData = new obj_data();
                  }
                  string[] strArray = str1.Replace("  ", " ").Split(' ');
                  objData.verts.Add(new Vector3D()
                  {
                    X = Convert.ToSingle(strArray[1], (IFormatProvider) CultureInfo.InvariantCulture),
                    Y = Convert.ToSingle(strArray[2], (IFormatProvider) CultureInfo.InvariantCulture),
                    Z = Convert.ToSingle(strArray[3], (IFormatProvider) CultureInfo.InvariantCulture)
                  });
                }
                else if (str1.StartsWith("vt "))
                {
                  if (flag)
                  {
                    flag = false;
                    num1 += objData.verts.Count;
                    num2 += objData.uv.Count;
                    num3 += objData.normals.Count;
                    objDataList.Add(objData);
                    objData = new obj_data();
                  }
                  string[] strArray = str1.Split(' ');
                  objData.uv.Add(new Vector2D()
                  {
                    X = Convert.ToSingle(strArray[1], (IFormatProvider) CultureInfo.InvariantCulture),
                    Y = Convert.ToSingle(strArray[2], (IFormatProvider) CultureInfo.InvariantCulture)
                  });
                }
                else if (str1.StartsWith("vn "))
                {
                  if (flag)
                  {
                    flag = false;
                    num1 += objData.verts.Count;
                    num2 += objData.uv.Count;
                    num3 += objData.normals.Count;
                    objDataList.Add(objData);
                    objData = new obj_data();
                  }
                  string[] strArray = str1.Split(' ');
                  objData.normals.Add(new Vector3D()
                  {
                    X = Convert.ToSingle(strArray[1], (IFormatProvider) CultureInfo.InvariantCulture),
                    Y = Convert.ToSingle(strArray[2], (IFormatProvider) CultureInfo.InvariantCulture),
                    Z = Convert.ToSingle(strArray[3], (IFormatProvider) CultureInfo.InvariantCulture)
                  });
                }
                else if (str1.StartsWith("f "))
                {
                  flag = true;
                  string str2 = str1.Substring(2);
                  char[] chArray = new char[1]{ ' ' };
                  foreach (string str3 in str2.Split(chArray))
                  {
                    Face face = new Face();
                    if (objData.verts.Count > 0)
                      face.x = (ushort) ((int) Convert.ToUInt16(str3.Split('/')[0]) - num1 - 1);
                    if (objData.uv.Count > 0)
                      face.y = (ushort) ((int) Convert.ToUInt16(str3.Split('/')[1]) - num2 - 1);
                    if (objData.normals.Count > 0)
                      face.z = (ushort) ((int) Convert.ToUInt16(str3.Split('/')[2]) - num3 - 1);
                    if (face.x < (ushort) 0 || face.y < (ushort) 0 || face.z < (ushort) 0)
                      throw new Exception();
                    objData.faces.Add(face);
                  }
                }
              }
            }
            if (!objDataList.Contains(objData))
              objDataList.Add(objData);
          }
        }
      }
      catch (Exception ex)
      {
        //int num = (int) MessageBox.Show(ex.ToString());
        return false;
      }
      return true;
    }

    public bool GenerateNewModel(string filename)
    {
      List<Animation> animationList = new List<Animation>();
      List<Author> authorList = new List<Author>();
      List<MaterialGroup> materialGroupList = new List<MaterialGroup>();
      List<Object3D> object3DList = new List<Object3D>();
      List<Model> modelList = new List<Model>();
      foreach (SectionHeader section in this.sections)
      {
        if (this.parsed_sections.Keys.Contains<uint>(section.id))
        {
          object parsedSection = this.parsed_sections[section.id];
          if (parsedSection is Animation)
            animationList.Add(parsedSection as Animation);
          else if (parsedSection is Author)
            authorList.Add(parsedSection as Author);
          else if (parsedSection is MaterialGroup)
            materialGroupList.Add(parsedSection as MaterialGroup);
          else if (parsedSection is Object3D)
            object3DList.Add(parsedSection as Object3D);
          else if (parsedSection is Model)
            modelList.Add(parsedSection as Model);
        }
      }
      try
      {
        using (FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write))
        {
          using (BinaryWriter outstream = new BinaryWriter((Stream) fileStream))
          {
            outstream.Write(-1);
            outstream.Write(100U);
            int count = this.sections.Count;
            outstream.Write(count);
            foreach (Animation animation in animationList)
              animation.StreamWrite(outstream);
            foreach (Author author in authorList)
              author.StreamWrite(outstream);
            foreach (MaterialGroup materialGroup in materialGroupList)
            {
              materialGroup.StreamWrite(outstream);
              foreach (uint index in materialGroup.items)
              {
                if (this.parsed_sections.Keys.Contains<uint>(index))
                  (this.parsed_sections[index] as Material).StreamWrite(outstream);
              }
            }
            foreach (Object3D object3D in object3DList)
              object3D.StreamWrite(outstream);
            foreach (Model model in modelList)
              model.StreamWrite(outstream);
            foreach (SectionHeader section in this.sections)
            {
              if (this.parsed_sections.Keys.Contains<uint>(section.id))
              {
                object parsedSection = this.parsed_sections[section.id];
                if (parsedSection is Unknown)
                  (parsedSection as Unknown).StreamWrite(outstream);
                else if (!(parsedSection is Animation) && !(parsedSection is Author) && (!(parsedSection is MaterialGroup) && !(parsedSection is Material)) && (!(parsedSection is Object3D) && !(parsedSection is Model)))
                {
                  if (parsedSection is Geometry)
                    (parsedSection as Geometry).StreamWrite(outstream);
                  else if (parsedSection is Topology)
                    (parsedSection as Topology).StreamWrite(outstream);
                  else if (parsedSection is PassthroughGP)
                    (parsedSection as PassthroughGP).StreamWrite(outstream);
                  else if (parsedSection is TopologyIP)
                    (parsedSection as TopologyIP).StreamWrite(outstream);
                  else if (parsedSection is Bones)
                    (parsedSection as Bones).StreamWrite(outstream);
                  else if (parsedSection is SkinBones)
                    (parsedSection as SkinBones).StreamWrite(outstream);
                  else if (parsedSection is QuatLinearRotationController)
                    (parsedSection as QuatLinearRotationController).StreamWrite(outstream);
                  else if (parsedSection is LinearVector3Controller)
                    (parsedSection as LinearVector3Controller).StreamWrite(outstream);
                  else
                    Console.WriteLine("Tried to export an unknown section.");
                }
              }
            }
            if (this.leftover_data != null)
              outstream.Write(this.leftover_data);
            fileStream.Position = 4L;
            outstream.Write((uint) fileStream.Length);
          }
        }
      }
      catch (Exception ex)
      {
        //int num = (int) MessageBox.Show(ex.ToString());
        return false;
      }
      return true;
    }

    private static void ComputeTangentBasis(ref List<Face> faces, ref List<Vector3D> verts, ref Vector2D[] uvs, ref Vector3D[] normals, ref Vector3D[] tangents, ref Vector3D[] binormals)
    {
      List<ushort> ushortList = new List<ushort>();
      foreach (Face face in faces)
      {
        Vector3D vector3D1 = verts[(int) face.x];
        Vector3D vector3D2 = verts[(int) face.y];
        Vector3D vector3D3 = verts[(int) face.z];
        Vector2D vector2D1 = uvs[(int) face.x];
        Vector2D vector2D2 = uvs[(int) face.y];
        Vector2D vector2D3 = uvs[(int) face.z];
        float num1 = uvs[(int) face.z].X - uvs[(int) face.x].X;
        float num2 = uvs[(int) face.z].Y - uvs[(int) face.x].Y;
        float num3 = uvs[(int) face.y].X - uvs[(int) face.x].X;
        float num4 = uvs[(int) face.y].Y - uvs[(int) face.x].Y;
        float num5 = (float) ((double) num1 * (double) num1 + (double) num2 * (double) num2);
        float num6 = (float) ((double) num1 * (double) num3 + (double) num2 * (double) num4);
        float num7 = (float) ((double) num3 * (double) num3 + (double) num4 * (double) num4);
        float num8 = (float) ((double) num5 * (double) num7 - (double) num6 * (double) num6);
        float num9 = 1f;
        float num10 = 1f;
        if ((double) num8 != 0.0)
        {
          num9 = (float) ((double) num7 * (double) num1 - (double) num6 * (double) num3) / num8;
          num10 = (float) ((double) num5 * (double) num3 - (double) num6 * (double) num1) / num8;
        }
        Vector3D v1 = verts[(int) face.z] * num9 + verts[(int) face.y] * num10 - verts[(int) face.x] * (num9 + num10);
        if (!ushortList.Contains(face.x))
        {
          binormals[(int) face.x] = Vector3D.Cross(v1, normals[(int) face.x]);
          binormals[(int) face.x].Normalize();
          tangents[(int) face.x] = Vector3D.Cross(binormals[(int) face.x], normals[(int) face.x]);
          tangents[(int) face.x].Normalize();
          ushortList.Add(face.x);
        }
        if (!ushortList.Contains(face.y))
        {
          binormals[(int) face.y] = Vector3D.Cross(v1, normals[(int) face.y]);
          binormals[(int) face.y].Normalize();
          tangents[(int) face.y] = Vector3D.Cross(binormals[(int) face.y], normals[(int) face.y]);
          tangents[(int) face.y].Normalize();
          ushortList.Add(face.y);
        }
        if (!ushortList.Contains(face.z))
        {
          binormals[(int) face.z] = Vector3D.Cross(v1, normals[(int) face.z]);
          binormals[(int) face.z].Normalize();
          tangents[(int) face.z] = Vector3D.Cross(binormals[(int) face.z], normals[(int) face.z]);
          tangents[(int) face.z].Normalize();
          ushortList.Add(face.z);
        }
      }
    }

    private static float fabsf(float p)
    {
      return Math.Abs(p);
    }
  }
}
