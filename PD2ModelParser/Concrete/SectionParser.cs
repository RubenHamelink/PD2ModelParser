using System;
using System.Collections.Generic;
using System.IO;
using PD2ModelParser.Abstract;
using PD2ModelParser.Sections;

namespace PD2ModelParser.Concrete
{
    public class SectionParser : ISectionParser
    {
        public Dictionary<uint, Section> Parse(BinaryReader binaryReader, List<SectionHeader> sectionHeaders,
            Dictionary<uint, Func<Section>> sectionTypes)
        {
            Dictionary<uint, Section> parsedSections = new Dictionary<uint, Section>();
            foreach (SectionHeader sectionHeader in sectionHeaders)
            {
                Section section = new Unknown();
                if (sectionTypes.ContainsKey(sectionHeader.type))
                    section= sectionTypes[sectionHeader.type]();
                binaryReader.BaseStream.Position = sectionHeader.offset + 12L;
                LogSection(section, sectionHeader);
                section.StreamReadData(binaryReader, sectionHeader);
                parsedSections.Add(sectionHeader.id, section);
            }

            return parsedSections;
        }

        private static void LogSection(Section section, SectionHeader header)
        {
            string name = section == null ? "UNKNOWN" : section.GetType().Name;
            Console.WriteLine($"{name} Tag at " + header.offset + " Size: " + header.size);
        }
    }
}
