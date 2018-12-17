import pyaudio
import wave

def record_sounds(output_file="record.wav", time=0):
    FORMAT = pyaudio.paInt16
    CHANNELS = 2
    RATE = 44100
    CHUNK = 1024
    RECORD_SECONDS = time/1000
    WAVE_OUTPUT_FILENAME = output_file
    audio = pyaudio.PyAudio()
    stream = audio.open(format=FORMAT, channels=CHANNELS, rate=RATE, input=True, frames_per_buffer=CHUNK)
    frames = []

    if time is not 0:
        for i in range(0, int(RATE / CHUNK * RECORD_SECONDS)):
            data = stream.read(CHUNK, False)
            frames.append(data)
    else:
        try:
            print("Press Ctrl+C to exit...")
            while True:
                data = stream.read(CHUNK, False)
                frames.append(data)
        except KeyboardInterrupt:
            pass
        except:
            return -1

    stream.stop_stream()
    stream.close()
    audio.terminate()

    wave_file = wave.open(WAVE_OUTPUT_FILENAME, 'wb')
    wave_file.setnchannels(CHANNELS)
    wave_file.setsampwidth(audio.get_sample_size(FORMAT))
    wave_file.setframerate(RATE)
    wave_file.writeframes(b''.join(frames))
    wave_file.close()

    return 0
