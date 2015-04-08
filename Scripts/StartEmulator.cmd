@echo Starting Emulator
cd "%~dp0"
call StopServices.cmd
timeout /t 5 /nobreak
del c:\logs\identitydemo.log
del c:\logs\sitea.log
call "C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator\WAStorageEmulator.exe" start

rem pause