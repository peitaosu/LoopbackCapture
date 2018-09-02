import os, sys, clr

sys.path.append(os.path.dirname(os.path.realpath(__file__)))
clr.AddReference("CSCore")
from System import EventHandler, EventArgs, Array, Byte, Threading
from CSCore import *
from CSCore.SoundIn import *
from CSCore.Codecs.WAV import *
from CSCore.Streams import *

output_file = "out.wav"
time = 5000

sampleRate = 48000

soundIn = WasapiLoopbackCapture()
soundIn.Initialize()
soundInSource = SoundInSource(soundIn)
soundInSource.FillWithZeros = False
convertedSource = soundInSource
convertedSource = FluentExtensions.ChangeSampleRate(convertedSource, sampleRate)
waveWriter = WaveWriter(output_file, convertedSource.WaveFormat)

def capture(sender, event_args):
    buffer = Array.CreateInstance(Byte, convertedSource.WaveFormat.BytesPerSecond / 2)
    while True:
        read = convertedSource.Read(buffer, 0, buffer.Length)
        waveWriter.Write(buffer, 0, read)
        if read <= 0:
            break

delegate = EventHandler(capture)
soundInSource.DataAvailable += delegate

soundIn.Start()
Threading.Thread.Sleep(time)
soundIn.Stop()
