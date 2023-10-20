using D2SLib.Model.Save;
using D2SLib.Model.TXT;
using System;
using System.IO;
//using System.Diagnostics;

namespace D2SLib
{
    public static class Globals
    {
        //public static readonly string PROJECT_DIRECTORY = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;    // debug executable
        public static readonly string PROJECT_DIRECTORY = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;   // release executable    ...to update, go to Build -> Build Solution, then Build -> Publish PD2-Converter, then press Publish - settings are Release, net48, portable, and C:\Users\...\Desktop\Docs\github repositories\PD2-Converter\src\main
        public static readonly string TEXT_DIR = PROJECT_DIRECTORY + @"\src\main\TEXT\";
        public static readonly string INPUT_DIR = PROJECT_DIRECTORY + @"\src\main\input\";
        public static readonly string OUTPUT_DIR = PROJECT_DIRECTORY + @"\src\main\output\";
        public static readonly string CFE = ".d2s";    // character file extension
        //public static readonly string MOST_RECENT_SEASON = "?";
        public static TXT txt_vanilla = new TXT();
        public static TXT txt_pd2_s1 = new TXT();
        public static TXT txt_pd2_s2 = new TXT();
        public static TXT txt_pd2_s3 = new TXT();
        public static TXT txt_pd2_s4 = new TXT();
        public static TXT txt_pd2_s5 = new TXT();
        public static TXT txt_pd2_s6 = new TXT();
        public static TXT txt_pd2_s7 = new TXT();
        public static TXT txt_pd2_s8 = new TXT();
        public static bool writeConsole_D2SRead = false;            // temporary, for debugging
        public static bool writeConsole_ItemsRead = false;          // temporary, for debugging
        public static bool writeConsole_ItemsReadComplete = false;  // temporary, for debugging
        public static bool writeConsole_Stash = false;              // temporary, for debugging
        public static byte[] space = new byte[000];
        public static string convert_from = "vanilla";
        public static string convert_to = "pd2";
        public static string force_convert_from = "";
        public static bool pd2_char_formatting = false;
        public static bool pd2_stash_formatting = false;          // flag for .stash and .stash.hc files
        public static bool ladder_files_converted = false;
        public static int files_converted = 0;
        public static int files_ignored = 0;
    }
    class Run
    {
        static void Main(string[] args)
        {
            try
            {
                /*
                // TODO: define relative paths here instead of directly in Globals class?
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
                */

                // TODO: Reimplement variables via a less hardcoded method to reduce the number of lines it takes to accomplish the same thing
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
                Globals.txt_pd2_s4.ItemStatCostTXT = ItemStatCostTXT.Read(Globals.TEXT_DIR + @"pd2_s4\ItemStatCost.txt");
                Globals.txt_pd2_s4.ItemsTXT.ArmorTXT = ArmorTXT.Read(Globals.TEXT_DIR + @"pd2_s4\Armor.txt");
                Globals.txt_pd2_s4.ItemsTXT.WeaponsTXT = WeaponsTXT.Read(Globals.TEXT_DIR + @"pd2_s4\Weapons.txt");
                Globals.txt_pd2_s4.ItemsTXT.MiscTXT = MiscTXT.Read(Globals.TEXT_DIR + @"pd2_s4\Misc.txt");
                Globals.txt_pd2_s5.ItemStatCostTXT = ItemStatCostTXT.Read(Globals.TEXT_DIR + @"pd2_s5\ItemStatCost.txt");
                Globals.txt_pd2_s5.ItemsTXT.ArmorTXT = ArmorTXT.Read(Globals.TEXT_DIR + @"pd2_s5\Armor.txt");
                Globals.txt_pd2_s5.ItemsTXT.WeaponsTXT = WeaponsTXT.Read(Globals.TEXT_DIR + @"pd2_s5\Weapons.txt");
                Globals.txt_pd2_s5.ItemsTXT.MiscTXT = MiscTXT.Read(Globals.TEXT_DIR + @"pd2_s5\Misc.txt");
                Globals.txt_pd2_s6.ItemStatCostTXT = ItemStatCostTXT.Read(Globals.TEXT_DIR + @"pd2_s6\ItemStatCost.txt");
                Globals.txt_pd2_s6.ItemsTXT.ArmorTXT = ArmorTXT.Read(Globals.TEXT_DIR + @"pd2_s6\Armor.txt");
                Globals.txt_pd2_s6.ItemsTXT.WeaponsTXT = WeaponsTXT.Read(Globals.TEXT_DIR + @"pd2_s6\Weapons.txt");
                Globals.txt_pd2_s6.ItemsTXT.MiscTXT = MiscTXT.Read(Globals.TEXT_DIR + @"pd2_s6\Misc.txt");
                Globals.txt_pd2_s7.ItemStatCostTXT = ItemStatCostTXT.Read(Globals.TEXT_DIR + @"pd2_s7\ItemStatCost.txt");
                Globals.txt_pd2_s7.ItemsTXT.ArmorTXT = ArmorTXT.Read(Globals.TEXT_DIR + @"pd2_s7\Armor.txt");
                Globals.txt_pd2_s7.ItemsTXT.WeaponsTXT = WeaponsTXT.Read(Globals.TEXT_DIR + @"pd2_s7\Weapons.txt");
                Globals.txt_pd2_s7.ItemsTXT.MiscTXT = MiscTXT.Read(Globals.TEXT_DIR + @"pd2_s7\Misc.txt");
                Globals.txt_pd2_s8.ItemStatCostTXT = ItemStatCostTXT.Read(Globals.TEXT_DIR + @"pd2_s8\ItemStatCost.txt");
                Globals.txt_pd2_s8.ItemsTXT.ArmorTXT = ArmorTXT.Read(Globals.TEXT_DIR + @"pd2_s8\Armor.txt");
                Globals.txt_pd2_s8.ItemsTXT.WeaponsTXT = WeaponsTXT.Read(Globals.TEXT_DIR + @"pd2_s8\Weapons.txt");
                Globals.txt_pd2_s8.ItemsTXT.MiscTXT = MiscTXT.Read(Globals.TEXT_DIR + @"pd2_s8\Misc.txt");
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
            if (input_convert == "vanilla" || input_convert == "\"vanilla\"" || input_convert == "1" || input_convert == "(1)" || input_convert == "van") Globals.convert_to = "vanilla";
            else if (input_convert == "\\") return;
            Console.Write($"Files in {Globals.INPUT_DIR.Substring(Globals.PROJECT_DIRECTORY.Length, Globals.INPUT_DIR.Length - Globals.PROJECT_DIRECTORY.Length - 1)} will be converted to {Globals.convert_to}");
            //if (Globals.convert_to != "vanilla") Console.Write($" (season {Globals.MOST_RECENT_SEASON})");
            Console.Write(".\r\n\r\n");

            if (Globals.convert_to != "vanilla")
            {
                Console.WriteLine($"If all files are from a specific season, conversion speed can be improved if that season's format is assumed.");
                //Console.Write($"Enter a season number (1-{Globals.MOST_RECENT_SEASON}) to assume, or leave blank to auto-detect: ");
                Console.Write($"Enter a season number to assume, or leave blank to auto-detect: ");
                string input_forced_version = Console.ReadLine();
                // TODO: Reimplement variables via a less hardcoded method to reduce the number of lines it takes to accomplish the same thing
                if (input_forced_version == "1") Globals.force_convert_from = "pd2_s1";
                else if (input_forced_version == "2") Globals.force_convert_from = "pd2_s2";
                else if (input_forced_version == "3") Globals.force_convert_from = "pd2_s3";
                else if (input_forced_version == "4") Globals.force_convert_from = "pd2_s4";
                else if (input_forced_version == "5") Globals.force_convert_from = "pd2_s5";
                else if (input_forced_version == "6") Globals.force_convert_from = "pd2_s6";
                else if (input_forced_version == "7") Globals.force_convert_from = "pd2_s7";
                else if (input_forced_version == "8") Globals.force_convert_from = "pd2_s8";
                else if (input_forced_version == "\\") return;
                if (Globals.force_convert_from != "") Console.WriteLine($"Files will be assumed to be from season {input_forced_version}.");
                else Console.WriteLine("File formats will be auto-detected.");
                Console.WriteLine();
            }

            Console.Write("Enter the character's name, or leave blank to convert all files: ");
            string input_name = Console.ReadLine();
            if (input_name == "\\") return;

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
                    else if (!(files[f].EndsWith(".d2x") || files[f].EndsWith(".sss") || files[f].EndsWith(".stash.hc") || files[f].EndsWith(".stash") || files[f].EndsWith(".key") || files[f].EndsWith(".map") || files[f].EndsWith(".ma0") || files[f].EndsWith(".ma1") || files[f].EndsWith(".ma2") || files[f].EndsWith(".ma3") || file_name == "place files here")) Console.WriteLine($"{file_name} is not a recognized character/stash file.");  // no message shown for other character files (.key, .map, .ma0, .ma1, .ma2, .ma3)
                }
            }

            if (Globals.ladder_files_converted) Console.WriteLine("Files denoted with \"NL\" were converted from ladder to non-ladder.");

            bool found_default = false;
            int num_files = 0;
            if (Globals.convert_to != "vanilla" && input_name == "")
            {
                Console.WriteLine("");
                Console.WriteLine($"Reading all stash files...");
                string[] files = Directory.GetFiles(Globals.INPUT_DIR);
                string file_name = "";
                num_files = files.Length;
                if (files.Length > 0)
                {
                    for (int f = 0; f < files.Length; f++)
                    {
                        file_name = files[f].Substring(Globals.INPUT_DIR.Length, files[f].Length - Globals.INPUT_DIR.Length);
                        if (files[f].EndsWith(".sss")) ConvertStash(file_name.Substring(0, file_name.Length), ".sss");
                        else if (files[f].EndsWith(".d2x")) ConvertStash(file_name.Substring(0, file_name.Length), ".d2x");
                        else if (files[f].EndsWith(".stash.hc")) ConvertStash(file_name.Substring(0, file_name.Length), ".stash.hc");
                        else if (files[f].EndsWith(".stash")) ConvertStash(file_name.Substring(0, file_name.Length), ".stash");
                        else if (file_name == "place files here") found_default = true;
                    }
                }
            }

            if (input_name == "")
            {
                Console.WriteLine();
                if (num_files == 0 || (num_files == 1 && found_default)) Console.WriteLine($"No files found.\r\nPlace files in {Globals.INPUT_DIR}");
                if (Globals.files_converted > 0) Console.WriteLine($"Converted files were saved in {Globals.OUTPUT_DIR.Substring(Globals.PROJECT_DIRECTORY.Length, Globals.OUTPUT_DIR.Length - Globals.PROJECT_DIRECTORY.Length - 1)}");
                if (Globals.files_ignored > 0) Console.WriteLine("Some files could not be converted.");
            }

            Console.Write("Press enter to close.");
            Console.ReadLine();

            // TODO: fix Standard of Heroes from vanilla/S1 to S2+... fails when trying to read? So are the text files inadequate somehow? Or is it something else? Text differences: unique=1, stackable=0, minstack=0, maxstack=0, spawnstack=0 -> unique=0, stackable=1, minstack=1, maxstack=50, spawnstack=1
            // TODO: fix "personalized" items
        }

        public static void ConvertCharacter(string input_name)
        {
            // TODO: Reimplement try-catch blocks via a less hardcoded method to reduce the number of lines it takes to accomplish the same thing
            Console.Write($"Reading {input_name}{Globals.CFE}... ");

            D2S character = new D2S();
            Globals.space = new byte[000];
            Globals.pd2_stash_formatting = false;

            if (Globals.force_convert_from != "")
            {
                Globals.pd2_char_formatting = true;
                Globals.convert_from = Globals.force_convert_from;
                if (Globals.convert_from == "pd2_s1") Core.TXT = Globals.txt_pd2_s1;
                else if (Globals.convert_from == "pd2_s2") Core.TXT = Globals.txt_pd2_s2;
                else if (Globals.convert_from == "pd2_s3") Core.TXT = Globals.txt_pd2_s3;
                else if (Globals.convert_from == "pd2_s4") Core.TXT = Globals.txt_pd2_s4;
                else if (Globals.convert_from == "pd2_s5") Core.TXT = Globals.txt_pd2_s5;
                else if (Globals.convert_from == "pd2_s6") Core.TXT = Globals.txt_pd2_s6;
                else if (Globals.convert_from == "pd2_s7") Core.TXT = Globals.txt_pd2_s7;
                else if (Globals.convert_from == "pd2_s8") Core.TXT = Globals.txt_pd2_s8;
                try
                {
                    character = Core.ReadD2S(File.ReadAllBytes(Globals.INPUT_DIR + input_name + Globals.CFE));
                }
                catch
                {
                    Globals.convert_from = "?";
                }
            }

            if (Globals.force_convert_from == "" || Globals.convert_from == "?")
            {
                Globals.pd2_char_formatting = false;
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
                                Globals.convert_from = "pd2_s4";
                                Core.TXT = Globals.txt_pd2_s4;
                                try
                                {
                                    character = Core.ReadD2S(File.ReadAllBytes(Globals.INPUT_DIR + input_name + Globals.CFE));
                                }
                                catch
                                {
                                    Globals.convert_from = "pd2_s5";
                                    Core.TXT = Globals.txt_pd2_s5;
                                    try
                                    {
                                        character = Core.ReadD2S(File.ReadAllBytes(Globals.INPUT_DIR + input_name + Globals.CFE));
                                    }
                                    catch
                                    {
                                        Globals.convert_from = "pd2_s6";
                                        Core.TXT = Globals.txt_pd2_s6;
                                        try
                                        {
                                            character = Core.ReadD2S(File.ReadAllBytes(Globals.INPUT_DIR + input_name + Globals.CFE));
                                        }
                                        catch
                                        {
                                            Globals.convert_from = "pd2_s7";
                                            Core.TXT = Globals.txt_pd2_s7;
                                            try
                                            {
                                                character = Core.ReadD2S(File.ReadAllBytes(Globals.INPUT_DIR + input_name + Globals.CFE));
                                            }
                                            catch
                                            {
                                                Globals.convert_from = "pd2_s8";
                                                Core.TXT = Globals.txt_pd2_s8;
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
                                }
                            }
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
                if (Globals.convert_to == "vanilla")
                {
                    Core.TXT = Globals.txt_vanilla;
                    /*
                    if (Globals.convert_from != "vanilla")
                    {
                        if (Globals.space != new byte[000]) ;   // TODO: test if this actually stores the info correctly
                        int extra_skill_points = (int) Globals.space[0] + (int) Globals.space[1] + (int) Globals.space[2];    // TODO: this should store the total skill points allocated in "new" skills
                        Globals.space = new byte[000];
                        character.Attributes.Stats["newskills"] = character.Attributes.Stats["newskills"] + extra_skill_points; // TODO: test whether this actually works when converting PD2 characters that had points allocated in "new" skills
                    }
                    */
                }
                else Core.TXT = Globals.txt_pd2_s8;     // attempts to convert to most recent version

                bool write_success = true;
                try
                {
                    File.WriteAllBytes(Globals.OUTPUT_DIR + input_name + Globals.CFE, Core.WriteD2S(character));    // TODO: This can't convert non-vanilla items/affixes back to vanilla - maybe it should try removing them?
                }
                catch
                {
                    write_success = false;
                    Console.Write(" couldn't write file\r\n");
                    Globals.files_ignored += 1;
                }
                if (write_success)
                {
                    // setup to verify file integrity
                    bool reread_success = true;
                    if (Globals.convert_to != "vanilla")
                    {
                        Globals.convert_from = "pd2_s8";
                        Core.TXT = Globals.txt_pd2_s8;
                    }
                    else
                    {
                        Globals.convert_from = "vanilla";
                        Core.TXT = Globals.txt_vanilla;
                    }
                    try
                    {
                        // attempt to read converted file to verify integrity
                        character = Core.ReadD2S(File.ReadAllBytes(Globals.OUTPUT_DIR + input_name + Globals.CFE));
                    }
                    catch
                    {
                        reread_success = false;
                    }
                    if (reread_success)
                    {
                        Console.Write(" saved");
                        Globals.files_converted += 1;
                        if (was_ladder)
                        {
                            Console.Write(" NL");
                            Globals.ladder_files_converted = true;
                        }
                        Console.Write("\r\n");
                    }
                    else
                    {
                        Console.Write(" couldn't save\r\n");
                        File.Delete(Globals.OUTPUT_DIR + input_name + Globals.CFE);
                    }
                }
            }
            else
            {
                Console.Write(" ignored (unknown file format)\r\n");
                Globals.files_ignored += 1;
            }

        }

        public static void ConvertStash(string stash_name, string type)
        {
            // TODO: Reimplement try-catch blocks via a less hardcoded method to reduce the number of lines it takes to accomplish the same thing
            Console.Write($"Reading {stash_name}... ");

            Globals.pd2_char_formatting = false;
            Globals.pd2_stash_formatting = false;
            if (type == ".stash" || type == ".stash.hc") Globals.pd2_stash_formatting = true;
            D2I stash = new D2I();
            UInt16 version = 0x60;

            if (Globals.force_convert_from != "")
            {
                Globals.convert_from = Globals.force_convert_from;
                if (Globals.convert_from == "pd2_s1") Core.TXT = Globals.txt_pd2_s1;
                else if (Globals.convert_from == "pd2_s2") Core.TXT = Globals.txt_pd2_s2;
                else if (Globals.convert_from == "pd2_s3") Core.TXT = Globals.txt_pd2_s3;
                else if (Globals.convert_from == "pd2_s4") Core.TXT = Globals.txt_pd2_s4;
                else if (Globals.convert_from == "pd2_s5") Core.TXT = Globals.txt_pd2_s5;
                else if (Globals.convert_from == "pd2_s6") Core.TXT = Globals.txt_pd2_s6;
                else if (Globals.convert_from == "pd2_s7") Core.TXT = Globals.txt_pd2_s7;
                else if (Globals.convert_from == "pd2_s8") Core.TXT = Globals.txt_pd2_s8;
                try
                {
                    stash = Core.ReadD2I(Globals.INPUT_DIR + stash_name, version, type);
                }
                catch
                {
                    Globals.convert_from = "?";
                }
            }

            if (Globals.force_convert_from == "" || Globals.convert_from == "?")
            {
                Globals.convert_from = "vanilla";
                Core.TXT = Globals.txt_vanilla;
                try
                {
                    stash = Core.ReadD2I(Globals.INPUT_DIR + stash_name, version, type);
                }
                catch
                {
                    Globals.convert_from = "pd2_s1";
                    Core.TXT = Globals.txt_pd2_s1;
                    try
                    {
                        stash = Core.ReadD2I(Globals.INPUT_DIR + stash_name, version, type);
                    }
                    catch
                    {
                        Globals.convert_from = "pd2_s2";
                        Core.TXT = Globals.txt_pd2_s2;
                        try
                        {
                            stash = Core.ReadD2I(Globals.INPUT_DIR + stash_name, version, type);
                        }
                        catch
                        {
                            Globals.convert_from = "pd2_s3";
                            Core.TXT = Globals.txt_pd2_s3;
                            try
                            {
                                stash = Core.ReadD2I(Globals.INPUT_DIR + stash_name, version, type);
                            }
                            catch
                            {
                                Globals.convert_from = "pd2_s4";
                                Core.TXT = Globals.txt_pd2_s4;
                                try
                                {
                                    stash = Core.ReadD2I(Globals.INPUT_DIR + stash_name, version, type);
                                }
                                catch
                                {
                                    Globals.convert_from = "pd2_s5";
                                    Core.TXT = Globals.txt_pd2_s5;
                                    try
                                    {
                                        stash = Core.ReadD2I(Globals.INPUT_DIR + stash_name, version, type);
                                    }
                                    catch
                                    {
                                        Globals.convert_from = "pd2_s6";
                                        Core.TXT = Globals.txt_pd2_s6;
                                        try
                                        {
                                            stash = Core.ReadD2I(Globals.INPUT_DIR + stash_name, version, type);
                                        }
                                        catch
                                        {
                                            Globals.convert_from = "pd2_s7";
                                            Core.TXT = Globals.txt_pd2_s7;
                                            try
                                            {
                                                stash = Core.ReadD2I(Globals.INPUT_DIR + stash_name, version, type);
                                            }
                                            catch
                                            {
                                                Globals.convert_from = "pd2_s8";
                                                Core.TXT = Globals.txt_pd2_s8;
                                                try
                                                {
                                                    stash = Core.ReadD2I(Globals.INPUT_DIR + stash_name, version, type);
                                                }
                                                catch
                                                {
                                                    Globals.convert_from = "?";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (Globals.convert_from != "?")
            {
                Console.Write($"[{Globals.convert_from}]... ");
                Core.TXT = Globals.txt_pd2_s8;     // attempts to convert to most recent version   ...TODO: Why isn't this specified for non-vanilla conversions only like the equivalent line in the ConvertCharacter() function? Did I never enable ConvertStash() to convert PD2 stash files to vanilla stash files?
                
                bool write_success = true;
                try
                {
                    File.WriteAllBytes(Globals.OUTPUT_DIR + stash_name, Core.WriteD2I(stash, version, type));
                }
                catch
                {
                    write_success = false;
                    Console.Write(" couldn't write file\r\n");
                    Globals.files_ignored += 1;
                }
                if (write_success)
                {
                    // setup to verify file integrity
                    bool reread_success = true;
                    if (Globals.convert_to != "vanilla")
                    {
                        Globals.convert_from = "pd2_s8";
                        Core.TXT = Globals.txt_pd2_s8;
                    }
                    else
                    {
                        Globals.convert_from = "vanilla";
                        Core.TXT = Globals.txt_vanilla;
                    }
                    try
                    {
                        // attempt to read converted file to verify integrity
                        stash = Core.ReadD2I(Globals.OUTPUT_DIR + stash_name, version, type);
                    }
                    catch
                    {
                        reread_success = false;
                    }
                    if (reread_success)
                    {
                        Console.Write(" saved\r\n");
                        Globals.files_converted += 1;
                    }
                    else
                    {
                        Console.Write(" couldn't save\r\n");
                        File.Delete(Globals.OUTPUT_DIR + stash_name);
                    }
                }
            }
            else
            {
                Console.Write(" ignored (unknown file format)\r\n");
                Globals.files_ignored += 1;
            }
        }

    }
}
