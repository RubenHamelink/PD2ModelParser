using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PD2ModelParser.Sections;

namespace PD2ModelParser.Abstract
{
    public interface ISectionParser
    {
        Dictionary<uint, Section> Parse(BinaryReader binaryReader, List<SectionHeader> sectionHeaders,
            Dictionary<uint, Func<Section>> sectionTypes);
    }
}
