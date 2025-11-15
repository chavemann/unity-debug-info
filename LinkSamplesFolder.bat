@echo off
REM Unity requires samples to be placed in a "Samples~" folder, but any folder ending in "~"
REM is ignore by Unity making it difficult to edit the sample files.
REM This creates a link/junction so that the sample files can be edited under the dev packagename_samples folder
REM and also exist inside the required "Samples~" folder.
REM Make sure to ignore the "PackageName_Samples" folder in version control.
REM mklink /J <LINK_NAME> <SOURCE_FOLDER>
@echo on
mklink /J "Assets/DebugInfo_Samples" "Assets/DebugInfo/Samples~"