using D2SLib;
using D2SLib.Model.Save;
using D2SLib.Model.TXT;
using System;
using System.IO;

namespace D2SLib
{
    public static class Globals
    {
        public static readonly string PROJECT_DIRECTORY = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        public static readonly string text_dir = PROJECT_DIRECTORY + @"\Resources\TEXT\";
        public static readonly string input_dir = PROJECT_DIRECTORY + @"\Resources\D2S\input\";
        public static readonly string output_dir = PROJECT_DIRECTORY + @"\Resources\D2S\output\";
        public static readonly string CFE = ".d2s";    // character file extension
        public static TXT txt_vanilla = new TXT();
        public static TXT txt_pd2_s1 = new TXT();
        public static TXT txt_pd2_s2 = new TXT();
        public static TXT txt_pd2_s3 = new TXT();
        public static bool writeConsole_ConvertCharacter = false;
        public static bool writeConsole_D2SRead = false;
        public static bool writeConsole_ItemsRead = false;
        public static bool writeConsole_ItemsReadComplete = false;
        public static bool recognized = true;
        public static bool vanilla = true;
        public static string version = "vanilla";
        public static byte[] space = new byte[000];
        public static string convert_to = "pd2";
    }
    class Run
    {
        static void Main(string[] args)
        {
            Globals.txt_vanilla.ItemStatCostTXT = ItemStatCostTXT.Read(Globals.text_dir + @"vanilla\ItemStatCost.txt");
            Globals.txt_vanilla.ItemsTXT.ArmorTXT = ArmorTXT.Read(Globals.text_dir + @"vanilla\Armor.txt");
            Globals.txt_vanilla.ItemsTXT.WeaponsTXT = WeaponsTXT.Read(Globals.text_dir + @"vanilla\Weapons.txt");
            Globals.txt_vanilla.ItemsTXT.MiscTXT = MiscTXT.Read(Globals.text_dir + @"vanilla\Misc.txt");
            Globals.txt_pd2_s1.ItemStatCostTXT = ItemStatCostTXT.Read(Globals.text_dir + @"pd2_s1\ItemStatCost.txt");
            Globals.txt_pd2_s1.ItemsTXT.ArmorTXT = ArmorTXT.Read(Globals.text_dir + @"pd2_s1\Armor.txt");
            Globals.txt_pd2_s1.ItemsTXT.WeaponsTXT = WeaponsTXT.Read(Globals.text_dir + @"pd2_s1\Weapons.txt");
            Globals.txt_pd2_s1.ItemsTXT.MiscTXT = MiscTXT.Read(Globals.text_dir + @"pd2_s1\Misc.txt");
            Globals.txt_pd2_s2.ItemStatCostTXT = ItemStatCostTXT.Read(Globals.text_dir + @"pd2_s2\ItemStatCost.txt");
            Globals.txt_pd2_s2.ItemsTXT.ArmorTXT = ArmorTXT.Read(Globals.text_dir + @"pd2_s2\Armor.txt");
            Globals.txt_pd2_s2.ItemsTXT.WeaponsTXT = WeaponsTXT.Read(Globals.text_dir + @"pd2_s2\Weapons.txt");
            Globals.txt_pd2_s2.ItemsTXT.MiscTXT = MiscTXT.Read(Globals.text_dir + @"pd2_s2\Misc.txt");
            Globals.txt_pd2_s3.ItemStatCostTXT = ItemStatCostTXT.Read(Globals.text_dir + @"pd2_s3\ItemStatCost.txt");
            Globals.txt_pd2_s3.ItemsTXT.ArmorTXT = ArmorTXT.Read(Globals.text_dir + @"pd2_s3\Armor.txt");
            Globals.txt_pd2_s3.ItemsTXT.WeaponsTXT = WeaponsTXT.Read(Globals.text_dir + @"pd2_s3\Weapons.txt");
            Globals.txt_pd2_s3.ItemsTXT.MiscTXT = MiscTXT.Read(Globals.text_dir + @"pd2_s3\Misc.txt");

            Console.WriteLine("This program converts character files for vanilla/PD2. Enter \"\\\" to exit.");
            Console.WriteLine("Which format would you like to convert to?");
            Console.Write("Enter \"vanilla\" (1), \"pd2\" (2), or leave blank for pd2: ");
            string input_convert = Console.ReadLine().ToLowerInvariant();
            if (input_convert == "vanilla" || input_convert == "\"vanilla\"" || input_convert == "1" || input_convert == "van") { Globals.convert_to = "vanilla"; }
            if (input_convert == "\\") { return; }
            Console.WriteLine($"Characters will be converted to {Globals.convert_to}.");

            Console.Write("Enter the character's name, or leave blank to convert all characters: ");
            string input_name = Console.ReadLine();
            if (input_name == "\\") { return; }

            if (input_name != "")
            {
                if (File.Exists(Globals.input_dir + input_name + Globals.CFE))
                {
                    ConvertCharacter(input_name);
                }
                else
                {
                    Console.WriteLine($"{input_name}{Globals.CFE} was not found in {Globals.input_dir.Substring(0,Globals.input_dir.Length-1)}.");
                }
            }
            else
            {
                Console.WriteLine("Reading all characters... ");
                string[] directories = Directory.GetDirectories(Globals.input_dir);
                string[] files = Directory.GetFiles(Globals.input_dir);
                string file_name = "";
                string directory_name = "";
                for (int d = 0; d < directories.Length; d++)
                {
                    string[] subfiles = Directory.GetFiles(directories[d]);
                    // add subfiles to files
                    string[] temp = new string[files.Length + subfiles.Length];
                    files.CopyTo(temp, 0);
                    subfiles.CopyTo(temp, files.Length);
                    files = temp;
                    // create directory in output folder
                    directory_name = directories[d].Substring(Globals.input_dir.Length, directories[d].Length - Globals.input_dir.Length);
                    Directory.CreateDirectory(Globals.output_dir + directory_name);
                }
                for (int f = 0; f < files.Length; f++)
                {
                    file_name = files[f].Substring(Globals.input_dir.Length, files[f].Length - Globals.input_dir.Length);
                    if (files[f].EndsWith(Globals.CFE))
                    {
                        if (!file_name.Contains(" "))
                        {
                            ConvertCharacter(file_name.Substring(0, file_name.Length - Globals.CFE.Length));
                        }
                        else
                        {
                            Console.WriteLine($"Ignored: {file_name} (invalid file name)");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{file_name} is not a character file.");
                    }
                }
            }

            // TODO: implement conversion for .d2i/.sss files (shared stash)
            //ConvertStash();

            Console.Write("Press enter to close.");
            Console.ReadLine();

        }

        public static void ConvertCharacter(string input_name)
        {
            bool writeConsole = Globals.writeConsole_ConvertCharacter;

            Console.Write($"Reading {input_name}{Globals.CFE}...");

            Globals.recognized = true;
            Globals.vanilla = true;
            Globals.version = "vanilla";
            Globals.space = new byte[000];
            D2S character = new D2S();
            Core.TXT = Globals.txt_vanilla;
            try
            {
                character = Core.ReadD2S(File.ReadAllBytes(Globals.input_dir + input_name + Globals.CFE));
            }
            catch
            {
                if (writeConsole) { Console.WriteLine("EXCEPTION, TRYING PD2 FORMATTING (S3)..."); }
                Globals.vanilla = false;
                Globals.version = "pd2_s3";
                Core.TXT = Globals.txt_pd2_s3;
                try
                {
                    character = Core.ReadD2S(File.ReadAllBytes(Globals.input_dir + input_name + Globals.CFE));
                }
                catch
                {
                    if (writeConsole) { Console.WriteLine("EXCEPTION, TRYING PD2 FORMATTING (S2)..."); }
                    Globals.version = "pd2_s2";
                    Core.TXT = Globals.txt_pd2_s2;
                    try
                    {
                        character = Core.ReadD2S(File.ReadAllBytes(Globals.input_dir + input_name + Globals.CFE));
                    }
                    catch
                    {
                        if (writeConsole) { Console.WriteLine("EXCEPTION, TRYING PD2 FORMATTING (S1)..."); }
                        Globals.version = "pd2_s1";
                        Core.TXT = Globals.txt_pd2_s1;
                        try
                        {
                            character = Core.ReadD2S(File.ReadAllBytes(Globals.input_dir + input_name + Globals.CFE));
                        }
                        catch
                        {
                            if (writeConsole) { Console.WriteLine("EXCEPTION, UNABLE TO DETERMINE FORMAT"); }
                            Globals.recognized = false;
                            Globals.version = "?";
                            Console.Write($" ignored (unknown file format)\r\n");
                        }
                    }
                }
            }

            if (Globals.recognized)
            {
                Globals.vanilla = false;
                Core.TXT = Globals.txt_pd2_s3;
                File.WriteAllBytes(Globals.output_dir + input_name + Globals.CFE, Core.WriteD2S(character));
                Console.Write($" [{Globals.version}]...");
                Console.Write(" saved\r\n");
            }

        }

        public static void ConvertStash()
        {
            // TODO...

            Console.Write("Reading stash...");

            //string stash_name = "_LOD_SharedStashSave.sss"; // D2 PlugY (is this even supported by D2SLib?)
            string stash_name = "SharedStash_SoftCore.d2i"; // D2 Resurrected
            string stash_input_dir = Globals.PROJECT_DIRECTORY + @"\Resources\D2I\input\" + stash_name;
            string stash_output_dir = Globals.PROJECT_DIRECTORY + @"\Resources\D2I\output\" + stash_name;
            uint stash_ver = 60;
            Globals.recognized = true;
            //Globals.vanilla = true;
            D2I stash = new D2I();
            Core.TXT = Globals.txt_vanilla;
            try
            {
                stash = Core.ReadD2I(File.ReadAllBytes(stash_input_dir), stash_ver);
            }
            catch
            {
                //Globals.vanilla = false;
                //Globals.version = "pd2_s3";
                Core.TXT = Globals.txt_pd2_s3;
                try
                {
                    stash = Core.ReadD2I(File.ReadAllBytes(stash_input_dir), stash_ver);
                }
                catch
                {
                    //Globals.version = "pd2_s2";
                    Core.TXT = Globals.txt_pd2_s2;
                    try
                    {
                        stash = Core.ReadD2I(File.ReadAllBytes(stash_input_dir), stash_ver);
                    }
                    catch
                    {
                        //Globals.version = "pd2_s1";
                        Core.TXT = Globals.txt_pd2_s1;
                        try
                        {
                            stash = Core.ReadD2I(File.ReadAllBytes(stash_input_dir), stash_ver);
                        }
                        catch
                        {
                            Globals.recognized = false;
                            //Globals.version = "?";
                            Console.Write($" ignored (unknown file format)\r\n");
                        }
                    }
                }
            }

            if (Globals.recognized)
            {
                //Globals.vanilla = false;
                if (Globals.convert_to == "vanilla")
                {
                    Core.TXT = Globals.txt_vanilla;
                }
                else
                {
                    Core.TXT = Globals.txt_pd2_s3;
                }
                File.WriteAllBytes(stash_output_dir, Core.WriteD2I(stash, stash_ver));
                //Console.Write($" [{Globals.version}]...");
                Console.Write(" saved\r\n");
            }

        }
    }
}
