using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PD2ModelParser.Sections
{
    public abstract class Section
    {
        public abstract void StreamReadData(BinaryReader instream, SectionHeader section);
    }
}
