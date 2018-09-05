using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PD2ModelParser.Abstract
{
    public interface ISectionHeaderParser
    {
        List<SectionHeader> Parse(BinaryReader binaryReader);
    }
}
