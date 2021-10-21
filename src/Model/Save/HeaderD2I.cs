using D2SLib.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace D2SLib.Model.Save
{
    public class HeaderD2I
    {
        //0x00
        public UInt32? Magic { get; set; }  // "SSS\0" (0x00535353 when read as a 32 bit unsigned integer)
        //0x04
        public UInt16 Version { get; set; } // "01" (0x3130) or "02" (0x3230)
        //0x06
        public UInt32 Gold { get; set; }    // Shared gold (0 bytes if no gold)
        //0x06 or 0x0A
        public UInt32 Pages { get; set; }   // Number of pages in the stash data

        public static HeaderD2I Read(byte[] bytes)
        {
            Boolean writeConsole = D2SLib.Globals.writeConsole_Stash;
            using (BitReader reader = new BitReader(bytes))
            {
                // TODO: Merge D2I.cs and HeaderD2I?
                HeaderD2I header = new HeaderD2I();
                header.Magic = reader.ReadUInt32();
                header.Version = reader.ReadUInt16();
                if (header.Version != 0x3130) header.Gold = reader.ReadUInt32(); // no gold = 12592, gold = 12848
                header.Pages = reader.ReadUInt32();
                //if (writeConsole) Console.WriteLine($"Stash Version: {header.Version}");
                return header;
            }
        }

        public static byte[] Write(HeaderD2I header)
        {
            using(BitWriter writer = new BitWriter())
            {
                writer.WriteUInt32(header.Magic ?? 0x00535353);
                writer.WriteUInt16(header.Version);
                if (header.Version != 0x3130) writer.WriteUInt32(header.Gold);
                writer.WriteUInt32(header.Pages);
                return writer.ToArray();
            }
        }

    }
}
