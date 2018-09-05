// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Program
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System;
using System.Diagnostics;
using System.IO;
using Ninject;
using PD2ModelParser.Abstract;
using PD2ModelParser.Concrete;

namespace PD2ModelParser
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
            {
                Console.Error.WriteLine("Input file is required");
                return;
            }

            IKernel kernel = new StandardKernel();
            kernel.Bind<IFileSystem>().To<FileSystem>();
            kernel.Bind<IModelParser>().To<ModelParser>();
            kernel.Bind<IModelToObjParser>().To<ModelToObjParser>();
            kernel.Bind<ISectionHeaderParser>().To<SectionHeaderParser>();
            kernel.Bind<ISectionParser>().To<SectionParser>();
            kernel.Bind<IObjParser>().To<ObjParser>();

            string input = args[0];
            string output = input.Replace(".model", ".obj");
            if (args.Length > 1 && !string.IsNullOrEmpty(args[1]))
                output = args[1];

            IModelParser modelParser = kernel.Get<IModelParser>();
            FileManager fileManager = new FileManager();
            Stopwatch stopwatch = Stopwatch.StartNew();
            modelParser.WriteToOutput(input, output);
            long newMs = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();
            fileManager.Open(input, output:output);
            stopwatch.Stop();
            Console.WriteLine($"Converting model the old way took {stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine($"Converting model the new way took {newMs}ms");
            Console.Read();
        }
    }
}