using System;
using System.Collections.Generic;
using System.Text;
using PD2ModelParser.Sections;

namespace PD2ModelParser.Abstract
{
    public interface IObjParser
    {
        byte[] Parse(List<SectionHeader> sectionHeaders, Dictionary<uint, Section> parsedSections);
    }
}
