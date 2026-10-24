using NAudio.CoreAudioApi;
using NAudio.Wave;

Console.WriteLine("0");

// Record system audio
// var wasapiRecorderBuilder = new WasapiRecorderBuilder();
// wasapiRecorderBuilder.WithLoopbackCapture();
// var wasapiRecorder = wasapiRecorderBuilder.Build();

// Record from an active audio capture device
var wasapiRecorderBuilder = new WasapiRecorderBuilder();
var enumerator = new MMDeviceEnumerator();
var flow = DataFlow.Capture;
var state = DeviceState.Active;
var points = enumerator.EnumerateAudioEndPoints(flow, state);
wasapiRecorderBuilder.WithDevice(points[0]);
var wasapiRecorder = wasapiRecorderBuilder.Build();		

// Write to a WAV file
var waveFormat = wasapiRecorder.WaveFormat;
var waveFileWriter = new WaveFileWriter("0.wav", waveFormat);

// Handle the DataAvailable event after subscribing
wasapiRecorder.DataAvailable += (data, _, _, _) =>
{
    waveFileWriter.Write(data);
};

// Start recording
Console.WriteLine("Press ENTER to start recording.");
Console.ReadLine();
wasapiRecorder.StartRecording();
Console.WriteLine("Recording now!");

// Recording length in milliseconds
Thread.Sleep(10000);

// Stop recording
wasapiRecorder.StopRecording();
wasapiRecorder.Dispose();

// Closing the writer corrects the WAV header
waveFileWriter.Dispose();

Console.WriteLine("1");