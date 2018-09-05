using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PD2ModelParser.Abstract;

namespace PD2ModelParser.Concrete
{
    public class ModelParser : IModelParser
    {
        private readonly IFileSystem fileSystem;
        private readonly IModelToObjParser modelToObjParser;

        public ModelParser(IFileSystem fileSystem, IModelToObjParser modelToObjParser)
        {
            this.fileSystem = fileSystem;
            this.modelToObjParser = modelToObjParser;
        }

        public void WriteToOutput(string input, string output)
        {
            byte[] modelFile = fileSystem.ReadAllBytes(input);
            byte[] objFile = modelToObjParser.Parse(modelFile);
            fileSystem.CreateDirectoryIfNotExists(Path.GetDirectoryName(output));
            fileSystem.WriteAllBytes(output, objFile);
        }
    }
}
