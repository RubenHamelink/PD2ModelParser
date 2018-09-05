// Decompiled with JetBrains decompiler
// Type: PD2Bundle.Hash64
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System.Runtime.InteropServices;
using System.Text;

namespace PD2Bundle
{
  internal class Hash64
  {
    [DllImport("hash64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong Hash(byte[] k, ulong length, ulong level);

    public static ulong HashString(string input, ulong level = 0)
    {
      return Hash64.Hash(Encoding.UTF8.GetBytes(input), (ulong) Encoding.UTF8.GetByteCount(input), level);
    }
  }
}
