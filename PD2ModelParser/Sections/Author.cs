// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.Author
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.IO;
using System.Text;

namespace PD2ModelParser.Sections
{
    internal class Author : Section
    {
        private static readonly uint author_tag = 1982055525;
        public string email;
        public ulong hashname;
        public uint id;
        public byte[] remaining_data;
        public uint size;
        public string source_file;
        public uint unknown2;

        public Author()
        {
        }

        public Author(BinaryReader instream, SectionHeader section)
        {
            StreamReadData(instream, section);
        }

        public sealed override void StreamReadData(BinaryReader instream, SectionHeader section)
        {
            id = section.id;
            size = section.size;
            hashname = instream.ReadUInt64();
            StringBuilder stringBuilder1 = new StringBuilder();
            int num1;
            while ((num1 = instream.ReadByte()) != 0)
                stringBuilder1.Append((char) num1);
            email = stringBuilder1.ToString();
            StringBuilder stringBuilder2 = new StringBuilder();
            int num2;
            while ((num2 = instream.ReadByte()) != 0)
                stringBuilder2.Append((char) num2);
            source_file = stringBuilder2.ToString();
            unknown2 = instream.ReadUInt32();
            remaining_data = null;
            if (section.offset + 12L + section.size <= instream.BaseStream.Position)
                return;
            remaining_data =
                instream.ReadBytes((int) (section.offset + 12L + section.size - instream.BaseStream.Position));
        }

        public void StreamWrite(BinaryWriter outstream)
        {
            outstream.Write(author_tag);
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
            byte num = 0;
            outstream.Write(hashname);
            outstream.Write(email.ToCharArray());
            outstream.Write(num);
            outstream.Write(source_file.ToCharArray());
            outstream.Write(num);
            outstream.Write(unknown2);
            if (remaining_data == null)
                return;
            outstream.Write(remaining_data);
        }

        public override string ToString()
        {
            return "[Author] ID: " + id + " size: " + size + " hashname: " +
                   StaticStorage.hashindex.GetString(hashname) + " email: " + email + " Source file: " + source_file +
                   " unknown2: " + unknown2 + (remaining_data != null
                       ? " REMAINING DATA! " + remaining_data.Length + " bytes"
                       : (object) "");
        }
    }
}