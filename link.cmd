@echo off
rmdir %~dp0"Assets\LarkFramework"
mklink /d %~dp0"Assets\LarkFramework" %~dp0"..\LarkFramework_V2\Assets\LarkFramework"
rmdir %~dp0"Assets\LarkTools"
mklink /d %~dp0"Assets\LarkTools" %~dp0"..\LarkTools\Assets\LarkTools"
