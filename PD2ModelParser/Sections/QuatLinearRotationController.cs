// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.QuatLinearRotationController
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.Collections.Generic;
using System.IO;

namespace PD2ModelParser.Sections
{
  internal class QuatLinearRotationController
  {
    private static uint quatlinearrotationcontroller_tag = 1686773868;
    public List<QuatLinearRotationController_KeyFrame> keyframes = new List<QuatLinearRotationController_KeyFrame>();
    public uint id;
    public uint size;
    public ulong hashname;
    public byte flag0;
    public byte flag1;
    public byte flag2;
    public byte flag3;
    public uint unknown1;
    public float keyframe_length;
    public uint keyframe_count;
    public byte[] remaining_data;

    public QuatLinearRotationController(BinaryReader instream, SectionHeader section)
    {
      this.id = section.id;
      this.size = section.size;
      this.hashname = instream.ReadUInt64();
      this.flag0 = instream.ReadByte();
      this.flag1 = instream.ReadByte();
      this.flag2 = instream.ReadByte();
      this.flag3 = instream.ReadByte();
      this.unknown1 = instream.ReadUInt32();
      this.keyframe_length = instream.ReadSingle();
      this.keyframe_count = instream.ReadUInt32();
      for (int index = 0; (long) index < (long) this.keyframe_count; ++index)
        this.keyframes.Add(new QuatLinearRotationController_KeyFrame(instream));
      this.remaining_data = (byte[]) null;
      if (section.offset + 12L + (long) section.size <= instream.BaseStream.Position)
        return;
      this.remaining_data = instream.ReadBytes((int) (section.offset + 12L + (long) section.size - instream.BaseStream.Position));
    }

    public void StreamWrite(BinaryWriter outstream)
    {
      outstream.Write(QuatLinearRotationController.quatlinearrotationcontroller_tag);
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
      outstream.Write(this.flag0);
      outstream.Write(this.flag1);
      outstream.Write(this.flag2);
      outstream.Write(this.flag3);
      outstream.Write(this.unknown1);
      outstream.Write(this.keyframe_length);
      outstream.Write(this.keyframe_count);
      foreach (QuatLinearRotationController_KeyFrame keyframe in this.keyframes)
        keyframe.StreamWriteData(outstream);
      if (this.remaining_data == null)
        return;
      outstream.Write(this.remaining_data);
    }

    public override string ToString()
    {
      string str = this.keyframes.Count == 0 ? "none" : "";
      bool flag = true;
      foreach (QuatLinearRotationController_KeyFrame keyframe in this.keyframes)
      {
        str = str + (flag ? (object) "" : (object) ", ") + (object) keyframe;
        flag = false;
      }
      return "[QuatLinearRotationController] ID: " + (object) this.id + " size: " + (object) this.size + " hashname: " + StaticStorage.hashindex.GetString(this.hashname) + " flag0: " + (object) this.flag0 + " flag1: " + (object) this.flag1 + " flag2: " + (object) this.flag2 + " flag3: " + (object) this.flag3 + " unknown1: " + (object) this.unknown1 + " keyframe_length: " + (object) this.keyframe_length + " count: " + (object) this.keyframe_count + " items: [ " + str + " ] " + (this.remaining_data != null ? (object) (" REMAINING DATA! " + (object) this.remaining_data.Length + " bytes") : (object) "");
    }
  }
}
