// Decompiled with JetBrains decompiler
// Type: PD2ModelParser.Program
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System;
using System.IO;
using System.Windows.Forms;

namespace PD2ModelParser
{
  internal class Program
  {
    [STAThread]
    private static void Main(string[] args)
    {
        if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
        {
            Application.Run((Form)new Form1());
            return;
        }

        string input = args[0];
        string output = input.Replace(".model", ".obj");
        if (args.Length > 1 && !string.IsNullOrEmpty(args[1]))
            output = args[1];
        
        FileManager fileManager = new FileManager();
        fileManager.Open(input, output:output);
    }
  }
}
