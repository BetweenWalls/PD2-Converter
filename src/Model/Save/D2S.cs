using D2SLib.IO;
using System;
using System.Linq;
//using System.Text.Json.Serialization;

namespace D2SLib.Model.Save
{
    public class D2S
    {
        //0x0000
        public Header Header { get; set; }
        //0x0010
        public UInt32 ActiveWeapon { get; set; }
        //0x0014 sizeof(16)
        public string Name { get; set; }
        //0x0024
        public Status Status { get; set; }
        //0x0025
        //[JsonIgnore]
        public byte Progression { get; set; }
        //0x0026 [unk = 0x0, 0x0]
        //[JsonIgnore]
        public byte[]? Unk0x0026 { get; set; }
        //0x0028
        public byte ClassId { get; set; }
        //0x0029 [unk = 0x10, 0x1E]
        //[JsonIgnore]
        public byte[]? Unk0x0029 { get; set; }
        //0x002b
        public byte Level { get; set; }
        //0x002c
        public UInt32 Created { get; set; }
        //0x0030
        public UInt32 LastPlayed { get; set; }
        //0x0034 [unk = 0xff, 0xff, 0xff, 0xff]
        //[JsonIgnore]
        public byte[]? Unk0x0034 { get; set; }
        //0x0038
        public Skill[] AssignedSkills { get; set; }
        //0x0078
        public Skill LeftSkill { get; set; }
        //0x007c
        public Skill RightSkill { get; set; }
        //0x0080
        public Skill LeftSwapSkill { get; set; }
        //0x0084
        public Skill RightSwapSkill { get; set; }
        //0x0088 [char menu appearance]
        public Appearances Appearances { get; set; }
        //0x00a8
        public Locations Location { get; set; }
        //0x00ab
        public UInt32 MapId { get; set; }
        //0x00af [unk = 0x0, 0x0]
        //[JsonIgnore]
        public byte[]? Unk0x00af { get; set; }
        //0x00b1
        public Mercenary Mercenary { get; set; }
        //0x00bf [unk = 0x0] (server related data)
        //[JsonIgnore]
        public byte[]? RealmData { get; set; }
        //0x014b
        public QuestsSection Quests { get; set; }
        //0x0279
        public WaypointsSection Waypoints { get; set; }
        //0x02c9
        public NPCDialogSection NPCDialog { get; set; }
        //0x2fc
        public Attributes Attributes { get; set; }
        

        public ClassSkills ClassSkills { get; set; }

        public ItemList PlayerItemList { get; set; }
        public CorpseList PlayerCorpses { get; set; }
        public MercenaryItemList MercenaryItemList { get; set; }
        public Golem Golem { get; set; }

        public static D2S Read(byte[] bytes)
        {
            using (BitReader reader = new BitReader(bytes))
            {
                bool writeConsole = Globals.writeConsole_D2SRead;
                D2S d2s = new D2S();
                d2s.Header = Header.Read(reader.ReadBytes(16));
                if (writeConsole) Console.WriteLine("Header...");
                if (writeConsole) Console.WriteLine("Header-Magic: " + d2s.Header.Magic);
                if (writeConsole) Console.WriteLine("Header-Version: " + d2s.Header.Version);
                if (writeConsole) Console.WriteLine("Header-Filesize: " + d2s.Header.Filesize);
                if (writeConsole) Console.WriteLine("Header-Checksum: " + d2s.Header.Checksum);
                d2s.ActiveWeapon = reader.ReadUInt32();
                if (writeConsole) Console.WriteLine("ActiveWeapon: " + d2s.ActiveWeapon);
                d2s.Name = reader.ReadString(16);
                if (writeConsole) Console.WriteLine("Name: " + d2s.Name);
                d2s.Status = Status.Read(reader.ReadByte());
                if (writeConsole) Console.WriteLine("Status...");
                if (writeConsole) Console.WriteLine("Status-Ladder: " + d2s.Status.IsLadder);
                if (writeConsole) Console.WriteLine("Status-Expansion: " + d2s.Status.IsExpansion);
                if (writeConsole) Console.WriteLine("Status-Dead: " + d2s.Status.IsDead);
                if (writeConsole) Console.WriteLine("Status-Hardcore: " + d2s.Status.IsHardcore);
                d2s.Progression = reader.ReadByte();
                if (writeConsole) Console.WriteLine("Progression: " + d2s.Progression);
                d2s.Unk0x0026 = reader.ReadBytes(2);
                if (writeConsole) Console.WriteLine("Unk0x0026: " + d2s.Unk0x0026);
                d2s.ClassId = reader.ReadByte();
                if (writeConsole) Console.WriteLine("ClassId: " + d2s.ClassId);
                d2s.Unk0x0029 = reader.ReadBytes(2);
                if (writeConsole) Console.WriteLine("Unk0x0029: " + d2s.Unk0x0029);
                d2s.Level = reader.ReadByte();
                if (writeConsole) Console.WriteLine("Level: " + d2s.Level);
                d2s.Created = reader.ReadUInt32();
                if (writeConsole) Console.WriteLine("Created: " + d2s.Created);
                d2s.LastPlayed = reader.ReadUInt32();
                if (writeConsole) Console.WriteLine("LastPlayed: " + d2s.LastPlayed);
                d2s.Unk0x0034 = reader.ReadBytes(4);
                if (writeConsole) Console.WriteLine("Unk0x0034: " + d2s.Unk0x0034);
                d2s.AssignedSkills = Enumerable.Range(0, 16).Select(e => Skill.Read(reader.ReadBytes(4))).ToArray();
                if (writeConsole) Console.WriteLine("AssignedSkills: " + d2s.AssignedSkills);
                d2s.LeftSkill = Skill.Read(reader.ReadBytes(4));
                if (writeConsole) Console.WriteLine("LeftSkill: " + d2s.LeftSkill);
                d2s.RightSkill = Skill.Read(reader.ReadBytes(4));
                if (writeConsole) Console.WriteLine("RightSkill: " + d2s.RightSkill);
                d2s.LeftSwapSkill = Skill.Read(reader.ReadBytes(4));
                if (writeConsole) Console.WriteLine("LeftSwapSkill: " + d2s.LeftSwapSkill);
                d2s.RightSwapSkill = Skill.Read(reader.ReadBytes(4));
                if (writeConsole) Console.WriteLine("RightSwapSkill: " + d2s.RightSwapSkill);
                d2s.Appearances = Appearances.Read(reader.ReadBytes(32));
                if (writeConsole) Console.WriteLine("Appearances: " + d2s.Appearances);
                d2s.Location = Locations.Read(reader.ReadBytes(3));
                if (writeConsole) Console.WriteLine("Location: " + d2s.Location);
                d2s.MapId = reader.ReadUInt32();
                if (writeConsole) Console.WriteLine("MapId: " + d2s.MapId);
                d2s.Unk0x00af = reader.ReadBytes(2);
                if (writeConsole) Console.WriteLine("Unk0x00af: " + d2s.Unk0x00af);
                d2s.Mercenary = Mercenary.Read(reader.ReadBytes(14));
                if (writeConsole) Console.WriteLine("Mercenary: " + d2s.Mercenary);
                d2s.RealmData = reader.ReadBytes(140);
                if (writeConsole) Console.WriteLine("RealmData: " + d2s.RealmData);
                d2s.Quests = QuestsSection.Read(reader.ReadBytes(302));
                if (writeConsole) Console.WriteLine("Quests: " + d2s.Quests);
                d2s.Waypoints = WaypointsSection.Read(reader.ReadBytes(80));
                if (writeConsole) Console.WriteLine("Waypoints: " + d2s.Waypoints);
                d2s.NPCDialog = NPCDialogSection.Read(reader.ReadBytes(52));
                if (writeConsole) Console.WriteLine("NPCDialog: " + d2s.NPCDialog);
                d2s.Attributes = Attributes.Read(reader);
                if (writeConsole) Console.WriteLine("Attributes: " + d2s.Attributes);
                d2s.ClassSkills = ClassSkills.Read(reader.ReadBytes(32), d2s.ClassId);
                if (writeConsole) Console.WriteLine("ClassSkills: " + d2s.ClassSkills);
                //if (writeConsole) Console.WriteLine("PlayerItemList...");
                d2s.PlayerItemList = ItemList.Read(reader, d2s.Header.Version);
                /*
                if (writeConsole) Console.WriteLine("PlayerItemList-Header: " + d2s.PlayerItemList.Header);
                if (writeConsole) Console.WriteLine("PlayerItemList-Count: " + d2s.PlayerItemList.Count);
                if (writeConsole) Console.WriteLine("Items...");
                for (int i = 0; i < d2s.PlayerItemList.Count; i++)
                {
                    if (writeConsole) Console.WriteLine("Item-" + i + " Code: " + d2s.PlayerItemList.Items[i].Code);
                    if (d2s.PlayerItemList.Items[i].Quantity > 0) { if (writeConsole) Console.WriteLine("Item-" + i + " Quantity: " + d2s.PlayerItemList.Items[i].Quantity); }
                }
                */
                d2s.PlayerCorpses = CorpseList.Read(reader, d2s.Header.Version);
                if (writeConsole) Console.WriteLine("PlayerCorpses: " + d2s.PlayerCorpses);
                if (d2s.Status.IsExpansion)
                {
                    bool temp = Globals.pd2_char_formatting;  // store value
                    Globals.pd2_char_formatting = false;  // temporarily use vanilla character formatting (merc/golem formatting isn't different for PD2)
                    d2s.MercenaryItemList = MercenaryItemList.Read(reader, d2s.Mercenary, d2s.Header.Version);
                    if (writeConsole) Console.WriteLine("MercenaryItemList: " + d2s.MercenaryItemList);
                    d2s.Golem = Golem.Read(reader, d2s.Header.Version);
                    if (writeConsole) Console.WriteLine("Golem: " + d2s.Golem);
                    Globals.pd2_char_formatting = temp;  // reset value
                }
                //Debug.Assert(reader.Position == (bytes.Length * 8));
                return d2s;
            }
        }

        public static byte[] Write(D2S d2s)
        {
            using (BitWriter writer = new BitWriter()) {
                writer.WriteBytes(Header.Write(d2s.Header));
                writer.WriteUInt32(d2s.ActiveWeapon);
                writer.WriteString(d2s.Name, 16);
                writer.WriteBytes(Status.Write(d2s.Status));
                writer.WriteByte(d2s.Progression);
                //Unk0x0026
                writer.WriteBytes(d2s.Unk0x0026 ?? new byte[2]);
                writer.WriteByte(d2s.ClassId);
                //Unk0x0029
                writer.WriteBytes(d2s.Unk0x0029 ?? new byte[] { 0x10, 0x1e });
                writer.WriteByte(d2s.Level);
                writer.WriteUInt32(d2s.Created);
                writer.WriteUInt32(d2s.LastPlayed);
                //Unk0x0034
                writer.WriteBytes(d2s.Unk0x0034 ?? new byte[] { 0xff, 0xff, 0xff, 0xff });
                for(int i = 0; i < 16; i ++)
                {
                    writer.WriteBytes(Skill.Write(d2s.AssignedSkills[i]));
                }
                writer.WriteBytes(Skill.Write(d2s.LeftSkill));
                writer.WriteBytes(Skill.Write(d2s.RightSkill));
                writer.WriteBytes(Skill.Write(d2s.LeftSwapSkill));
                writer.WriteBytes(Skill.Write(d2s.RightSwapSkill));
                writer.WriteBytes(Appearances.Write(d2s.Appearances));
                writer.WriteBytes(Locations.Write(d2s.Location));
                writer.WriteUInt32(d2s.MapId);
                //0x00af [unk = 0x0, 0x0]
                writer.WriteBytes(d2s.Unk0x00af ?? new byte[2]);
                writer.WriteBytes(Mercenary.Write(d2s.Mercenary));
                //0x00bf [unk = 0x0] (server related data)
                writer.WriteBytes(d2s.RealmData ?? new byte[140]);
                writer.WriteBytes(QuestsSection.Write(d2s.Quests));
                writer.WriteBytes(WaypointsSection.Write(d2s.Waypoints));
                writer.WriteBytes(NPCDialogSection.Write(d2s.NPCDialog));
                writer.WriteBytes(Attributes.Write(d2s.Attributes));
                writer.WriteBytes(ClassSkills.Write(d2s.ClassSkills));
                writer.WriteBytes(ItemList.Write(d2s.PlayerItemList, d2s.Header.Version));
                writer.WriteBytes(CorpseList.Write(d2s.PlayerCorpses, d2s.Header.Version));
                if (d2s.Status.IsExpansion)
                {
                    bool temp = Globals.pd2_char_formatting;  // store value
                    Globals.pd2_char_formatting = false;  // temporarily use vanilla character formatting (merc/golem formatting isn't different for PD2)
                    writer.WriteBytes(MercenaryItemList.Write(d2s.MercenaryItemList, d2s.Mercenary, d2s.Header.Version));
                    writer.WriteBytes(Golem.Write(d2s.Golem, d2s.Header.Version));
                    Globals.pd2_char_formatting = temp;  // reset value
                }
                byte[] bytes =  writer.ToArray();
                Header.Fix(bytes);
                return bytes;
            }
        }
    }
}
