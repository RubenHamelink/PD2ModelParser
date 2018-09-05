using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PD2ModelParser.Abstract;

namespace PD2ModelParser.Concrete
{
    public class SectionHeaderParser : ISectionHeaderParser
    {
        public List<SectionHeader> Parse(BinaryReader binaryReader)
        {
            List<SectionHeader> sections = new List<SectionHeader>();
            uint num1 = 0;
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
            Console.WriteLine("Size: " + (object)num4 + " bytes, Sections: " + (object)num6);
            for (int index = 0; index < num6; ++index)
            {
                SectionHeader sectionHeader = new SectionHeader(binaryReader);
                sections.Add(sectionHeader);
                Console.WriteLine((object)sectionHeader);
                num5 += sectionHeader.size + 12U;
                Console.WriteLine("Next offset: " + (object)num5);
                binaryReader.BaseStream.Position = (long)num5;
            }
            return sections;
        }
    }
}
