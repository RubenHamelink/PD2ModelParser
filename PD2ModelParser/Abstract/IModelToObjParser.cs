using System;
using System.Collections.Generic;
using System.Text;

namespace PD2ModelParser.Abstract
{
    public interface IModelToObjParser
    {
        byte[] Parse(byte[] model);
    }
}
