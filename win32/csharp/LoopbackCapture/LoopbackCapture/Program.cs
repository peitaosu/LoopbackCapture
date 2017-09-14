/* Some Code Fragments from:
   1. https://stackoverflow.com/questions/18812224/c-sharp-recording-audio-from-soundcard - Florian R.
 */

using System;
using CSCore;
using CSCore.SoundIn;
using CSCore.Codecs.WAV;
using System.Threading;

namespace LoopbackCapture
{
    class Program
    {
        static int Main(string[] args)
        {
            int time;
            string output_file;

            switch (args.Length)
            {
                case 1:
                    if (args[0] == "-h")
                    {
                        System.Console.WriteLine("Usage:");
                        System.Console.WriteLine("    LoopbackCapture.exe <output/wav> <time/milliseconds>");
                        return 1;
                    }
                    output_file = args[0];
                    time = 0;
                    break;
                case 2:
                    output_file = args[0];
                    try
                    {
                        time = Int32.Parse(args[1]);
                    }
                    catch
                    {
                        time = 0;
                    }
                    break;
                default:
                    time = 0;
                    output_file = "record.wav";
                    break;
            }

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
                    if (time != 0)
                    {
                        Thread.Sleep(time);
                    }
                    else
                    {
                        Console.ReadKey();
                    }

                    //stop recording
                    capture.Stop();
                }
            }
            return 0;
        }
    }
}
