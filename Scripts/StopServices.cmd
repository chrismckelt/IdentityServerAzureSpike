@echo Stopping IISExpress and Emulator
TaskKill /F /IM iisexpress.exe
"C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator\WAStorageEmulator.exe" stop
pause