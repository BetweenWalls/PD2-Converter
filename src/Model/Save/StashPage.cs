using D2SLib.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace D2SLib.Model.Save
{
    public class StashPage
    {
        //unused
        public UInt16 ItemCount { get; set; }  // Number of items, only for online pd2 format for .stash and .stash.hc files
        //0x00
        public UInt16? Header { get; set; }  // "ST" (0x5453 when read as a 16 bit unsigned integer)
        //0x02
        public UInt32 Flags { get; set; }   // isShared, isIndex, isMainIndex, isReserved (4 bytes since PlugY 11.02, else 0 bytes... older versions not supported)
        //0x06
        public string Name { get; set; }    // Page name (up to 15 characters + null in PlugY 11.02, or up to 20 characters + null in PlugY 14 or later)
        //Variable
        public ItemList PageItems { get; set; }

        public static StashPage Read(StashPage page, BitReader reader, UInt32 version)
        {
            Boolean writeConsole = D2SLib.Globals.writeConsole_Stash;
            if (!Globals.pd2_stash_formatting)
            {
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
            }
            page.PageItems = ItemList.Read(reader, version);

            return page;
        }

        public static byte[] Write(StashPage page, UInt32 version)
        {
            using (BitWriter writer = new BitWriter())
            {
                if (!Globals.pd2_stash_formatting)
                {
                    writer.WriteUInt16(page.Header ?? 0x5453);
                    writer.WriteUInt32(page.Flags);
                    writer.WriteString(page.Name + '\0', page.Name.Length + 1);
                    writer.WriteString("JM", "JM".Length);
                }
                writer.WriteUInt16(page.PageItems.Count);
                for (int i = 0; i < page.PageItems.Count; i++)
                {
                    Item.Write(page.PageItems.Items[i], version, writer);
                }
                
                return writer.ToArray();
            }

        }

    }
}
