using System;
using System.Collections.Generic;
using System.Text;

namespace PD2ModelParser.Abstract
{
    public interface IModelParser
    {
        void WriteToOutput(string input, string output);
    }
}
