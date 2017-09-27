/* Some Code Fragments from:
   1. https://stackoverflow.com/questions/18812224/c-sharp-recording-audio-from-soundcard - Florian R.
   2. https://github.com/filoe/cscore/blob/master/Samples/RecordWithSpecificFormat/Program.cs - CSCore Source Code
 */

using System;
using CSCore;
using CSCore.SoundIn;
using CSCore.Codecs.WAV;
using CSCore.Streams;
using System.Threading;
using System.Runtime.InteropServices;
using RGiesecke.DllExport;

namespace LoopbackCapture
{
    public class LoopbackCapture
    {
        [DllExport("Capture", CallingConvention = CallingConvention.Cdecl)]
        public static int Capture(string output_file, int time)
        {

            int sampleRate = 48000;
            int bitsPerSample = 24;


            //create a new soundIn instance
            using (WasapiCapture soundIn = new WasapiLoopbackCapture())
            {

                //initialize the soundIn instance
                soundIn.Initialize();

                //create a SoundSource around the the soundIn instance
                SoundInSource soundInSource = new SoundInSource(soundIn) { FillWithZeros = false };

                //create a source, that converts the data provided by the soundInSource to any other format
                IWaveSource convertedSource = soundInSource
                    .ChangeSampleRate(sampleRate) // sample rate
                    .ToSampleSource()
                    .ToWaveSource(bitsPerSample); //bits per sample

                //channels...
                using (convertedSource = convertedSource.ToStereo())
                {

                    //create a new wavefile
                    using (WaveWriter waveWriter = new WaveWriter(output_file, convertedSource.WaveFormat))
                    {

                        //register an event handler for the DataAvailable event of the soundInSource
                        soundInSource.DataAvailable += (s, e) =>
                        {
                            //read data from the converedSource
                            byte[] buffer = new byte[convertedSource.WaveFormat.BytesPerSecond / 2];
                            int read;

                            //keep reading as long as we still get some data
                            while ((read = convertedSource.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                //write the read data to a file
                                waveWriter.Write(buffer, 0, read);
                            }
                        };

                        //start recording
                        soundIn.Start();

                        //delay and keep recording
                        Thread.Sleep(time);

                        //stop recording
                        soundIn.Stop();
                    }
                }
            }
            return 0;
        }
    }
}
