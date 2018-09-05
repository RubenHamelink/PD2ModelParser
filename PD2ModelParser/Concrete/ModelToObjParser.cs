using System;
using System.Collections.Generic;
using System.IO;
using PD2ModelParser.Abstract;
using PD2ModelParser.Sections;

namespace PD2ModelParser.Concrete
{
    public class ModelToObjParser : IModelToObjParser
    {
        private ISectionHeaderParser headerParser;
        private ISectionParser sectionParser;
        private Dictionary<uint, Func<Section>> sectionTypes;
        private IObjParser objParser;

        public ModelToObjParser(ISectionHeaderParser headerParser, ISectionParser sectionParser, IObjParser objParser)
        {
            this.headerParser = headerParser;
            this.sectionParser = sectionParser;
            this.objParser = objParser;

            sectionTypes = new Dictionary<uint, Func<Section>>
            {
                {1572868536, () => new Animation() },
                {1982055525, () => new Author() },
                {246692983, () => new Bones() },
                {2058384083, () => new Geometry() },
                {1012162716, () => new Material() },
                {690449181, () => new MaterialGroup() },
                {1646341512, () => new Model() },
                {268226816, () => new Object3D() },
                {3819155914, () => new PassthroughGP() },
                {1686773868, () => new QuatLinearRotationController() },
                {1707874341, () => new SkinBones() },
                {1280342547, () => new Topology() },
            };
        }

        public byte[] Parse(byte[] model)
        {
            byte[] obj = null;
            using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(model)))
            {
                List<SectionHeader> sectionHeaders = headerParser.Parse(binaryReader);
                Dictionary<uint, Section> parsedSections = sectionParser.Parse(binaryReader, sectionHeaders, sectionTypes);
                obj = objParser.Parse(sectionHeaders, parsedSections);
            }

            return obj;
        }
    }
}
