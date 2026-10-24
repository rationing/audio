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
// by handling the DataAvailable event
wasapiRecorder.DataAvailable += (data, _, _, _) =>
{
    waveFileWriter.Write(data);
};

// Start recording
wasapiRecorder.StartRecording();

// Give the recorder some time to actually pick up
// audio from the system
Thread.Sleep(10000);

// Stop recording
wasapiRecorder.StopRecording();

// Close the recorder and the writer
// Closing the writer corrects the WAV header
wasapiRecorder.Dispose();
waveFileWriter.Dispose();

Console.WriteLine("1");