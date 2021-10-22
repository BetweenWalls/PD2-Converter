using D2SLib.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace D2SLib.Model.Save
{
    public class StashPage
    {
        //0x00
        public UInt16? Header { get; set; }  // "ST" (0x5453 when read as a 16 bit unsigned integer)
        //0x02
        public UInt32 Flags { get; set; }   // isShared, isIndex, isMainIndex, isReserved (4 bytes since PlugY 11.02, else 0 bytes... older versions not supported)
        //0x06
        public string Name { get; set; }    // Page name (up to 15 characters + null in PlugY 11.02, or up to 20 characters + null in PlugY 14 or later)
        //Variable (0-20 + 1)
        public ItemList PageItems { get; set; }

        public static StashPage Read(StashPage page, BitReader reader, UInt32 version)
        {
            Boolean writeConsole = D2SLib.Globals.writeConsole_Stash;
            version = 0x60;
            //version = 0x5453;
            page.Header = reader.ReadUInt16();
            page.Flags = reader.ReadUInt32();

            string page_name = "";
            bool page_name_finished = false;
            while (!page_name_finished)
            {
                string temp = reader.ReadString(1);
                page_name += temp;
                if (String.IsNullOrEmpty(temp)) page_name_finished = true;
            }
            page.Name = page_name;
            if (writeConsole) Console.Write($"{page.Name}");
            //page.PageItems = new ItemList();
            page.PageItems = ItemList.Read(reader, version);

            if (writeConsole) Console.Write($" ...{page.PageItems.Items.Count} items\r\n");
            for (int i = 0; i < page.PageItems.Items.Count; i++)
            {
                //if (writeConsole) Console.WriteLine($"- {page.PageItems.Items[i].Code} ({page.PageItems.Items[i].Quality})");
            }
            return page;
        }

        public static byte[] Write(StashPage page, UInt32 version)
        {
            using (BitWriter writer = new BitWriter())
            {
                writer.WriteUInt16(page.Header ?? 0x5453);
                writer.WriteUInt32(page.Flags);
                writer.WriteString(page.Name + '\0', page.Name.Length + 1);
                // TODO: Simplify further by calling ItemList.Write?
                writer.WriteString("JM", "JM".Length);
                writer.WriteUInt16(page.PageItems.Count);
                for (int i = 0; i < page.PageItems.Count; i++)
                {
                    Item.Write(page.PageItems.Items[i], 0x60, writer);
                }
                
                return writer.ToArray();
            }

        }

    }
}
