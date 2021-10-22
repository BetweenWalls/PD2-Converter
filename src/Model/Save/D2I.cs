using D2SLib.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace D2SLib.Model.Save
{
    public class D2I
    {
        //0x00
        public UInt32 Magic { get; set; }  // "SSS\0" (0x00535353) for .sss files, "CSTM" (0x4D545343) for .d2x files
        //0x04
        public UInt16 Version { get; set; } // "01" (0x3130) [no gold] or "02" (0x3230) [shared gold] for .sss files, "01" (0x3130) for .d2x files
        //0x06
        public UInt32 Gold { get; set; }    // Shared gold (0 bytes if no gold)
        //0x06 or 0x0A
        public UInt32 Pages { get; set; }   // Number of pages in the stash data
        //0x0A or 0x0E
        public StashPage[] PageList { get; set; }

        public static D2I Read(byte[] bytes, UInt32 version, string type)
        {
            bool writeConsole = D2SLib.Globals.writeConsole_Stash;
            using (BitReader reader = new BitReader(bytes))
            {
                D2I d2i = new D2I();
                d2i.Magic = reader.ReadUInt32();
                d2i.Version = reader.ReadUInt16();
                if (d2i.Version == 12848 || type == ".d2x") d2i.Gold = reader.ReadUInt32(); // no gold = 12592, gold = 12848
                d2i.Pages = reader.ReadUInt32();
                if (writeConsole) Console.WriteLine($"\r\nStash Version: {d2i.Version}");
                if (writeConsole) Console.WriteLine("Shared Gold: " + d2i.Gold);
                d2i.PageList = new StashPage[d2i.Pages];
                for (int p = 0; p < d2i.PageList.Length; p++)
                {
                    d2i.PageList[p] = new StashPage();
                    if (writeConsole) Console.Write($"Page {p+1}... ");
                    d2i.PageList[p] = StashPage.Read(d2i.PageList[p], reader, version);
                }
                return d2i;
            }
        }

        public static byte[] Write(D2I d2i, UInt32 version, string type)
        {
            using (BitWriter writer = new BitWriter())
            {
                writer.WriteUInt32(d2i.Magic);  // TODO: check if "SSS\0" or "CSTM" should be specified - d2i.Magic can't be null, can it?
                writer.WriteUInt16(d2i.Version);
                if (d2i.Version != 0x3130 || type == ".d2x") writer.WriteUInt32(d2i.Gold);
                writer.WriteUInt32(d2i.Pages);
                for (int p = 0; p < d2i.Pages; p++)
                {
                    writer.WriteBytes(StashPage.Write(d2i.PageList[p], version));
                }
                return writer.ToArray();
            }
        }
        
    }
}
