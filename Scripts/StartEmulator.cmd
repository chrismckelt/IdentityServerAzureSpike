@echo Starting Emulator
cd "%~dp0"
call StopServices.cmd

call "C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator\WAStorageEmulator.exe" start
del c:\temp\identitydemo.log
rem pause