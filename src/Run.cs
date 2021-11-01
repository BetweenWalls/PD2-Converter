using D2SLib.Model.Save;
using D2SLib.Model.TXT;
using System;
using System.Diagnostics;
using System.IO;

namespace D2SLib
{
    public static class Globals
    {
        //public static readonly string PROJECT_DIRECTORY = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;    // debug executable
        public static string PROJECT_DIRECTORY;
        public static string TEXT_DIR;
        public static string INPUT_DIR;
        public static string OUTPUT_DIR;
        public static readonly string CFE = ".d2s";    // character file extension
        public static readonly string MOST_RECENT_SEASON = "season 3";
        public static TXT txt_vanilla = new TXT();
        public static TXT txt_pd2_s1 = new TXT();
        public static TXT txt_pd2_s2 = new TXT();
        public static TXT txt_pd2_s3 = new TXT();
        //public static TXT txt_pd2_s4 = new TXT();
        public static bool writeConsole_D2SRead = false;            // temporary, for debugging
        public static bool writeConsole_ItemsRead = false;          // temporary, for debugging
        public static bool writeConsole_ItemsReadComplete = false;  // temporary, for debugging
        public static bool writeConsole_Stash = false;              // temporary, for debugging
        public static byte[] space = new byte[000];
        public static string convert_from = "vanilla";
        public static string convert_to = "pd2";
        public static bool pd2_char_formatting = false;
        public static bool ladder_files_converted = false;
        public static bool files_converted = false;
    }
    class Run
    {
        static void Main(string[] args)
        {
            try
            {
                // Get the directory of the executing EXE
                var mainModule = Process.GetCurrentProcess().MainModule;
                var executingDirectory = Path.GetDirectoryName(mainModule.FileName);

                // Set the working directory in case somewhere we're not specifying explicit paths
                Directory.SetCurrentDirectory(executingDirectory);

                // Set directories
                Globals.PROJECT_DIRECTORY = executingDirectory;
                Globals.TEXT_DIR = Globals.PROJECT_DIRECTORY + @"\TEXT\";
                Globals.INPUT_DIR = Globals.PROJECT_DIRECTORY + @"\input\";
                Globals.OUTPUT_DIR = Globals.PROJECT_DIRECTORY + @"\output\";

                // Check for pre-reqs
                if (!Directory.Exists(Globals.TEXT_DIR))
                {
                    Console.WriteLine("Failed to find required TEXT directory: {0}", Globals.TEXT_DIR);
                    Console.WriteLine("Press enter to close.");
                    Console.ReadLine();
                    return;
                }

                // Create directories
                Directory.CreateDirectory(Globals.INPUT_DIR);
                Directory.CreateDirectory(Globals.OUTPUT_DIR);

                Globals.txt_vanilla.ItemStatCostTXT = ItemStatCostTXT.Read(Globals.TEXT_DIR + @"vanilla\ItemStatCost.txt");
                Globals.txt_vanilla.ItemsTXT.ArmorTXT = ArmorTXT.Read(Globals.TEXT_DIR + @"vanilla\Armor.txt");
                Globals.txt_vanilla.ItemsTXT.WeaponsTXT = WeaponsTXT.Read(Globals.TEXT_DIR + @"vanilla\Weapons.txt");
                Globals.txt_vanilla.ItemsTXT.MiscTXT = MiscTXT.Read(Globals.TEXT_DIR + @"vanilla\Misc.txt");
                Globals.txt_pd2_s1.ItemStatCostTXT = ItemStatCostTXT.Read(Globals.TEXT_DIR + @"pd2_s1\ItemStatCost.txt");
                Globals.txt_pd2_s1.ItemsTXT.ArmorTXT = ArmorTXT.Read(Globals.TEXT_DIR + @"pd2_s1\Armor.txt");
                Globals.txt_pd2_s1.ItemsTXT.WeaponsTXT = WeaponsTXT.Read(Globals.TEXT_DIR + @"pd2_s1\Weapons.txt");
                Globals.txt_pd2_s1.ItemsTXT.MiscTXT = MiscTXT.Read(Globals.TEXT_DIR + @"pd2_s1\Misc.txt");
                Globals.txt_pd2_s2.ItemStatCostTXT = ItemStatCostTXT.Read(Globals.TEXT_DIR + @"pd2_s2\ItemStatCost.txt");
                Globals.txt_pd2_s2.ItemsTXT.ArmorTXT = ArmorTXT.Read(Globals.TEXT_DIR + @"pd2_s2\Armor.txt");
                Globals.txt_pd2_s2.ItemsTXT.WeaponsTXT = WeaponsTXT.Read(Globals.TEXT_DIR + @"pd2_s2\Weapons.txt");
                Globals.txt_pd2_s2.ItemsTXT.MiscTXT = MiscTXT.Read(Globals.TEXT_DIR + @"pd2_s2\Misc.txt");
                Globals.txt_pd2_s3.ItemStatCostTXT = ItemStatCostTXT.Read(Globals.TEXT_DIR + @"pd2_s3\ItemStatCost.txt");
                Globals.txt_pd2_s3.ItemsTXT.ArmorTXT = ArmorTXT.Read(Globals.TEXT_DIR + @"pd2_s3\Armor.txt");
                Globals.txt_pd2_s3.ItemsTXT.WeaponsTXT = WeaponsTXT.Read(Globals.TEXT_DIR + @"pd2_s3\Weapons.txt");
                Globals.txt_pd2_s3.ItemsTXT.MiscTXT = MiscTXT.Read(Globals.TEXT_DIR + @"pd2_s3\Misc.txt");
                //Globals.txt_pd2_s4.ItemStatCostTXT = ItemStatCostTXT.Read(Globals.text_dir + @"pd2_s4\ItemStatCost.txt");
                //Globals.txt_pd2_s4.ItemsTXT.ArmorTXT = ArmorTXT.Read(Globals.text_dir + @"pd2_s4\Armor.txt");
                //Globals.txt_pd2_s4.ItemsTXT.WeaponsTXT = WeaponsTXT.Read(Globals.text_dir + @"pd2_s4\Weapons.txt");
                //Globals.txt_pd2_s4.ItemsTXT.MiscTXT = MiscTXT.Read(Globals.text_dir + @"pd2_s4\Misc.txt");
            }
            catch
            {
                Console.WriteLine("This program converts character/stash files for vanilla/PD2.");
                Console.WriteLine($"Cannot find the TEXT files from the current directory: {Globals.PROJECT_DIRECTORY}");
                Console.WriteLine("Press enter to close.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("This program converts character/stash files for vanilla/PD2. Enter \"\\\" to exit.");
            Console.WriteLine("Which format would you like to convert to?");
            Console.Write("Enter \"vanilla\" (1), \"pd2\" (2), or leave blank for pd2: ");
            string input_convert = Console.ReadLine().ToLowerInvariant();
            if (input_convert == "vanilla" || input_convert == "\"vanilla\"" || input_convert == "1" || input_convert == "(1)" || input_convert == "van") { Globals.convert_to = "vanilla"; }
            if (input_convert == "\\") { return; }
            Console.Write($"Files in {Globals.INPUT_DIR.Substring(Globals.PROJECT_DIRECTORY.Length, Globals.INPUT_DIR.Length - Globals.PROJECT_DIRECTORY.Length - 1)} will be converted to {Globals.convert_to}");
            if (Globals.convert_to != "vanilla") Console.Write($" ({Globals.MOST_RECENT_SEASON})");
            Console.Write(".\r\n\r\n");

            Console.Write("Enter the character's name, or leave blank to convert all files: ");
            string input_name = Console.ReadLine();
            if (input_name == "\\") { return; }

            if (input_name != "")
            {
                if (File.Exists(Globals.INPUT_DIR + input_name + Globals.CFE)) ConvertCharacter(input_name);
                else Console.WriteLine($"{input_name}{Globals.CFE} was not found in {Globals.INPUT_DIR.Substring(Globals.PROJECT_DIRECTORY.Length, Globals.INPUT_DIR.Length - Globals.PROJECT_DIRECTORY.Length - 1)}");
            }
            else
            {
                Console.WriteLine("Reading all characters... ");
                string[] directories = Directory.GetDirectories(Globals.INPUT_DIR);
                string[] files = Directory.GetFiles(Globals.INPUT_DIR);
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
                    directory_name = directories[d].Substring(Globals.INPUT_DIR.Length, directories[d].Length - Globals.INPUT_DIR.Length);
                    Directory.CreateDirectory(Globals.OUTPUT_DIR + directory_name);
                }
                for (int f = 0; f < files.Length; f++)
                {
                    file_name = files[f].Substring(Globals.INPUT_DIR.Length, files[f].Length - Globals.INPUT_DIR.Length);
                    if (files[f].EndsWith(Globals.CFE)) ConvertCharacter(file_name.Substring(0, file_name.Length - Globals.CFE.Length));
                    else if (!(files[f].EndsWith(".d2x") || files[f].EndsWith(".sss") || files[f].EndsWith(".key") || files[f].EndsWith(".map") || files[f].EndsWith(".ma0") || files[f].EndsWith(".ma1") || files[f].EndsWith(".ma2") || files[f].EndsWith(".ma3") || file_name == "place files here")) Console.WriteLine($"{file_name} is not a recognized character/stash file.");  // no message shown for other character files (.key, .map, .ma0, .ma1, .ma2, .ma3)
                }
            }

            if (Globals.ladder_files_converted) Console.WriteLine("Files denoted with \"NL*\" were converted from ladder to non-ladder.");

            if (Globals.convert_to != "vanilla" && input_name == "")
            {
                Console.WriteLine("");
                Console.WriteLine($"Reading all stash files...");
                string[] files = Directory.GetFiles(Globals.INPUT_DIR);
                string file_name = "";
                if (files.Length > 0)
                {
                    bool found_default = false;
                    for (int f = 0; f < files.Length; f++)
                    {
                        file_name = files[f].Substring(Globals.INPUT_DIR.Length, files[f].Length - Globals.INPUT_DIR.Length);
                        if (files[f].EndsWith(".sss")) ConvertStash(file_name.Substring(0, file_name.Length), ".sss");
                        else if (files[f].EndsWith(".d2x")) ConvertStash(file_name.Substring(0, file_name.Length), ".d2x");
                        else if (file_name == "place files here") found_default = true;
                    }
                    Console.WriteLine();
                    if (Globals.files_converted) Console.WriteLine($"Converted files were saved in {Globals.OUTPUT_DIR.Substring(Globals.PROJECT_DIRECTORY.Length, Globals.OUTPUT_DIR.Length - Globals.PROJECT_DIRECTORY.Length - 1)}");
                    else if (found_default && files.Length == 1) Console.WriteLine($"No files found.\r\nPlace files in {Globals.INPUT_DIR}");
                    else Console.WriteLine("No files converted.");
                }
                else Console.WriteLine($"No files found.\r\nPlace files in {Globals.INPUT_DIR}");
            }

            Console.Write("Press enter to close.");
            Console.ReadLine();
        }

        public static void ConvertCharacter(string input_name)
        {
            Console.Write($"Reading {input_name}{Globals.CFE}... ");

            Globals.pd2_char_formatting = false;
            Globals.space = new byte[000];
            D2S character = new D2S();
            Globals.convert_from = "vanilla";
            Core.TXT = Globals.txt_vanilla;
            try
            {
                character = Core.ReadD2S(File.ReadAllBytes(Globals.INPUT_DIR + input_name + Globals.CFE));
            }
            catch
            {
                Globals.pd2_char_formatting = true;
                Globals.convert_from = "pd2_s1";
                Core.TXT = Globals.txt_pd2_s1;
                try
                {
                    character = Core.ReadD2S(File.ReadAllBytes(Globals.INPUT_DIR + input_name + Globals.CFE));
                }
                catch
                {
                    Globals.convert_from = "pd2_s2";
                    Core.TXT = Globals.txt_pd2_s2;
                    try
                    {
                        character = Core.ReadD2S(File.ReadAllBytes(Globals.INPUT_DIR + input_name + Globals.CFE));
                    }
                    catch
                    {
                        Globals.convert_from = "pd2_s3";
                        Core.TXT = Globals.txt_pd2_s3;
                        try
                        {
                            character = Core.ReadD2S(File.ReadAllBytes(Globals.INPUT_DIR + input_name + Globals.CFE));
                        }
                        catch
                        {
                            Globals.convert_from = "?";
                        }
                    }
                }
            }

            if (Globals.convert_from != "?")
            {
                Console.Write($"[{Globals.convert_from}]... ");

                bool was_ladder = false;
                if (character.Status.IsLadder)
                {
                    character.Status.IsLadder = false;  // TODO: test whether this works with actual ladder character files
                    was_ladder = true;
                }
                if (character.Header.Version == 0x61) character.Header.Version = 0x60; // TODO: test whether this works with actual D2R character files (0x61 = D2R, 0x60 = legacy)

                Globals.pd2_char_formatting = true;
                if (Globals.convert_to == "vanilla") Core.TXT = Globals.txt_vanilla;
                else Core.TXT = Globals.txt_pd2_s3;

                try
                {
                    File.WriteAllBytes(Globals.OUTPUT_DIR + input_name + Globals.CFE, Core.WriteD2S(character));    // TODO: This can't convert non-vanilla items/affixes back to vanilla - maybe it should try removing them?
                    Console.Write("saved");
                    Globals.files_converted = true;
                    if (was_ladder)
                    {
                        Console.Write(" NL*");
                        Globals.ladder_files_converted = true;
                    }
                    Console.Write("\r\n");
                }
                catch
                {
                    Console.Write("couldn't save\r\n");
                }
            }
            else
            {
                Console.Write("ignored (unknown file format)\r\n");
            }

        }

        public static void ConvertStash(string stash_name, string type)
        {
            Console.Write($"Reading {stash_name}... ");

            Globals.pd2_char_formatting = false;
            Globals.convert_from = "vanilla";
            Core.TXT = Globals.txt_vanilla;
            UInt16 stash_version = 0x3230;
            D2I stash = new D2I();
            try
            {
                stash = Core.ReadD2I(Globals.INPUT_DIR + stash_name, stash_version, type);
            }
            catch
            {
                Globals.convert_from = "pd2_s1";
                Core.TXT = Globals.txt_pd2_s1;
                try
                {
                    stash = Core.ReadD2I(Globals.INPUT_DIR + stash_name, stash_version, type);
                }
                catch
                {
                    Globals.convert_from = "pd2_s2";
                    Core.TXT = Globals.txt_pd2_s2;
                    try
                    {
                        stash = Core.ReadD2I(Globals.INPUT_DIR + stash_name, stash_version, type);
                    }
                    catch
                    {
                        Globals.convert_from = "pd2_s3";
                        Core.TXT = Globals.txt_pd2_s3;
                        try
                        {
                            stash = Core.ReadD2I(Globals.INPUT_DIR + stash_name, stash_version, type);
                        }
                        catch
                        {
                            Globals.convert_from = "?";
                        }
                    }
                }
            }

            if (Globals.convert_from != "?")
            {
                Console.Write($"[{Globals.convert_from}]... ");
                Core.TXT = Globals.txt_pd2_s3;
                try
                {
                    File.WriteAllBytes(Globals.OUTPUT_DIR + stash_name, Core.WriteD2I(stash, stash_version, type));
                    Console.Write("saved\r\n");
                    Globals.files_converted = true;
                }
                catch
                {
                    Console.Write("couldn't save\r\n");
                }
            }
            else
            {
                Console.Write("ignored (unknown file format)\r\n");
            }
        }

    }
}
