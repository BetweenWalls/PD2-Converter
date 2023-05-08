## Simple Character/Stash Converter for PD2
This program converts character/stash files between vanilla and PD2, and updates PD2 files so they can be used with the most recent season without needing to drop old items.

Supports all PD2 characters and vanilla characters from v1.10+ (.d2s files), as well as shared/personal stashes from PlugY v11.02+ (.sss and .d2x files). Built from [dschu012's D2SLib](https://github.com/dschu012/D2SLib) repository. Requires .NET framework to run.

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

### Notes

I didn't bother separating the source code from the release code, so there are a bunch of extra folders/files that can be ignored.

There is currently an issue that prevents vanilla & season 1 files with *Standard of Heroes* from being converted to later seasons. Files with personalized items may be prevented from being converted correctly. Additionally, one report mentioned season 2 files with *Mephisto's Soulstone* preventing conversion as well. There may also be some corruption modifiers from earlier PD2 seasons which prevent conversion, but there hasn't been enough testing to confirm which those are.

Although primarily designed for converting vanilla or PD2 characters & stashes to the most recent PD2 season, characters may also be converted in the reverse direction. When converting characters from PD2 to vanilla, items or item affixes that don't exist in vanilla will prevent files with them from being converted. While this "reverse" conversion seems to otherwise work correctly for loading/playing those characters in the vanilla game, other programs (such as Hero Editor) may not load them correctly. When using such programs to edit characters from PD2, the characters should be converted to vanilla and saved from within the vanilla game prior to being edited.

<!--This program is unnecessary for converting season 3+ characters since there have been no major formatting differences since s2.-->

### Feedback

If you encounter bugs, let me know on reddit ([u/BetweenWalls](https://www.reddit.com/message/compose/?to=BetweenWalls)) or discord (@BetweenWalls#2390), or just submit an issue here.
