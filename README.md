# Loopback Capture

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/peitaosu/LoopbackCapture/master/LICENSE)

## What is Loopback Capture

Loopback Capture is a tool which can be used to capture the loopback from audio devices.

## Build (Windows)

The C# version require some packages, before you build it, you need to install these packages:

* CSCore - An advanced audio library, written in C#.
* Unmanaged Exports - DllExport for .Net.

Suggest to use NuGet to install them.

## Install Soundflower (macOS)

macOS not support to capture Loopback from device directly. The workaround is to route what is playing on the computer digitally back to the input without using a cable.

Soundflower is a free open source system add-on for Mac computers that allows you to route what is playing on the computer digitally back to the input without using a cable. Set Soundflower as your system output device, then in Audacity, set Soundflower as your recording device.

You can get compiled Soundflower kernel extension in here: https://github.com/mattingalls/Soundflower/releases

About how to setup device, there is an example in Release Note please take a look.

## Usage

```
# CSharp

> LoopbackCapture.exe <output/wav> <time/milliseconds>

# C++

> LoopbackCapture.exe -?
> LoopbackCapture.exe --list-devices
> LoopbackCapture.exe [--device "Device long name"] [--file "file name"] [--time "capture milliseconds"] [--int-16]
    -? prints this message.
    --list-devices displays the long names of all active playback devices.
    --device captures from the specified device (default if omitted)
    --file saves the output to a file
    --time capture specific milliseconds
    --int-16 attempts to coerce data to 16-bit integer format

# Python (macOS)

> from mac.LoopbackCapture import record_sounds
> exit_code = record_sounds(audio_file, milliseconds)

```

