using D2SLib.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace D2SLib.Model.Save
{
    public class D2I
    {

        public HeaderD2I Header { get; set; }
        public StashPage[] PageList { get; set; }

        public static D2I Read(byte[] bytes, UInt32 version)
        {
            Boolean writeConsole = D2SLib.Globals.writeConsole_Stash;
            D2I d2i = new D2I();
            using (BitReader reader = new BitReader(bytes))
            {
                // TODO: Merge D2I.cs and HeaderD2I?
                HeaderD2I header = new HeaderD2I();
                header.Magic = reader.ReadUInt32();
                header.Version = reader.ReadUInt16();
                if (header.Version == 12848) header.Gold = reader.ReadUInt32(); // no gold = 12592, gold = 12848
                header.Pages = reader.ReadUInt32();
                if (writeConsole) Console.WriteLine($"Stash Version: {header.Version}");
                //d2i.Header = HeaderD2I.Read(reader.ReadBytes(X)); // bytes used are either 10 or 14
                d2i.Header = header;

                if (writeConsole) Console.WriteLine("Shared Gold: " + d2i.Header.Gold);
                d2i.PageList = new StashPage[d2i.Header.Pages];
                for (int p = 0; p < d2i.PageList.Length; p++)
                {
                    d2i.PageList[p] = new StashPage();
                    if (writeConsole) Console.Write($"Page {p+1}... ");
                    d2i.PageList[p] = StashPage.Read(d2i.PageList[p], reader, version);
                }
                return d2i;
            }
        }

        public static byte[] Write(D2I d2i, UInt32 version)
        {
            using (BitWriter writer = new BitWriter())
            {
                writer.WriteBytes(HeaderD2I.Write(d2i.Header));
                for (int p = 0; p < d2i.Header.Pages; p++)
                {
                    writer.WriteBytes(StashPage.Write(d2i.PageList[p], version));
                }
                return writer.ToArray();
            }
        }

    }
}
