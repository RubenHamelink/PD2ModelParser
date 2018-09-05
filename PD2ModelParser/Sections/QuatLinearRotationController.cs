// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.QuatLinearRotationController
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.Collections.Generic;
using System.IO;

namespace PD2ModelParser.Sections
{
    internal class QuatLinearRotationController : Section
    {
        private static readonly uint quatlinearrotationcontroller_tag = 1686773868;
        public byte flag0;
        public byte flag1;
        public byte flag2;
        public byte flag3;
        public ulong hashname;
        public uint id;
        public uint keyframe_count;
        public float keyframe_length;

        public List<QuatLinearRotationController_KeyFrame>
            keyframes = new List<QuatLinearRotationController_KeyFrame>();

        public byte[] remaining_data;
        public uint size;
        public uint unknown1;

        public QuatLinearRotationController()
        {
        }

        public QuatLinearRotationController(BinaryReader instream, SectionHeader section)
        {
            StreamReadData(instream, section);
        }

        public sealed override void StreamReadData(BinaryReader instream, SectionHeader section)
        {
            id = section.id;
            size = section.size;
            hashname = instream.ReadUInt64();
            flag0 = instream.ReadByte();
            flag1 = instream.ReadByte();
            flag2 = instream.ReadByte();
            flag3 = instream.ReadByte();
            unknown1 = instream.ReadUInt32();
            keyframe_length = instream.ReadSingle();
            keyframe_count = instream.ReadUInt32();
            for (int index = 0; (long) index < (long) keyframe_count; ++index)
                keyframes.Add(new QuatLinearRotationController_KeyFrame(instream));
            remaining_data = null;
            if (section.offset + 12L + section.size <= instream.BaseStream.Position)
                return;
            remaining_data =
                instream.ReadBytes((int) (section.offset + 12L + section.size - instream.BaseStream.Position));
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(quatlinearrotationcontroller_tag);
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
            outstream.Write(flag0);
            outstream.Write(flag1);
            outstream.Write(flag2);
            outstream.Write(flag3);
            outstream.Write(unknown1);
            outstream.Write(keyframe_length);
            outstream.Write(keyframe_count);
            foreach (QuatLinearRotationController_KeyFrame keyframe in keyframes)
                keyframe.StreamWriteData(outstream);
            if (remaining_data == null)
                return;
            outstream.Write(remaining_data);
        }

        public override string ToString()
        {
            string str = keyframes.Count == 0 ? "none" : "";
            bool flag = true;
            foreach (QuatLinearRotationController_KeyFrame keyframe in keyframes)
            {
                str = str + (flag ? "" : (object) ", ") + keyframe;
                flag = false;
            }

            return "[QuatLinearRotationController] ID: " + id + " size: " + size + " hashname: " +
                   StaticStorage.hashindex.GetString(hashname) + " flag0: " + flag0 + " flag1: " + flag1 + " flag2: " +
                   flag2 + " flag3: " + flag3 + " unknown1: " + unknown1 + " keyframe_length: " + keyframe_length +
                   " count: " + keyframe_count + " items: [ " + str + " ] " + (remaining_data != null
                       ? " REMAINING DATA! " + remaining_data.Length + " bytes"
                       : (object) "");
        }
    }
}