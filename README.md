This repository contains the code for a .NET console app.
I created this in C# to give myself an easy way to record
and save system audio on my Windows 10 laptop.

```
git clone https://github.com/rationing/audio.git
cd audio
dotnet run
```

If you also use Windows, make sure to disable any audio
enhancements that Windows uses by default. They will ruin
your audio quality when recording system audio through
either the output device "Speakers" or the input device
"Stereo Mix". Open "Sound Control Panel" to disable them.

Running the console app will create two files:
- 0.wav contains the raw data.
- 1.wav contains a louder version.