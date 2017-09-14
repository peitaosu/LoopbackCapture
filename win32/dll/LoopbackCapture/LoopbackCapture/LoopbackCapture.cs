/* Some Code Fragments from:
   1. https://stackoverflow.com/questions/18812224/c-sharp-recording-audio-from-soundcard - Florian R.
 */

using System;
using CSCore;
using CSCore.SoundIn;
using CSCore.Codecs.WAV;
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

            //set wave format
            WaveFormat wave_format = new WaveFormat(44100, 32, 2, AudioEncoding.Pcm);
            int latency = 100;

            using (WasapiCapture capture = new WasapiLoopbackCapture(latency, wave_format))
            {

                //initialize the selected device for recording
                capture.Initialize();

                //create a wavewriter to write the data to
                using (WaveWriter w = new WaveWriter(output_file, capture.WaveFormat))
                {
                    //setup an eventhandler to receive the recorded data
                    capture.DataAvailable += (s, e) =>
                    {
                        //save the recorded audio
                        w.Write(e.Data, e.Offset, e.ByteCount);
                    };

                    //start recording
                    capture.Start();

                    //delay and keep recording
                    Thread.Sleep(time);

                    //stop recording
                    capture.Stop();
                }
            }
            return 0;
        }
    }
}
