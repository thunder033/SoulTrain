# SoulTrain

###Recommended Tools:
- Visual Studio 2015
- Unity 5.2(.4)
- SourceTree (1.7)
- Maya 2016

####Using Visual Studio as Diff Tool in Source Tree

1. Open Options menu through `Tools` > `Options`
2. Under `Diff` tab, set `External Diff Tool` to `Custom`
3. Set the `Diff Command` to `[VS Install Directory]\Common7\IDE\vsDiffMerge.exe`, where `[VS Install Directory]` is probably `C:\Program Files (x86)\Visual Studio 14.0` and `Arguments` to `$LOCAL $REMOTE \\t`
4. Under `Diff` tab, set `External Merge Tool` to `Custom`
5. Set the `Diff Command` to `[VS Install Directory]\Common7\IDE\vsDiffMerge.exe`, where `[VS Install Directory]` is probably `C:\Program Files (x86)\Visual Studio 14.0` and `Arguments` to `$LOCAL $REMOTE $BASE $MERGED \\m`
