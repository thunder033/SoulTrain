# SoulTrain

###Recommended Tools:
- Visual Studio 2015
- Unity 5.2(.4)
- SourceTree (1.7)
- Maya 2016

###Using Visual Studio as Diff Tool in Source Tree

####Setup
1. Open Options menu through `Tools` > `Options`
2. Under `Diff` tab, set `External Diff Tool` to `Custom`
3. Set the `Diff Command` to `[VS Install Directory]\Common7\IDE\vsDiffMerge.exe`, where `[VS Install Directory]` is probably `C:\Program Files (x86)\Visual Studio 14.0` and `Arguments` to `$LOCAL $REMOTE \\t`
4. Under `Diff` tab, set `External Merge Tool` to `Custom`
5. Set the `Diff Command` to `[VS Install Directory]\Common7\IDE\vsDiffMerge.exe`, where `[VS Install Directory]` is probably `C:\Program Files (x86)\Visual Studio 14.0` and `Arguments` to `$LOCAL $REMOTE $BASE $MERGED \\m`

####Usage

When a merge conflict is encountered in SourceTree:

1. Right click on the conflicted file and select `Resolve Conflicts` > `Launch External Merge Tool` *(note: Visual Studio may take a few seconds to start up)*
2. When Visual Studio Luanches, select the correct edits and make adjustment to resolve conflict
3. Click `Accept Merge`
4. When you focus back to SourceTree, the merged file should be automatically Staged and a `[conflicted file].orig` created. The `.orig` file can be safely deleted.
