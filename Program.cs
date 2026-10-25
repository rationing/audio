using NAudio.CoreAudioApi;
using NAudio.Effects;
using NAudio.Wave;

// Prepare for recording
Console.Clear();
var wasapiRecorderBuilder = new WasapiRecorderBuilder();

// Record system audio (output from speakers)
// wasapiRecorderBuilder.WithLoopbackCapture();

// Record from an active audio capture device
var enumerator = new MMDeviceEnumerator();
var flow = DataFlow.Capture;
var state = DeviceState.Active;
var points = enumerator.EnumerateAudioEndPoints(flow, state);
wasapiRecorderBuilder.WithDevice(points[0]);
Console.WriteLine("Device used: " + points[0].FriendlyName);

// Build the recorder; last chance to change parameters
var wasapiRecorder = wasapiRecorderBuilder.Build();		


// Write to a WAV file
var wf = wasapiRecorder.WaveFormat;
Console.WriteLine("Sample Rate: " + wf.SampleRate);
Console.WriteLine("Bit depth: " + wf.BitsPerSample);
Console.WriteLine("Channels: " + wf.Channels);
var waveFileWriter = new WaveFileWriter("0.wav", wf);

// Handle the DataAvailable event after subscribing
wasapiRecorder.DataAvailable += (data, _, _, _) =>
{
    waveFileWriter.Write(data);
};

// Start recording
Console.WriteLine("Press ENTER to start recording.");
Console.ReadLine();
wasapiRecorder.StartRecording();
Console.WriteLine("Press ENTER to stop recording.");
Console.ReadLine();

// Stop recording
wasapiRecorder.StopRecording();
wasapiRecorder.Dispose();

// Closing the writer fixes the WAV header
waveFileWriter.Dispose();

// Always save a louder version
var audioFile = new AudioFileReader("0.wav");
var gainEffect = new GainEffect();
gainEffect.GainDb = 20f;
var esp = new EffectSampleProvider(audioFile, gainEffect);
WaveFileWriter.CreateWaveFile16("1.wav", esp);

// Write final message to the console
Console.WriteLine("Done. Please listen to 0.wav and 1.wav");
Console.WriteLine("The app will exit now.");