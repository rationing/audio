using NAudio.Wave;

Console.WriteLine("0");

// Record system audio
var wasapiRecorderBuilder = new WasapiRecorderBuilder();
wasapiRecorderBuilder.WithLoopbackCapture();
var wasapiRecorder = wasapiRecorderBuilder.Build();

// Write to a WAV file
var waveFormat = new WaveFormat();
var waveFileWriter = new WaveFileWriter("0.wav", waveFormat);

// Write to the WAV file when data is available
wasapiRecorder.DataAvailable += (a, _, _, _) =>
{
    waveFileWriter.Write(a);
};

// Start recording
wasapiRecorder.StartRecording();

// Stop recording
wasapiRecorder.StopRecording();

Console.WriteLine("1");