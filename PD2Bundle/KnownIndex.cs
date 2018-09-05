// Decompiled with JetBrains decompiler
// Type: PD2Bundle.KnownIndex
// Assembly: PD2ModelParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE191739-AC38-4AA2-84FC-C99C6D61FA50
// Assembly location: C:\Users\ruben\Downloads\12597_Diesel Model Tool v1.03_1.03\PD2ModelParser.exe

using System;
using System.Collections.Generic;
using System.IO;

namespace PD2Bundle
{
  public class KnownIndex
  {
    private Dictionary<ulong, string> hashes = new Dictionary<ulong, string>();

    public string GetString(ulong hash)
    {
      if (this.hashes.ContainsKey(hash))
        return this.hashes[hash];
      return Convert.ToString(hash);
    }

    private void CheckCollision(Dictionary<ulong, string> item, ulong hash, string value)
    {
      if (!item.ContainsKey(hash) || !(item[hash] != value))
        return;
      Console.WriteLine("Hash collision: {0:x} : {1} == {2}", (object) hash, (object) item[hash], (object) value);
    }

    public void Clear()
    {
      this.hashes.Clear();
    }

    public bool Load()
    {
      try
      {
        using (StreamReader streamReader = new StreamReader((Stream) new FileStream("hashes.txt", FileMode.Open)))
        {
          for (string input = streamReader.ReadLine(); input != null; input = streamReader.ReadLine())
          {
            ulong hash = Hash64.HashString(input, 0UL);
            this.CheckCollision(this.hashes, hash, input);
            this.hashes[hash] = input;
          }
          streamReader.Close();
        }
      }
      catch (Exception ex)
      {
        return false;
      }
      return true;
    }
  }
}
