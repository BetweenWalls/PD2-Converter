## Simple Character/Stash Converter for PD2
This program converts character/stash files between vanilla and PD2, and updates PD2 files so they can be used with the most recent season without needing to drop old items.

Supports all PD2 characters and vanilla characters from v1.10+ (.d2s files), as well as shared/personal stashes from PlugY v11.02+ (.sss and .d2x files). Built from [dschu012's D2SLib](https://github.com/dschu012/D2SLib) repository. Requires .NET framework to run.

I didn't bother separating the source code from the release code, so there are a bunch of extra folders/files that can be ignored.

### [Download](https://github.com/BetweenWalls/PD2-Converter/archive/main.zip)

### Instructions
1. Download PD2-Converter-main.zip and extract the files
2. Copy your character and stash files from your save folder (Diablo II\Save) - the only files that matter are those ending in .d2s or .d2x or .sss
3. Navigate to PD2-Converter-main\src\main\input and paste your files there
4. Navigate to PD2-Converter-main\src\main and run PD2-Converter.exe - a command line menu will pop up
5. For default operation, leave the 3 prompts blank by pressing enter 3 times
6. Wait for the program to convert your files
7. Navigate to PD2-Converter-main\src\main\output and copy the converted files from there back into your save folder 

If you want to revert to an older version of PD2 for whatever reason, you can replace your game files with the versions included in the Patches folder.

### Errors
The converter may not work in all cases - if the converter doesn't work for a particular file, you can use the traditional method of reverting to the previous season and dropping whichever items were fundamentally altered after that season. For example, the transition from s6 to s7 saw maps being fundamentally altered to become stackable. If you drop the maps, there's no need to convert anything. If you want to try keeping the maps, transfer them all to another character instead and then use the converter on that character only.

Fundamentally altered items per season:
* S1: maps, Standard of Heroes
* S2: maps, any item with Enhanced Damage or Maximum Damage per Level
* S7: maps (including dungeons & arenas)

The converter may have trouble with:
* Standard of Heroes (S2 or earlier)
* Mephisto's Soulstone (S2 or earlier)
* Personalized items
* Items with specific corruptions that were removed/changed in later seasons (details unknown)

### Reverse Conversions
Although primarily designed for converting vanilla or PD2 characters & stashes to the most recent PD2 season, characters may also be converted in the reverse direction. When converting characters from PD2 to vanilla, items or item affixes that don't exist in vanilla will prevent files with them from being converted. While this "reverse" conversion seems to otherwise work correctly for loading/playing those characters in the vanilla game, other programs (such as Hero Editor) may not load them correctly. To fix this issue when using such programs to edit characters from PD2, the characters should be converted to vanilla and *saved from within the vanilla game* prior to being edited.

### Feedback

If you encounter bugs, let me know on reddit ([u/BetweenWalls](https://www.reddit.com/message/compose/?to=BetweenWalls)) or discord (@BetweenWalls#2390), or just submit an issue here.
