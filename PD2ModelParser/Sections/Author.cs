// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Sections.Author
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.IO;
using System.Text;

namespace PD2ModelParser.Sections
{
  internal class Author
  {
    private static uint author_tag = 1982055525;
    public uint id;
    public uint size;
    public ulong hashname;
    public string email;
    public string source_file;
    public uint unknown2;
    public byte[] remaining_data;

    public Author(BinaryReader instream, SectionHeader section)
    {
      this.id = section.id;
      this.size = section.size;
      this.hashname = instream.ReadUInt64();
      StringBuilder stringBuilder1 = new StringBuilder();
      int num1;
      while ((num1 = (int) instream.ReadByte()) != 0)
        stringBuilder1.Append((char) num1);
      this.email = stringBuilder1.ToString();
      StringBuilder stringBuilder2 = new StringBuilder();
      int num2;
      while ((num2 = (int) instream.ReadByte()) != 0)
        stringBuilder2.Append((char) num2);
      this.source_file = stringBuilder2.ToString();
      this.unknown2 = instream.ReadUInt32();
      this.remaining_data = (byte[]) null;
      if (section.offset + 12L + (long) section.size <= instream.BaseStream.Position)
        return;
      this.remaining_data = instream.ReadBytes((int) (section.offset + 12L + (long) section.size - instream.BaseStream.Position));
    }

    public void StreamWrite(BinaryWriter outstream)
    {
      outstream.Write(Author.author_tag);
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
      byte num = 0;
      outstream.Write(this.hashname);
      outstream.Write(this.email.ToCharArray());
      outstream.Write(num);
      outstream.Write(this.source_file.ToCharArray());
      outstream.Write(num);
      outstream.Write(this.unknown2);
      if (this.remaining_data == null)
        return;
      outstream.Write(this.remaining_data);
    }

    public override string ToString()
    {
      return "[Author] ID: " + (object) this.id + " size: " + (object) this.size + " hashname: " + StaticStorage.hashindex.GetString(this.hashname) + " email: " + this.email + " Source file: " + this.source_file + " unknown2: " + (object) this.unknown2 + (this.remaining_data != null ? (object) (" REMAINING DATA! " + (object) this.remaining_data.Length + " bytes") : (object) "");
    }
  }
}
